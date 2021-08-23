using dndCompanion.Models.Character.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dndCompanion.Services.DndDataStore.Character
{
    class MockClassesDataStore : IDataStore<CharacterClass>
    {
        private class DummyCharacterClass : CharacterClass
        {

            public override string Name
            {
                get => DummyName;
            }
            public string DummyName { get; set; }
        }

        private readonly string fileName = "classes.xml";
        public List<CharacterClass> CharacterClasses { get; private set; }
        private bool loaded = false;

        public MockClassesDataStore()
        {
        }
        public async Task<bool> AddAsync(CharacterClass item)
        {
            CharacterClasses.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var oldItem = CharacterClasses
                //.Where((CharacterClass arg) => arg.Id == id)
                .FirstOrDefault();
            CharacterClasses.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<CharacterClass> GetOneAsync(Guid id)
        {
            return await Task.FromResult(new DummyCharacterClass() { DummyName = id.ToString() });
        }

        public async Task<IEnumerable<CharacterClass>> GetAllAsync(bool forceRefresh = false)
        {
            if (forceRefresh || !loaded)
                CharacterClasses = (await LoadCharacterClassesAsync()).ToList();
            return await Task.FromResult(CharacterClasses);
        }

        public async Task<bool> UpdateAsync(CharacterClass spell)
        {
            var oldItem = CharacterClasses.Where((CharacterClass arg) => arg.Name == spell.Name).FirstOrDefault();
            CharacterClasses.Remove(oldItem);
            CharacterClasses.Add(spell);

            return await Task.FromResult(true);
        }

        private async Task<IEnumerable<CharacterClass>> LoadCharacterClassesAsync()
        {
            return File.Exists(fileName) ? await LoadClassesFromFileAsync() : await LoadClassesSynteticAsync();
        }
        private async Task<IEnumerable<CharacterClass>> LoadClassesFromFileAsync()
        {
            loaded = false;

            var result = new List<CharacterClass>();

            loaded = true;

            return await Task.FromResult(result);
        }

        private async Task<IEnumerable<CharacterClass>> LoadClassesSynteticAsync()
        {
            loaded = false;

            var result = new List<CharacterClass>();

            for (var i = 0; i <= 10; i++)
            {
                result.Add(new DummyCharacterClass(){ DummyName = string.Format("Class [{0}]", i) });
            }
            result.AddRange(new[] {
                new DummyCharacterClass() { DummyName = "Artificer" },
                new DummyCharacterClass() { DummyName = "Barbarian" },
                new DummyCharacterClass() { DummyName = "Bard" },
                new DummyCharacterClass() { DummyName = "Cleric" },
                new DummyCharacterClass() { DummyName = "Druid" },
                new DummyCharacterClass() { DummyName = "Fighter" },
                new DummyCharacterClass() { DummyName = "Monk" },
                new DummyCharacterClass() { DummyName = "Mystic" },
                new DummyCharacterClass() { DummyName = "Paladin" },
                new DummyCharacterClass() { DummyName = "Ranger" },
                new DummyCharacterClass() { DummyName = "Rogue" },
                new DummyCharacterClass() { DummyName = "Sorcerer" },
                new DummyCharacterClass() { DummyName = "Warlock" },
                new DummyCharacterClass() { DummyName = "Wizard" }
            });

            loaded = true;

            return await Task.FromResult(result);
        }
    }
}
