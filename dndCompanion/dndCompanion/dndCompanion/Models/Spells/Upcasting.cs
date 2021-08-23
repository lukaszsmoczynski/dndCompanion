﻿using dndCompanion.Models.Dice;
using System.Collections.Generic;

namespace dndCompanion.Models.Spells
{
    public class Upcasting
    {
        public int Level { get; set; }
        public string Description { get; set; }
        public IEnumerable<Roll> Rolls { get; set; }
    }
}