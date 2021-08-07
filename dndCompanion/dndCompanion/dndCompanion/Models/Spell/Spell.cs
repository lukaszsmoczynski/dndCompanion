using dndCompanion.Models.Character;
using dndCompanion.Models.Dice;
using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Spell
{
    public class Spell
    {
        public bool Show { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public SpellSchool School { get; set; }
        public SpellCastTime CastTime { get; set; }
        public SpellRange Range { get; set; }
        public SpellComponents Components { get; set; }
        public SpellDuration Duration { get; set; }
        public string Description { get; set; }
        public List<Roll> Rolls { get; set; }
        public List<CasterClass> Classes { get; set; }
        public bool Ritual { get; set; }
        public List<Upcasting> Upcasting { get; set; }

        //public Spell(string name)
        //{
        //    Name = name;
        //}
    }
}
