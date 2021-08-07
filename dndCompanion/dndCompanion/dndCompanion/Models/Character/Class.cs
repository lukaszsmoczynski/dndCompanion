using System;
using System.Collections.Generic;
using System.Text;

namespace dndCompanion.Models.Character
{
    public enum Class
    {
        Barbarian,
        Bard,
        Cleric,
        Druid,
        Fighter,
        Monk,
        Paladin,
        Ranger,
        Rogue,
        Sorcerer,
        Warlock,
        Wizard
    }
    public enum CasterClass
    {
        Bard = Class.Bard,
        Cleric = Class.Cleric,
        Druid = Class.Druid,
        Paladin = Class.Paladin,
        Ranger = Class.Ranger,
        Sorcerer = Class.Sorcerer,
        Warlock = Class.Warlock,
        Wizard = Class.Wizard
    }
}
