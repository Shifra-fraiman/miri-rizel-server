using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CopyRight.Dal.Service
{
    public class TasksService : ITasks
    {
        private CopyRightContext db;
        public TasksService(CopyRightContext db)
        {// קבלתי בהזרקה ושמרתי במאפין
            this.db = db;
        }

        public async Task<CopyRight.Dal.Models.Task> CreateAsync(CopyRight.Dal.Models.Task item)
        {
            try
            {
                await db.Tasks.AddAsync(item);
                await db.SaveChangesAsync();
                return await GetById(item.TaskId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Models.Task>> ReadTaskAsync()
        {
            try
            {

                List<Models.Task> l = await db.Tasks.Where(t => t.Project.Authorize == 1).Include(t => t.AssignedToNavigation).Include(t=>t.Project.Authorize)
                               .Include(t => t.Project)
                               .Include(t => t.PriorityNavigation).ToListAsync();
                if (l.Count > 0)
                {
                    return l;
                }
                return l;

            }
            catch (Exception ex)
            {
                throw new Exception("error-", ex);
            }
        }
        public async Task<bool> ReadTaskAuthAsync(int id)
        {
            {
                try
                {
                    List<Models.Task> item = await ReadAsync(o => o.TaskId == id && o.Project.Authorize == 1);
                    if (item.Count > 0)
                    {
                        return true;
                    }

                    return false;
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


        public async Task<bool> DeleteAsync(int i)
        {
            try
            {
                List<CopyRight.Dal.Models.Task> item = await ReadAsync(o => o.TaskId == i);
                db.Tasks.Remove(item.First());
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<CopyRight.Dal.Models.Task>> ReadAsync(Predicate<CopyRight.Dal.Models.Task> filter)
        {
           
                try
                {
                    List<Models.Task> l = await db.Tasks
                                   .Include(t => t.AssignedToNavigation)
                                   .Include(t => t.Project)
                                   .Include(t => t.StatusNavigation)
                                   .Include(t => t.PriorityNavigation)
                                   .ToListAsync();

                    return l.FindAll(p => filter(p));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

               
            
        }

        public async Task<List<CopyRight.Dal.Models.Task>> ReadAllAsync()
        {
            try
            {
                return await db.Tasks
                             .Include(t => t.AssignedToNavigation)
                             .Include(t => t.Project)
                             .Include(t => t.StatusNavigation)
                             .Include(t => t.PriorityNavigation)
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

        public async Task<bool> UpdateAsync(CopyRight.Dal.Models.Task item)
        {
            try
            {
                var data = db.Tasks.FirstOrDefault(x => x.TaskId == item.TaskId);
                if (data != null)
                {
                    data.Priority = item.Priority;
                    data.Title = item.Title;
                    data.Description = item.Description;
                    data.Status = item.Status;
                    data.ProjectId = item.ProjectId;
                    data.AssignedTo = item.AssignedTo;
                    data.DueDate = item.DueDate;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CopyRight.Dal.Models.Task> GetById(int id)
        {
            try
            {
                return await db.Tasks
                               .Include(t => t.AssignedToNavigation)
                               .Include(t => t.Project)
                               .Include(t => t.StatusNavigation)
                               .Include(t => t.PriorityNavigation)
                               .FirstOrDefaultAsync(x => x.TaskId == id);
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
        public async Task<List<CopyRight.Dal.Models.StatusCodeProject>> ReadAllStatusAsync()
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
        public async Task<List<PriorityCode>> ReadAllPriorityAsync()
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
        public async Task<bool> UpdateGoogleCalendarAsync(int taskId, string googleId)
        {
            try
            {
                var data = db.Tasks.FirstOrDefault(x => x.TaskId == taskId);
                if (data != null)
                {
                    data.GoogleId = googleId;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
