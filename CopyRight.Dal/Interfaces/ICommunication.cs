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
        Task<List<Communication>> GetByIdLAsync(int id);
        Task<List<Communication>> GetByIdCAsync(int id);
        Task<List<RelatedToCode>> ReadRealatedToAllAsync();
        Task<bool> UpdateCommunicationAsync(int id, int code, int relatedId);

    }
}
