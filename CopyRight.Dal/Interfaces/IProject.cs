
using CopyRight.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dal.Interfaces
{
    public interface IProject :IDalCrud<Project>
    {
        Task<Project> GetByIdAsync(int id);
    }
}
