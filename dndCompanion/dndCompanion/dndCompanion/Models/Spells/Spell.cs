using dndCompanion.Models.Character.Class;
using dndCompanion.Models.Dice;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace dndCompanion.Models.Spells
{
    public class Spell
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public School School { get; set; }
        public CastTime CastTime { get; set; }
        public Range Range { get; set; }
        public Components Components { get; set; }
        public Duration Duration { get; set; }
        public string Description { get; set; }
        public List<Roll> Rolls { get; set; }
        public IEnumerable<string> Classes { get; set; }
        public bool Ritual { get; set; }
        public List<Upcasting> Upcasting { get; set; }
        public ImageSource Image { get; set; }
    }
}
