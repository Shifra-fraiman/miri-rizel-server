using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dal.Service
{
    public class CommunicationService : ICommunication
    {
        private CopyRightContext db;
        public CommunicationService(CopyRightContext db)
        {
            this.db = db;
        }

        public async Task<Communication> CreateAsync(Communication item)
        {
            try
            {
                await db.Communications.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                db.Communications.Remove(item);
                throw new Exception("Failed to create communication", ex);
            }
        }

        public async Task<bool> DeleteAsync(int item)
        {
            try
            {
                Communication l = await db.Communications.FirstOrDefaultAsync(c => c.CommunicationId == item);
                if (l == null)
                    throw new Exception("lead does not exist in DB");
                db.Communications.Remove(l);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Communication>> ReadAllAsync()
        {
            try
            {
                return await db.Communications
                             .Include(t => t.RelatedToNavigation)
                             //.Include(t => t.RelatedId)
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
        public async Task<List<RelatedToCode>> ReadRealatedToAllAsync()
        {
            try
            {
                return await db.RelatedToCodes.ToListAsync();
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
        public Task<List<Communication>> ReadAsync(Predicate<Communication> filter)
        {
            throw new NotImplementedException();
        }
       

        public Task<bool> UpdateAsync(Communication item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Communication>> GetByIdLAsync(int id)
        {
            try
            {
                return await db.Communications.Where(x => x.RelatedId == id && x.RelatedTo == 2).ToListAsync();
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

        public async Task<List<Communication>> GetByIdCAsync(int id)
        {
            try
            {
                return await db.Communications.Where(x => x.RelatedId == id && x.RelatedTo==1).ToListAsync();
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
    }

}
