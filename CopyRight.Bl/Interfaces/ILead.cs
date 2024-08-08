using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Bl.Interfaces
{
    public interface ILead: IBlcrud<Leads>
    {
        Task<Leads> GetByIdAsync(int id);
        Task<Customers> replaceToCustomer(Leads l);
        Task<bool> existsEmailAsync(string email);

    }
}
