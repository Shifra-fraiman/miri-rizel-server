using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyRight.Bl.Interfaces
{
    public interface IBlcrud<T>
    {
        Task<T> CreateAsync(T item);
        Task<bool> DeleteAsync(int item);
        Task<List<T>> ReadAllAsync();
        Task<List<T>> ReadAsync(Predicate<T> filter);
        Task<bool> UpdateAsync(T item);

    }
}
