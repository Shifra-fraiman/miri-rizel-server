using CopyRight.Dto.Models;

namespace CopyRight.Bl.Interfaces
{
    public interface ICustomer : IBlcrud<Customers>
      
    {
        Task<bool> DeletByEmailAsync(string email);
        Task<Customers> GetByIdAsync(int customerId);
        Task<List<StatusCodeUser>> GetCustomerStatusAsync();
        Task<bool> existsEmailAsync(string email);
    }
}
