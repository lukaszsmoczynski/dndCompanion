using dndCompanion.Externals.DndDatabase;
using dndCompanion.Models.Spells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dndCompanion.Services.DndDataStore.Spells
{
    class SpellsDataStore : ISpellsDataStore
    {
        public List<Spell> Spells { get; private set; }
        private bool loaded = false;

        private IDndDatabaseClient DndDatabaseClient => DependencyService.Get<IDndDatabaseClient>();

        public async Task<bool> AddAsync(Spell item)
        {
            Spells.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Id == id).FirstOrDefault();
            Spells.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Spell> GetOneAsync(Guid id)
        {
            return await Task.FromResult(Spells.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Spell>> GetAllAsync(bool forceRefresh = false)
        {
            if (forceRefresh || !loaded)
                await LoadSpellsAsync();

            return await Task.FromResult(Spells);
        }

        public async Task<bool> UpdateAsync(Spell spell)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Name == spell.Name).FirstOrDefault();
            Spells.Remove(oldItem);
            Spells.Add(spell);

            return await Task.FromResult(true);
        }

        private async Task LoadSpellsAsync()
        {
            loaded = false;
            Spells = (await DndDatabaseClient.DownloadSpellsAsync()).ToList();
            loaded = true;

            await Task.CompletedTask;
        }
    }
}
