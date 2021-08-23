using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndCompanion.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddAsync(T element);
        Task<bool> UpdateAsync(T element);
        Task<bool> DeleteAsync(Guid id);
        Task<T> GetOneAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false);
    }
}
