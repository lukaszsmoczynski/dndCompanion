using dndCompanion.Externals.DndDatabase.DTOs.Dices;
using System;
using System.Collections.Generic;

namespace dndCompanion.Externals.DndDatabase.DTOs.Spells
{
    public record SpellDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }
        public SchoolDto School { get; init; }
        public CastTimeDto CastTime { get; init; }
        public RangeDto Range { get; init; }
        public ComponentsDto Components { get; init; }
        public DurationDto Duration { get; init; }
        public string Description { get; init; }
        public IEnumerable<RollDto> Rolls { get; init; }
        public IEnumerable<string> Classes { get; init; }
        public bool Ritual { get; init; }
        public IEnumerable<UpcastingDto> Upcasting { get; init; }
    }
}
