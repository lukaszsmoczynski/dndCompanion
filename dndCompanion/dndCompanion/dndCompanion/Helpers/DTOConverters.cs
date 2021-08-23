using dndCompanion.Externals.DndDatabase.DTOs.Dices;
using dndCompanion.Externals.DndDatabase.DTOs.Spells;
using dndCompanion.Models.Dice;
using dndCompanion.Models.Spells;
using System.Linq;

namespace dndCompanion.Helpers
{
    public static class DTOConverters
    {
        public static Spell AsModel(this SpellDto spellDto)
        {
            return new Spell
            {
                Id = spellDto.Id,
                Name = spellDto.Name,
                Level = spellDto.Level,
                School = (School)spellDto.School,
                CastTime = spellDto.CastTime?.AsModel(),
                Range = spellDto.Range?.AsModel(),
                Components = spellDto.Components?.AsModel(),
                Duration = spellDto.Duration.AsModel(),
                Description = spellDto.Description,
                Rolls = spellDto.Rolls?.Select(roll => roll.AsModel()).ToList(),
                Classes = spellDto.Classes,
                Ritual = spellDto.Ritual,
                Upcasting = spellDto.Upcasting?.Select(upcasting => upcasting.AsModel()).ToList()
            };
        }
        public static CastTime AsModel(this CastTimeDto castTimeDto)
        {
            return new CastTime
            {
                Amount = castTimeDto.Amount,
                Unit = (CastTimeUnit)castTimeDto.Unit
            };
        }
        public static Range AsModel(this RangeDto rangeDto)
        {
            return new Range
            {
                Amount = rangeDto.Amount,
                Unit = (RangeUnit)rangeDto.Unit
            };
        }
        public static Roll AsModel(this RollDto rollDto)
        {
            return new Roll
            {
                Dices = rollDto.Dices,
                Description = rollDto.Description
            };
        }
        public static Upcasting AsModel(this UpcastingDto upcastingDto)
        {
            return new Upcasting
            {
                Level = upcastingDto.Level,
                Rolls = upcastingDto.Rolls?.Select(roll => roll.AsModel())
            };
        }
        //public static ClassDto AsModel(this Class characterClass)
        //{
        //    return new ClassDto
        //    {
        //        Id = characterClass.Id,
        //        Name = characterClass.Name,
        //        Description = characterClass.Description,
        //        //SubClass = characterClass.SubClass?.AsDto()
        //    };
        //}
        //public static CasterClassDto AsModel(this CasterClass casterClass)
        //{
        //    return new CasterClassDto
        //    {
        //        Id = casterClass.Id,
        //        Name = casterClass.Name,
        //        Description = casterClass.Description,
        //        //SubClass = casterClass.SubClass?.AsDto(),
        //    };
        //}
        //public static SubClassDto AsModel(this SubClass subClass)
        //{
        //    return new SubClassDto
        //    {
        //        Id = subClass.Id,
        //        Name = subClass.Name,
        //        Description = subClass.Description,
        //        //SubClass = subClass.SubClass?.AsDto()
        //    };
        //}
        //public static CasterSubClassDto AsModel(this CasterSubClass casterSubClass)
        //{
        //    return new CasterSubClassDto
        //    {
        //        Id = casterSubClass.Id,
        //        Name = casterSubClass.Name,
        //        Description = casterSubClass.Description,
        //        //SubClass = casterSubClass.SubClass?.AsDto(),
        //    };
        //}
        public static Duration AsModel(this DurationDto durationDto)
        {
            return new Duration
            {
                Amount = durationDto.Amount,
                Concentration = durationDto.Concentration,
                Unit = (DurationUnit)durationDto.Unit,
                UpTo = durationDto.UpTo
            };
        }
        public static Components AsModel(this ComponentsDto componentsDto)
        {
            return new Components
            {
                Verbal = componentsDto.Verbal,
                Somatic = componentsDto.Somatic,
                Material = componentsDto.Material,
                MaterialComponents = componentsDto.MaterialComponents?.Select(materialComponent => materialComponent.AsModel())
            };
        }
        public static MaterialComponent AsModel(this MaterialComponentDto materialComponentDto)
        {
            return new MaterialComponent
            {
                Name = materialComponentDto.Name,
                Value = materialComponentDto.Value,
                Consumed = materialComponentDto.Consumed,
                Description = materialComponentDto.Description
            };
        }
    }
}
