using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopyRight.Dal;

namespace CopyRight.Bl.Service
{
    public class TasksService :ITasks
    {
        public Dal.Interfaces.ITasks MyTask;

        private DalManager dalManager;

        readonly IMapper mapper;
        public TasksService( DalManager dalManager, Dal.Interfaces.ITasks MyTask)
        {
            this.dalManager = dalManager;
            this.MyTask = MyTask;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }

        public async Task<Tasks> CreateAsync(Tasks item)
        {
            try
            {
                Dal.Models.Task newTask = mapper.Map<Tasks, Dal.Models.Task>(item);
                return mapper.Map<Dal.Models.Task, Tasks>(await dalManager.tasks.CreateAsync(newTask));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {

                List<Dal.Models.Task> u = await MyTask.ReadAsync(o => o.TaskId == id);
                return await MyTask.DeleteAsync(u.First().TaskId);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<Tasks>> ReadAsync(Predicate<Tasks> filter)
        {
            List<Tasks> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }
        public async Task<List<Tasks>> ReadAllAsync()
        {
            return mapper.Map<List<Dal.Models.Task>, List<Tasks>>(await dalManager.tasks.ReadAllAsync());
        }


        public async Task<bool> UpdateAsync(Tasks item)
        {

            var mappedTask = mapper.Map<Tasks, Dal.Models.Task>(item);
            return await MyTask.UpdateAsync(mappedTask);
        }

        public async Task<Tasks> GetById(int id)
        {
            return mapper.Map<Dal.Models.Task, Dto.Models.Tasks>(await dalManager.tasks.GetById(id));
        }
        public async Task<List<Dto.Models.StatusCodeProject>> ReadAllStatusAsync()
        {
            return mapper.Map<List<Dal.Models.StatusCodeProject>, List<Dto.Models.StatusCodeProject>>(await dalManager.tasks.ReadAllStatusAsync());
        }
        public async Task<List<Dto.Models.PriorityCode>> ReadAllPriorityAsync()
        {
            return mapper.Map<List<Dal.Models.PriorityCode>, List<Dto.Models.PriorityCode>>(await dalManager.tasks.ReadAllPriorityAsync());
        }

        public async Task<bool> UpdateGoogleCalendarAsync(int taskId, string googleId)
        {
            return await dalManager.tasks.UpdateGoogleCalendarAsync(taskId, googleId);
        }


    }
}
