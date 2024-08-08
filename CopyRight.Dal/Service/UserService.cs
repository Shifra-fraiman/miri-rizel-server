using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CopyRight.Dal.Service
{
    public class UserService : IUser
    {
        private CopyRightContext db;
        public UserService(CopyRightContext db)
        {
            this.db = db;
        }

        public async Task<User> LogInAsync(string email, string password)
        {
            User userFound = await db.Users.Include(t => t.RoleNavigation).FirstOrDefaultAsync(user => user.Email == email);
            try
            {
                //if (userFound != null && BCrypt.Net.BCrypt.Verify(password, userFound.Password))
                if (userFound != null)
                {
                    return userFound;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during login: {ex.Message}");
            }
        }

        public async Task<User> LogInGoogleAsync(string email)
        {
            User userFound = await db.Users.Include(t => t.RoleNavigation).FirstOrDefaultAsync(user => user.Email == email);
            try
            {
                
               
                if (userFound != null)
               

               
                        return userFound;
                    
                    else
                    {
                        return null;
                    }
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> CreateAsync(User item)
        {
            
            try

            {
                item.IsActive = true;   
                Console.WriteLine($"item: {item.UserId} {item.FirstName} {item.Email} {item.Role} {item.CreatedDate}");
               
                await db.Users.AddAsync(item);
               await db.SaveChangesAsync();
               return item;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int item)
        {
            try
            {
                User user = db.Users.FirstOrDefault(c => c.UserId == item);
                if (user == null)
                    throw new Exception("customer does not exist in DB");
               // db.Users.Remove(user);
               //await db.SaveChangesAsync();
               user.IsActive = false;   
                return await UpdateAsync(user);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> ReadAsync(Predicate<User> filter)
        {
            try
            {
                var list = await db.Users.ToListAsync();
                return list.FindAll(p => filter(p));
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        

        public async Task<List<User>> ReadAllAsync()=> await db.Users.Include(t => t.RoleNavigation).ToListAsync();

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                User user = await db.Users.Include(t => t.RoleNavigation).FirstOrDefaultAsync(x => x.UserId == id);
                if (user == null)
                    throw new Exception("user does not exist in DB");
                else
                    return user;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                User user = await db.Users.Include(t => t.RoleNavigation).FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                    throw new Exception("user does not exist in DB");
                else
                    return user;
            }
            catch( Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> UpdateAsync(User item)
        {
            try
            {
                var existingLead = await db.Users.Include(t => t.RoleNavigation).FirstOrDefaultAsync(x => x.UserId == item.UserId);
                if (existingLead == null)
                    throw new Exception("Lead does not exist in DB");
                //existingLead.Communications= item.Communications;
                existingLead.Email= item.Email;
                existingLead.FirstName= item.FirstName;
                existingLead.LastName= item.LastName;
                existingLead.CreatedDate= item.CreatedDate;
                existingLead.UserId= item.UserId;
                existingLead.Role= item.Role;
                existingLead.Tasks= item.Tasks;
                existingLead.Password= item.Password;
                existingLead.IsActive= item.IsActive;   
                db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> UpdatePassword(string email, string password)
        {
            try
            {
                var user = await db.Users.Include(t => t.RoleNavigation).FirstOrDefaultAsync(x => x.Email == email);
                if (user != null)
                {
                    user.Password = password;
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RoleCode>> ReadAllRoleAsync()
        {
            try
            {
                return await db.RoleCodes
                               .ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("error: ", ex);

            }
        }
    }
}




