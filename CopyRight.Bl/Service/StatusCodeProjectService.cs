using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Bl.Service
{
    public class StatusCodeProjectService : IStatusCodeProject
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public StatusCodeProjectService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }


        public async Task<StatusCodeProject> CreateAsync(StatusCodeProject item)
        {
            return mapper.Map<StatusCodeProject>(await dalManager.statusP.CreateAsync(mapper.Map<Dal.Models.StatusCodeProject>(item)));
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<StatusCodeProject>> ReadAllAsync() => mapper.Map<List<Dal.Models.StatusCodeProject>, List<StatusCodeProject>>(await dalManager.statusP.ReadAllAsync());
    

        public async Task<List<StatusCodeProject>> ReadAsync(Predicate<StatusCodeProject> filter)
        {
            List<StatusCodeProject> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }

        public Task<bool> UpdateAsync(StatusCodeProject item)
        {
            throw new NotImplementedException();
        }
    }
}
