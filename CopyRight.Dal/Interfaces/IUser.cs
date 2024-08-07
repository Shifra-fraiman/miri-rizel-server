using CopyRight.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Dal.Interfaces
{
    public interface IUser:IDalCrud<User>
    {
        Task<User> LogInAsync(string email, string password);
        Task<User> LogInGoogleAsync(string email);

        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<bool> UpdatePassword(string email, string password);
        Task<List<RoleCode>> ReadAllRoleAsync();


    }
}
