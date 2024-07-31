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
    public class StatusCodeProjectService : IStatusCodeProject
    {

        private CopyRightContext db;
        public StatusCodeProjectService(CopyRightContext db)
        {
            this.db = db;
        }
       
        public async Task<StatusCodeProject> CreateAsync(StatusCodeProject item)
        {
            try
            {
                await db.StatusCodeProjects.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                db.StatusCodeProjects.Remove(item);
                throw new Exception("Failed to create status", ex);
            }
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StatusCodeProject>> ReadAllAsync()
        {
            try
            {
                return await db.StatusCodeProjects
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

        public async Task<List<StatusCodeProject>> ReadAsync(Predicate<StatusCodeProject> filter)
        {
            List<StatusCodeProject> s = await db.StatusCodeProjects.ToListAsync();
            return s.FindAll(filter);
        }

        public Task<bool> UpdateAsync(StatusCodeProject item)
        {
            throw new NotImplementedException();
        }
    }
}
