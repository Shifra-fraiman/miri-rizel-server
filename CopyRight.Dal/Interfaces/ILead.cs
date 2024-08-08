using CopyRight.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dal.Interfaces
{
    public interface ILead: IDalCrud<Lead>
    {
        Task<Lead> GetByIdAsync(int id);
        Task<bool> existsEmailAsync(string email);

    }
}
