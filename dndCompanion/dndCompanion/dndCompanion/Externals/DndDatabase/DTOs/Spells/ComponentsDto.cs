using System.Collections.Generic;

namespace dndCompanion.Externals.DndDatabase.DTOs.Spells
{
    public record ComponentsDto
    {
        public bool Verbal { get; init; }
        public bool Somatic { get; init; }
        public bool Material { get; init; }

        public IEnumerable<MaterialComponentDto> MaterialComponents { get; init; }
    }
}
