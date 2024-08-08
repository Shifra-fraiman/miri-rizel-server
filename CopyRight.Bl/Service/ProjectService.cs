using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace CopyRight.Bl.Service
{
    public class ProjectService : IProject
    {
        public DalManager _dalManager;
        public Dal.Interfaces.IProject proj;
        readonly IMapper mapper;
        public ProjectService(DalManager dal, Dal.Interfaces.IProject p)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
            this.proj = p;
            this._dalManager = dal;
        }
        public async Task<Projects> CreateAsync(Projects item)
        {
            Project p = new() { CreatedDate = DateTime.Now, ProjectId = item.ProjectId, Name = item.Name, Description = item.Description, StartDate = item.StartDate, EndDate = item.EndDate, Status = item.Status.Id, CustomerId = item.Customer.CustomerId ,IsActive=true,Authorize=item.Authorize};
            return mapper.Map<Dto.Models.Projects>(await proj.CreateAsync(mapper.Map<Dal.Models.Project>(p)));

            var newCustomer = mapper.Map<Dal.Models.Project>(item);
            newCustomer.CreatedDate = DateTime.UtcNow;
            return mapper.Map<Dto.Models.Projects>(await _dalManager.project.CreateAsync(newCustomer));
        }
        public async Task<bool> ReadTaskAuthAsync(int id)
        {


            try
            {
                List<Dal.Models.Project> u = await proj.ReadAsync(o => o.ProjectId == id && o.Authorize == 1);
                Console.WriteLine(u);
                if (u.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                List<Dal.Models.Project> u = await proj.ReadAsync(o => o.ProjectId == id);
                u.First().IsActive = false; 
                return await proj.UpdateAsync(u.First());
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<Projects>> ReadAsync(Predicate<Projects> filter)
        {
            List< Dto.Models.Projects> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }
     
        public async Task<List<Projects>> ReadAllAsync() => 
mapper.Map<List<Dal.Models.Project>, List<Projects>>(await _dalManager.project.ReadAllAsync());
        public async Task<List<Projects>> ReadProjectAsync() =>
mapper.Map<List<Dal.Models.Project>, List<Projects>>(await _dalManager.project.ReadAsync(o => o.Authorize == 1));
        public async Task<bool> UpdateAsync(Projects item) => await proj.UpdateAsync(mapper.Map<Projects, Dal.Models.Project>(item));

       
       
        public async Task<bool> IsOnTheDB(int? id)
        {
            List<Customer> m = await _dalManager.customers.ReadAsync(o => o.CustomerId == id);
            var k = m.FirstOrDefault();
            return m != null;
        }
        public bool IsCorrectDates(DateTime? start, DateTime? end)
        {
            return (start < end);
        }
        public async Task<Projects> GetByIdAsync(int id)
        {
            try
            {
                Dal.Models.Project u = await _dalManager.project.GetByIdAsync(id);
                return mapper.Map<Dal.Models.Project,Projects>(u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

