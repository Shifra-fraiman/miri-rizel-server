using CopyRight.Dal.Models;
using CopyRight.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Bl.Interfaces
{
    public interface ICommunication: IBlcrud<Communications>
    {
        Task<List<Communications>> GetByIdLAsync(int id);

        Task<List<Communications>> GetByIdCAsync(int id);

    }
}
