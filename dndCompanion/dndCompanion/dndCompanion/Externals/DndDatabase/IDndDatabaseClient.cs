using dndCompanion.Models.Spells;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dndCompanion.Externals.DndDatabase
{
    interface IDndDatabaseClient
    {
        public Task<IEnumerable<Spell>> DownloadSpellsAsync();
    }
}
