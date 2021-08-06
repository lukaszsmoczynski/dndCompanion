using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Spell
{
    public enum SpellTimeUnit
    {
        BonusAction,
        Reaction,
        Action,
        Round,
        Minute,
        Hour
    }

    public class SpellCastTime
    {
        public int Amount { get; set; }
        public SpellTimeUnit Unit { get; set; }
    }
}
