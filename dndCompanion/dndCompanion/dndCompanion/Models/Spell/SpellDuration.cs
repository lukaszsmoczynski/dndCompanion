using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Spell
{
    public enum SpellDurationUnit
    {
        Instant,
        Round,
        Minute,
        Hour,
        Day,
        Special,
        UntilDispelledOrTriggered
    }

    public class SpellDuration
    {
        public int Amount { get; set; }
        public SpellDurationUnit Unit { get; set; }
    }
}
