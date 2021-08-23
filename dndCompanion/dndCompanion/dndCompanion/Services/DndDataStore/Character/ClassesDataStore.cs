using dndCompanion.Models.Character.Class;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dndCompanion.Services.DndDataStore.Character
{
    class ClassesDataStore : IDataStore<CharacterClass>
    {
        public Task<bool> AddAsync(CharacterClass element)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CharacterClass>> GetAllAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<CharacterClass> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CharacterClass element)
        {
            throw new NotImplementedException();
        }
    }
}
