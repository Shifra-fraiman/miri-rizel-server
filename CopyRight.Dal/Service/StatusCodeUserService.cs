using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dal.Service
{
    public class StatusCodeUserService : IStatusCodeUser
    {

        private CopyRightContext db;
        public StatusCodeUserService(CopyRightContext db)
        {
            this.db = db;
        }
        
        public async Task<StatusCodeUser> CreateAsync(StatusCodeUser item)
        {
            try
            {
                await db.StatusCodeUsers.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                db.StatusCodeUsers.Remove(item);
                throw new Exception("Failed to create role", ex);
            }
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StatusCodeUser>> ReadAllAsync()
        {
            try
            {
                return await db.StatusCodeUsers
                               .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while saving data to the database. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("error-", ex);
            }
        }

        public async Task<List<StatusCodeUser>> ReadAsync(Predicate<StatusCodeUser> filter)
        {
            List<StatusCodeUser> s = await db.StatusCodeUsers.ToListAsync();
            return s.FindAll(filter);
        }

        public Task<bool> UpdateAsync(StatusCodeUser item)
        {
            throw new NotImplementedException();
        }
    }
}
