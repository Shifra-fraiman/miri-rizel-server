using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using System.Net.Mail;
using System.Net;

namespace CopyRight.Bl.Service
{
    public class DocumentService : IDocument
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public DocumentService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }

        public async Task<Documents> AddDocumentAsync(Documents document)
        {
            var documentDal = mapper.Map<Document>(document);
            var addedDocument = await dalManager.document.AddDocumentAsync(documentDal);
            return mapper.Map<Documents>(addedDocument);
        }
        public async Task<bool> SendEmail(string nameCustomer)
        {

            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "systemcopyright1@gmail.com";
            string smtpPassword = "sziq eykg egpi imcb";
            bool enableSsl = true;
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = enableSsl;
                // יצירת הודעת מייל
                var message = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "CopyRight"),
                    Subject = "נוסף מסמך חדש לדרייב שלך",
                    Body = $"המסמך שייך ללקוח : {nameCustomer}",
                    IsBodyHtml = false
                };
                message.To.Add(smtpUsername);
                try
                {
                    Console.WriteLine(message.Sender);
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
