using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Spell
{
    public enum SpellRangeUnit
    {
        Self,
        Touch,
        Foot,
        Mile,
        Sight,
        Special,
        Unlimited
    }

    public class SpellRange
    {
        public int Amount { get; set; }
        public SpellRangeUnit Unit { get; set; }
    }
}
