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
    public class RoleCodeService : IRoleCode
    {
        private CopyRightContext db;
        public RoleCodeService(CopyRightContext db)
        {
            this.db = db;
        }
        public async Task<RoleCode> CreateAsync(RoleCode item)
        {
            try
            {
                await db.RoleCodes.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                db.RoleCodes.Remove(item);
                throw new Exception("Failed to create role", ex);
            }
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleCode>> ReadAllAsync()
        {
            try
            {
                return await db.RoleCodes
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

        public async Task<List<RoleCode>> ReadAsync(Predicate<RoleCode> filter)
        {
            List<RoleCode> roles = await db.RoleCodes.ToListAsync();
            return roles.FindAll(filter);
        }

        public Task<bool> UpdateAsync(RoleCode item)
        {
            throw new NotImplementedException();
        }
    }
}
