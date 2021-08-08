using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndCompanion.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddSpellAsync(T item);
        Task<bool> UpdateSpellAsync(T item);
        Task<bool> DeleteSpellAsync(string id);
        Task<T> GetSpellAsync(string id);
        Task<IEnumerable<T>> GetSpellsAsync(bool forceRefresh = false);
    }
}
