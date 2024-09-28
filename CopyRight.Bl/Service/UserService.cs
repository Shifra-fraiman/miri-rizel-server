using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dto.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;

namespace CopyRight.Bl.Service
{
    public class UserService : IUser
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public UserService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }


        public async Task<User> CreateAsync(User item)
        {
            try
            {
                //check if the email is unique
                List<Dal.Models.User> users = await dalManager.users.ReadAsync(o => o.Email == item.Email);
                Dal.Models.User u = users.FirstOrDefault();
                if (u != null)
                    throw new Exception("Email must be unique. This email is already in the system") { Data = { ["StatusCode"] = 409 } };
                //check that the role id exist in the system
                List<Dal.Models.RoleCode> roles = await dalManager.users.ReadAllRoleAsync();
                var roleIs = roles.FirstOrDefault(role => role.Id == item.Role.Id);
                if (roleIs == null) throw new Exception("The role is not exist in the system");
                //signUp the user
                item.UserId = 0;
                item.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
                var newUser = mapper.Map<Dal.Models.User>(item);
                return mapper.Map<User>(await dalManager.users.CreateAsync(newUser));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User> LogInAsync(string email, string password)
        {
            try
            {
                return mapper.Map<User>(await dalManager.users.LogInAsync(email, password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> LogInGoogleAsync(string email)
        {
            try
            {
                return mapper.Map<User>(await dalManager.users.LogInGoogleAsync(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(int item)
        {
            try
            {
                List<Dal.Models.User> users = await dalManager.users.ReadAsync(o => o.UserId == item);
                Dal.Models.User u = users.FirstOrDefault();
                if (u == null)
                    return false;
                //throw new Exception("User not found in the database.") { Data = { ["StatusCode"] = 404 } };
                return await dalManager.users.DeleteAsync(u.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            try
            {
                List<Dal.Models.User> users = await dalManager.users.ReadAsync(o => o.Email == email);
                Dal.Models.User u = users.FirstOrDefault();
                if (u != null)
                    return await dalManager.users.DeleteAsync(u.UserId);
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                List<Dal.Models.User> users = await dalManager.users.ReadAsync(o => o.UserId == id);
                Dal.Models.User u = users.FirstOrDefault();
                if (u != null)
                    return await dalManager.users.DeleteAsync(u.UserId);
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                Dal.Models.User u = await dalManager.users.GetByIdAsync(id);
                return mapper.Map<User>(u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                Dal.Models.User u = await dalManager.users.GetByEmailAsync(email);
                return mapper.Map<User>(u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<User>> ReadAsync(Predicate<User> filter)
        {
            try
            {
                List<User> list = await ReadAllAsync();
                list.ForEach(l1 => Console.WriteLine($"list: {l1.UserId}, Email: {l1.Email}, Password: {l1.Password}"));
                List<User> filteredList = list.Where(user => filter(user)).ToList();
                return filteredList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<User>> ReadAllAsync() => mapper.Map<List<Dal.Models.User>, List<User>>(await dalManager.users.ReadAllAsync());

        public async Task<bool> UpdateAsync(User item) => await dalManager.users.UpdateAsync(mapper.Map<User, Dal.Models.User>(item));
        public async Task<bool> UpdatePassword(string email, string password)
        {
            password = BCrypt.Net.BCrypt.HashPassword(password);
            return await dalManager.users.UpdatePassword(email, password);
        }

        public async Task<bool> SendResetEmail(string email, string tempPassword)
        {
            // יצירת חיבור לשרת SMTP
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "email.copyright.noreply@gmail.com";
            string smtpPassword = "hihr jqkb yohr mbwr";
            bool enableSsl = true;

            // כתובת ה-URL של הלוגו
            //string logoUrl = "https://i.imgur.com/sl1f9jq.jpeg";
            string logoUrl = "https://iili.io/dLN0ep4.png";

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = enableSsl;

                var message = new MailMessage();
                message.From = new MailAddress(smtpUsername, "CopyRight");
                message.To.Add(email);
                message.Subject = "בקשתך לאיפוס סיסמה";
                message.IsBodyHtml = true; // הגדרת גוף המייל כ-HTML

                string contactDetails = $@"{smtpUsername} :צור קשר ";
                string body = $@"
        <html>
        <head>
            <style>
                .container{{
                    font-family:'Varela Round', sans-serif;
                    background-color:#f4f4f4;
                    padding:5px;
                    text-align:center;
                    align-items:center;
                }}
                .email-container {{
                    margin:80px; 
                    background-color: #ffffff;
                    padding: 20px;
                    margin: 40px auto;
                    width: 80%;
                    max-width: 500px;
                    border: 1px solid #dddddd;
                    text-align:center;
                }}
                .logo {{
                    width: 125px;
                    margin-top: 20px;
                }}
                .verification-code {{
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    padding: 10px;
                    margin: 20px 0;
                    display: inline-block;
                    font-size:20px;
                }}
                .contact-details {{
                    margin-top: 10px;
                    font-size: 12px;
                    color: #888888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='email-container'>
                    <h2>בקשתך לאיפוס סיסמה</h2>
                    <p>:קוד האימות שלך הוא</p>
                    <div class='verification-code'>{tempPassword}</div>
                    <div class='contact-details'>{contactDetails}</div>
                </div>
            </div>
        </body>
        </html>";

                message.Body = body;

                try
                {
                    await client.SendMailAsync(message);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public string RandomaPassword()
        {

            Random rand = new Random();
            // יצירת קוד רנדומלי בן 5 ספרות
            int code = rand.Next(10000, 100000); // ערכי הגבול כוללים את 10000 והמקסימום כולל 100000
            return code.ToString();
        }

        public bool CheckCorrect(string email)
        {
            // ביטוי רגולרי לבדיקת תקינות כתובת מייל
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            // בדיקת התאמה של הכתובת לתבנית הרגולרית
            return Regex.IsMatch(email, pattern);
        }


        public async Task<List<Dto.Models.RoleCode>> ReadAllRoleAsync()
        {
            return mapper.Map<List<Dal.Models.RoleCode>, List<Dto.Models.RoleCode>>(await dalManager.users.ReadAllRoleAsync());
        }

        public async Task<bool> SendEmailSignUp(string name)
        {
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "email.copyright.noreply@gmail.com";
            string smtpPassword = "hihr jqkb yohr mbwr";
            bool enableSsl = true;
            // יצירת חיבור לשרת SMTP
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = enableSsl;
                // יצירת הודעת מייל
                var message = new MailMessage();
                message.From = new MailAddress(smtpUsername, "CopyRight");
                message.To.Add(smtpUsername);
                message.Subject = "נרשם משתמש חדש לאתר שלך";
                message.Body = $"{name} נרשם בהצלחה ";
                // שליחת המייל
                try
                {
                    await client.SendMailAsync(message);
                    return true;
                }
                catch
                {
                    return false;

                }
            }
        }

    }
}


