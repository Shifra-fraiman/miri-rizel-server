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
    public class StatusCodeUserService : IStatusCodeUser
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public StatusCodeUserService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }


        public async Task<StatusCodeUser> CreateAsync(StatusCodeUser item)
        {
            return mapper.Map<StatusCodeUser>(await dalManager.statusU.CreateAsync(mapper.Map<Dal.Models.StatusCodeUser>(item)));
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }


        public async Task<List<StatusCodeUser>> ReadAllAsync() => mapper.Map<List<Dal.Models.StatusCodeUser>, List<StatusCodeUser>>(await dalManager.statusU.ReadAllAsync());
      

        public async Task<List<StatusCodeUser>> ReadAsync(Predicate<StatusCodeUser> filter)
        {
            List<StatusCodeUser> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }

        public Task<bool> UpdateAsync(StatusCodeUser item)
        {
            throw new NotImplementedException();
        }
    }
}

