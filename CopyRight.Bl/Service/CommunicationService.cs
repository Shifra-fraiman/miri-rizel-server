using AutoMapper;
using CopyRight.Bl.Interfaces;
using CopyRight.Dal;
using CopyRight.Dal.Service;
using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using CopyRight.Dal.Interfaces;

namespace CopyRight.Bl.Service
{
    public class CommunicationService : Bl.Interfaces.ICommunication
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public CommunicationService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }
        //public async Task<List<Dto.models.Communications>>convertList(List<Dal.Models.Communication>li)
        //{
        //    List<Dto.models.Communications> big = new List<Dto.models.Communications>();
        //    li.ForEach(o => big.Add(Convert(o)));
        //    return big; 
        //}
        //public async Task<Dto.models.Communications> Convert(Dal.Models.Communication c)
        //{
        //    string name;
        //    if (c.RelatedId == 1)
        //    {
        //        List<Dal.Models.Customer> ct = await dalManager.customers.ReadAsync(o => o.CustomerId == c.RelatedId);
        //        Dal.Models.Customer customer = ct[0];
        //         name=customer.FirstName;   
        //    }
        //    else
        //    { Dal.Models.Lead l = await dalManager.leads.GetByIdAsync(c.RelatedId);
        //        name =l.FirstName;
        //    }
        //    Dto.models.Communications new1= new Dto.models.Communications() {
        //        Date=c.Date,
        //        Details=c.Details,
        //        RelatedId=c.RelatedId,      
        //        RelatedTo=c.RelatedTo,
        //        Type=c.Type,    
        //        name=name,
        //        CommunicationId =c.CommunicationId};
        //    return new1;
        //}
        public async Task<List<Communications>> ConvertList(Task<List<Dal.Models.Communication>> liTask)
        {
            List<Communication> li = await liTask;
            List<Communications> big = new List<Communications>();
            foreach (var item in li)
            {
                var convertedItem = await Convert(item);
                big.Add(convertedItem);
            }
            return big;
        }
        public async Task<Communications> Convert(Dal.Models.Communication c)
        {
            string name;
            if (c.RelatedTo == 1)
            {
                List<Dal.Models.Customer> ct = await dalManager.customers.ReadAsync(o => o.CustomerId == c.RelatedId);
                Dal.Models.Customer customer = ct[0];
                name = customer.FirstName+" " +customer.LastName;
            }
            else
            {
                Dal.Models.Lead l = await dalManager.leads.GetByIdAsync(c.RelatedId);
                name = l.FirstName + " " + l.LastName;
            }
            List<Dal.Models.RelatedToCode> k = await dalManager.communications.ReadRealatedToAllAsync();
            Dal.Models.RelatedToCode rtc=k.Find(i=>i.Id== c.RelatedTo);
            relatedToCode newrtc= mapper.Map<Dal.Models.RelatedToCode, relatedToCode>(rtc);
            Communications new1 = new Communications()
            {
                Date = c.Date,
                Details = c.Details,
                RelatedId = c.RelatedId,
                RelatedTo = newrtc,
                Type = c.Type,
                name = name,
                CommunicationId = c.CommunicationId
            };
            return new1;
        }
        public async Task<Communications> CreateAsync(Communications item)
        {
            try

            {
                Dal.Models.Communication d=new Dal.Models.Communication() {CommunicationId=item.CommunicationId,RelatedTo=item.RelatedTo.Id,RelatedId=item.RelatedId,Date=item.Date,Type=item.Type,Details=item.Details };
                //Dal.Models.Communication newTask = mapper.Map<Communications, Dal.Models.Communication>(item);
                return mapper.Map<Dal.Models.Communication, Communications>(await dalManager.communications.CreateAsync(d));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteAsync(int item)
        {
            return await dalManager.communications.DeleteAsync(item);
        }

        public async Task<List<Communications>> ReadAllAsync()
        {

            //return mapper.Map<List<Dal.Models.Communication>, List<Communications>>(await dalManager.communications.ReadAllAsync());
            return await ConvertList(dalManager.communications.ReadAllAsync());   
        }

        public Task<List<Communications>> ReadAsync(Predicate<Communications> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Communications item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Communications>> GetByIdAsync(int id)
        {
            return mapper.Map<List<Dal.Models.Communication>, List<Communications>>(await dalManager.communications.GetByIdAsync(id));
        }

      

    }
}
