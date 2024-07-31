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
    public class PriorityCodeService : IPriorityCode
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public PriorityCodeService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }
        public async Task<PriorityCode> CreateAsync(PriorityCode item)
        {
            return mapper.Map<PriorityCode>(await dalManager.priorities.CreateAsync(mapper.Map<Dal.Models.PriorityCode>(item)));
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriorityCode>> ReadAllAsync()=> mapper.Map<List<Dal.Models.PriorityCode>, List<PriorityCode>>(await dalManager.priorities.ReadAllAsync());
      

        public async Task<List<PriorityCode>> ReadAsync(Predicate<PriorityCode> filter)
        {
            List<PriorityCode> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }

        public Task<bool> UpdateAsync(PriorityCode item)
        {
            throw new NotImplementedException();
        }
    }
}
