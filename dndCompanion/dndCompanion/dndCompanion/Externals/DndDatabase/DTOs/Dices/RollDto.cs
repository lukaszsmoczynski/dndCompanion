using System.Collections.Generic;

namespace dndCompanion.Externals.DndDatabase.DTOs.Dices
{
    public record RollDto
    {
        public string Description { get; init; }
        public IDictionary<uint, int> Dices { get; init; }
    }
}
