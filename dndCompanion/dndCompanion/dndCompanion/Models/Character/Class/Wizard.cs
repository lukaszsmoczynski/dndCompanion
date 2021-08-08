using dndCompanion.Models.Spell;
using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Character.Class
{
    public class Wizard : ICharacterClass, ICasterClass
    {
        public string Name => "Wizard";
    }
}
