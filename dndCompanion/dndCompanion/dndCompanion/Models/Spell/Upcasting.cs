using dndCompanion.Models.Dice;
using System.Collections.Generic;

namespace dndCompanion.Models.Spell
{
    public class Upcasting
    {
        public int Level { get; set; }
        public string Description { get; set; }
        public List<Roll> Rolls { get; set; } = new List<Roll>();
    }
}