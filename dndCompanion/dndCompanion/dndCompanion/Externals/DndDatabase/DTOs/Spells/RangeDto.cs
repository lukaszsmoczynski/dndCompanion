namespace dndCompanion.Externals.DndDatabase.DTOs.Spells
{
    public record RangeDto
    {
        public int Amount { get; init; }
        public SpellRangeUnitDto Unit { get; init; }
    }
}
