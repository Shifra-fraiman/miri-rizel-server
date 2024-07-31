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
    public class PriorityCodeService : IPriorityCode
    {
        private CopyRightContext db;
        public PriorityCodeService(CopyRightContext db)
        {
            this.db = db;
        }
        
        public async Task<PriorityCode> CreateAsync(PriorityCode item)
        {
            try
            {
                await db.PriorityCodes.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                db.PriorityCodes.Remove(item);
                throw new Exception("Failed to create role", ex);
            }
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriorityCode>> ReadAllAsync()
        {
            try
            {
                return await db.PriorityCodes
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

        public async Task<List<PriorityCode>> ReadAsync(Predicate<PriorityCode> filter)
        {
            List<PriorityCode> p = await db.PriorityCodes.ToListAsync();
            return p.FindAll(filter);
        }

        public Task<bool> UpdateAsync(PriorityCode item)
        {
            throw new NotImplementedException();
        }
    }
}
