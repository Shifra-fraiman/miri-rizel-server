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
    public class RelatedCodeService : IRealatedToCode
    {
        private CopyRightContext db;
        public RelatedCodeService(CopyRightContext db)
        {
            this.db = db;
        }
        
        public async Task<RelatedToCode> CreateAsync(RelatedToCode item)
        {
            try
            {
                await db.RelatedToCodes.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                db.RelatedToCodes.Remove(item);
                throw new Exception("Failed to create role", ex);
            }
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RelatedToCode>> ReadAllAsync()
        {
            try
            {
                return await db.RelatedToCodes
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

        public async Task<List<RelatedToCode>> ReadAsync(Predicate<RelatedToCode> filter)
        {
            List<RelatedToCode> roles = await db.RelatedToCodes.ToListAsync();
            return roles.FindAll(filter);
        }

        public Task<bool> UpdateAsync(RelatedToCode item)
        {
            throw new NotImplementedException();
        }
    }
}
