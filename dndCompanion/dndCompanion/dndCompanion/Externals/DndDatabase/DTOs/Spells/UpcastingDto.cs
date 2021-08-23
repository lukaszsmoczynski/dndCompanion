using dndCompanion.Externals.DndDatabase.DTOs.Dices;
using System.Collections.Generic;

namespace dndCompanion.Externals.DndDatabase.DTOs.Spells
{
    public record UpcastingDto
    {
        public int Level { get; init; }
        public string Description { get; init; }
        public IEnumerable<RollDto> Rolls { get; init; }
    }
}