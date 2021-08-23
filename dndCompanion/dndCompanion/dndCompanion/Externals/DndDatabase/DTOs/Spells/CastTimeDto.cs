namespace dndCompanion.Externals.DndDatabase.DTOs.Spells
{
    public record CastTimeDto
    {
        public int Amount { get; init; }
        public SpellTimeUnitDto Unit { get; init; }
    }
}
