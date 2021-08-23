using System.Collections.Generic;

namespace dndCompanion.Models.Dice
{
    public class Roll
    {
        public string Description { get; set; }
        public IDictionary<uint, int> Dices { get; set; }
    }
}
