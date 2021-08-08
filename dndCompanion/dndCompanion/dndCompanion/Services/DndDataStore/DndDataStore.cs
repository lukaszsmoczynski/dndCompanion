using dndCompanion.Models.Spell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dndCompanion.Services.DndDataStore
{
    class DndDataStore : IDataStore<Spell>
    {
        public List<Spell> Spells { get; private set; }
        private bool loaded = false;

        public DndDataStore()
        {
        }
        public async Task<bool> AddSpellAsync(Spell item)
        {
            Spells.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSpellAsync(string name)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Name == name).FirstOrDefault();
            Spells.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Spell> GetSpellAsync(string name)
        {
            return await Task.FromResult(Spells.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Spell>> GetSpellsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || !loaded)
                Spells = (await LoadSpellsAsync()).ToList<Spell>();
            return await Task.FromResult(Spells);
        }

        public async Task<bool> UpdateSpellAsync(Spell spell)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Name == spell.Name).FirstOrDefault();
            Spells.Remove(oldItem);
            Spells.Add(spell);

            return await Task.FromResult(true);
        }

        private async Task<IEnumerable<Spell>> LoadSpellsAsync()
        {
            loaded = false;

            loaded = true;

            return await Task.FromResult(new List<Spell>());
        }
    }
}
