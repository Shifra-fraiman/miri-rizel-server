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
    public class RoleCodeService : IRoleCode
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public RoleCodeService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }


        public async Task<RoleCode> CreateAsync(RoleCode item)
        {
            return mapper.Map<RoleCode>(await dalManager.roles.CreateAsync(mapper.Map<Dal.Models.RoleCode>(item)));
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }
        public async Task<List<RoleCode>> ReadAllAsync()=>mapper.Map<List<Dal.Models.RoleCode>, List<RoleCode>>(await dalManager.roles.ReadAllAsync());
       

        public async Task<List<RoleCode>> ReadAsync(Predicate<RoleCode> filter)
        {
            List<RoleCode> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }

        public Task<bool> UpdateAsync(RoleCode item)
        {
            throw new NotImplementedException();
        }
    }
}
