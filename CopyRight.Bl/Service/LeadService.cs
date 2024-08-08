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
        public async Task<bool> existsEmailAsync(string email)
        {
            try
            {
                return await dalManager.leads.existsEmailAsync(email);
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }
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

        public async Task<Customers> replaceToCustomer(Leads item)
        {
            try
            {
                await dalManager.leads.DeleteAsync(item.LeadId);
                List<Dal.Models.StatusCodeUser> statuses =await dalManager.statusU.ReadAllAsync();
                Dal.Models.StatusCodeUser status= null;
                foreach (var s in statuses)
                {
                    if (s.Description == "Active")
                        status = s;
                }
                if(status == null)
                    status = new Dal.Models.StatusCodeUser();
               
                    Customers c = new Customers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Source = item.Source,
                        BusinessName = item.BusinessName,
                        Phone = item.Phone,
                        Email = item.Email,
                        Status = mapper.Map<Dto.Models.StatusCodeUser>(status),
                        CreatedDate = item.CreatedDate
                    };
                
                

                var newCustomer = mapper.Map<Dal.Models.Customer>(c);
                return mapper.Map<Dto.Models.Customers>(await dalManager.customers.CreateAsync(newCustomer));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
