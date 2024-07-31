using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Bl.Service
{
    public class RelatedCodeService : IRelatedToCode
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public RelatedCodeService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }
        

        public async Task<relatedToCode> CreateAsync(relatedToCode item)
        {
            return mapper.Map<relatedToCode>(await dalManager.realeted.CreateAsync(mapper.Map<Dal.Models.RelatedToCode>(item)));
        }

        public Task<bool> DeleteAsync(int item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<relatedToCode>> ReadAllAsync()=> mapper.Map<List<Dal.Models.RelatedToCode>, List<relatedToCode>>(await dalManager.realeted.ReadAllAsync());


       

        public async Task<List<relatedToCode>> ReadAsync(Predicate<relatedToCode> filter)
        {
            List<relatedToCode> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));

        }

        public Task<bool> UpdateAsync(relatedToCode item)
        {
            throw new NotImplementedException();
        }
    }
}
