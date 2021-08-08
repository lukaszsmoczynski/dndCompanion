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
        public SpellCastTime CastTime { get; set; } = new SpellCastTime();
        public SpellRange Range { get; set; } = new SpellRange();
        public SpellComponents Components { get; set; } = new SpellComponents();
        public SpellDuration Duration { get; set; } = new SpellDuration();
        public string Description { get; set; }
        public List<Roll> Rolls { get; set; } = new List<Roll>();
        public List<ICasterClass> Classes { get; set; } = new List<ICasterClass>();
        public bool Ritual { get; set; }
        public List<Upcasting> Upcasting { get; set; } = new List<Upcasting>();

        //public Spell(string name)
        //{
        //    Name = name;
        //}
    }
}
