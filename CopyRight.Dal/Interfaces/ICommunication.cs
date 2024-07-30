using CopyRight.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dal.Interfaces
{
    public interface ICommunication: IDalCrud<Communication>
    {
        Task<List<Communication>> GetByIdAsync(int id);
       Task<List<RelatedToCode>> ReadRealatedToAllAsync();

    }
}
