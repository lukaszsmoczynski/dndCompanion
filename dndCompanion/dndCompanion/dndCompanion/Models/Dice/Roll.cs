using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Dice
{
    public class Roll
    {
        public string Description { get; set; }
        public Dictionary<Dice, int> Dices { get; set; }
    }
}
