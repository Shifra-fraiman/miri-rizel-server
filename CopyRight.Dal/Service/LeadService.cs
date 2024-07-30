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
    public class LeadService : ILead
    {
        private CopyRightContext db;
        public LeadService(CopyRightContext db)
        {
            this.db = db;
        }

        public async Task<Lead> CreateAsync(Lead item)
        {
            try
            {
                db.Leads.Add(item);
                await db.SaveChangesAsync();
                await db.SaveChangesAsync();
                return item;
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
                Lead l = await db.Leads.FirstOrDefaultAsync(c => c.LeadId == item);
                if (l == null)
                    throw new Exception("lead does not exist in DB");
                db.Leads.Remove(l);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Lead>> ReadAsync(Predicate<Lead> filter)
        {
            try
            {
                List<Lead> l = await db.Leads.ToListAsync();
                return l.FindAll(p => filter(p));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<Lead>> ReadAllAsync() =>await db.Leads.ToListAsync();

        public async Task<bool> UpdateAsync(Lead item)
        {
            try
            {
                var existingLead = db.Leads.FirstOrDefault(x => x.LeadId == item.LeadId);

                if (existingLead == null)
                    throw new Exception("Lead does not exist in DB");
                existingLead.FirstName = item.FirstName;
                existingLead.LastName = item.LastName;
                existingLead.BusinessName = item.BusinessName;
                existingLead.LastContactedDate = item.LastContactedDate;
                existingLead.Phone = item.Phone;
                existingLead.Email = item.Email;
                existingLead.Source = item.Source;
                existingLead.Notes = item.Notes;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Lead> GetByIdAsync(int id)
        {
            try
            {
                Lead l = await db.Leads.FirstOrDefaultAsync(x => x.LeadId == id);
                if (l == null)
                    throw new Exception("user does not exist in DB");
                else
                    return l;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
