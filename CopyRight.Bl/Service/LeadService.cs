using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;

namespace CopyRight.Bl.Service
{
    public class LeadService : ILead
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public LeadService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }
        public async Task<Leads> CreateAsync(Leads item)
        {
            return mapper.Map<Leads>(await dalManager.leads.CreateAsync(mapper.Map<Lead>(item)));
        }
        public async Task<bool> DeleteAsync(int item)
        {
            return await dalManager.leads.DeleteAsync(item);
        }
        public async Task<List<Leads>> ReadAsync(Predicate<Leads> filter)
        {
            try
            {
                List<Leads> l = await ReadAllAsync();
                return l.FindAll(l => filter(l));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<List<Leads>> ReadAllAsync() => mapper.Map<List<Lead>, List<Leads>>(await dalManager.leads.ReadAllAsync());
        public async Task<bool> UpdateAsync(Leads item)
        {
            try
            {
                return await dalManager.leads.UpdateAsync(mapper.Map<Leads, Lead>(item));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<Leads> GetByIdAsync(int id)
        {
            try
            {
                Lead u = await dalManager.leads.GetByIdAsync(id);
                return mapper.Map<Leads>(u);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
