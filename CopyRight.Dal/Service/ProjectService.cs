using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CopyRight.Dal.Service
{
    public class ProjectService : IProject
    {
        private CopyRightContext db;
        public ProjectService(CopyRightContext db)
        {// קבלתי בהזרקה ושמרתי במאפין
            this.db = db;
        }
        public async Task<Project> CreateAsync(Project item)
        {
            try
            {
                item.StartDate = EnsureUtc(item.StartDate);
                item.EndDate = EnsureUtc(item.EndDate);
                item.CreatedDate = EnsureUtc(item.CreatedDate);
                item.IsActive=true;
                await db.Projects.AddAsync(item);

                await db.SaveChangesAsync();
                return item;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        private DateTime? EnsureUtc(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                if (dateTime.Value.Kind == DateTimeKind.Unspecified)
                {
                    return DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
                }
                return dateTime.Value.ToUniversalTime();
            }
            return null;
        }
        public async Task<bool> DeleteAsync(int i)
        {
            try
            {
                List<Project> item = await ReadAsync(o => o.ProjectId == i);
                db.Projects.Remove(item.First());
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Project>> ReadAsync(Predicate<Project> filter)
        {
            List<Project> projects = await ReadAllAsync();

            return projects.FindAll(p => filter(p));
        }

        public async Task<List<Project>> ReadAllAsync()
        {
            try
            {
                return await db.Projects
                             .Include(t => t.Customer).Include(p => p.Customer.StatusNavigation)
                             .Include(t => t.StatusNavigation).Include(o=>o.AuthorizeNavigation)
                             .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while saving data to the database. Please try again later.", ex);
            }
        }
        public async Task<List<Project>> ReadProjectAsync()
        {
            try
            {

                return await db.Projects
                            .Include(t => t.Customer).Include(p => p.Customer.StatusNavigation)
                            .Include(t => t.StatusNavigation).Include(o => o.Authorize)
                            .Where(x => x.IsActive == true && x.Authorize == 1).ToListAsync();

            }
          
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while take data to the database. Please try again later.", ex);
            }
        }
        public async Task<bool> ReadTaskAuthAsync(int id)
        {
            try
            {
                Project item = await db.Projects.Include(t => t.Customer).Include(t => t.Status).FirstOrDefaultAsync(t => t.ProjectId == id && t.Authorize == 1);


                if (item != null)
                {
                    return true;

                }
                else if (item == null)
                {


                    return false;
                }
            }
            catch (Exception ex)
            {
                {

                    throw new Exception(ex.Message);
                }

            }
            return false;
        }


        public async Task<bool> UpdateAsync(Project item)
        {
            try
            {
                List<Project> projects = await db.Projects.ToListAsync();
                int index = projects.FindIndex(x => x.ProjectId == item.ProjectId);
                if (index == -1)
                    throw new Exception("user does not exist in DB");
                var existingproject = db.Projects.FirstOrDefault(x => x.ProjectId == item.ProjectId);
                existingproject.Name = item.Name;
                existingproject.Status = item.Status;
                existingproject.Tasks = item.Tasks;
                existingproject.StartDate = item.StartDate;
                existingproject.EndDate = item.EndDate;
                existingproject.Description = item.Description;
                existingproject.CreatedDate = item.CreatedDate;
                existingproject.CustomerId = item.CustomerId;
                existingproject.IsActive = item.IsActive;   
                List<Project> projectss = await db.Projects.ToListAsync();
                projectss[index] = existingproject;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<Project> GetByIdAsync(int id)
        {
            try
            {
                Project p = await db.Projects.Include(t => t.Customer).Include(p => p.Customer.StatusNavigation)
                             .Include(t => t.StatusNavigation).FirstOrDefaultAsync(x => x.ProjectId == id);
                if (p == null)
                    throw new Exception("project does not exist in DB");
                else
                    return p;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
