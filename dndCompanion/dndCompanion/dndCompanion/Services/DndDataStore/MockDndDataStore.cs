using dndCompanion.Models.Spell;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using dndCompanion.Models.Character;
using dndCompanion.Models.Character.Class;
using dndCompanion.Models.Character.Class.Subclass.Fighter;
using dndCompanion.Models.Character.Class.Subclass.Rogue;
using dndCompanion.Models.Character.Class.Subclass.Cleric;
using dndCompanion.Models.Character.Class.Subclass.Paladin;
using dndCompanion.Models.Character.Class.Subclass.Warlock;
using dndCompanion.Models.Character.Class.Subclass.Druid.CircleOfTheLand2;
using System.IO;

namespace dndCompanion.Services.DndDataStore
{
    class MockDndDataStore : IDataStore<Spell>
    {
        private readonly string fileName = "spells.xml";
        public List<Spell> Spells { get; private set; }
        private bool loaded = false;

        public MockDndDataStore()
        {
        }
        public async Task<bool> AddSpellAsync(Spell item)
        {
            Spells.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteSpellAsync(string name)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Name == name).FirstOrDefault();
            Spells.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Spell> GetSpellAsync(string name)
        {
            return await Task.FromResult(Spells.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Spell>> GetSpellsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || !loaded)
                Spells = (await LoadSpellsAsync()).ToList<Spell>();
            return await Task.FromResult(Spells);
        }

        public async Task<bool> UpdateSpellAsync(Spell spell)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Name == spell.Name).FirstOrDefault();
            Spells.Remove(oldItem);
            Spells.Add(spell);

            return await Task.FromResult(true);
        }

        private async Task<IEnumerable<Spell>> LoadSpellsAsync()
        {
            return File.Exists(fileName) ? await LoadSpellsFromFileAsync() : await LoadSpellsSynteticAsync();
        }
        private async Task<IEnumerable<Spell>> LoadSpellsFromFileAsync()
        {
            loaded = false;

            var result = new List<Spell>();

            var reader = XmlReader.Create(fileName);
            try
            {
                var c = new List<string>();

                Spell spell = null;
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        string value;
                        string[] tokens;

                        switch (reader.Name.ToString())
                        {
                            case "spell":
                                if (spell != null)
                                {
                                    result.Add(spell);
                                }

                                spell = new Spell();
                                break;
                            case "name":
                                spell.Name = reader.ReadString();
                                break;
                            case "level":
                                spell.Level = Int32.Parse(reader.ReadString());
                                break;

                            case "school":
                                switch (reader.ReadString())
                                {
                                    case "A": spell.School = SpellSchool.Abjuration; break;
                                    case "C": spell.School = SpellSchool.Conjuration; break;
                                    case "D": spell.School = SpellSchool.Divination; break;
                                    case "EN": spell.School = SpellSchool.Enchantment; break;
                                    case "EV": spell.School = SpellSchool.Evocation; break;
                                    case "I": spell.School = SpellSchool.Illusion; break;
                                    case "N": spell.School = SpellSchool.Necromancy; break;
                                    case "T": spell.School = SpellSchool.Transmutation; break;
                                }
                                break;
                            case "time":
                                value = reader.ReadString();

                                tokens = value.Split(' ');
                                spell.CastTime.Amount = Int32.Parse(tokens[0]);
                                switch (tokens[1])
                                {
                                    case "bonus": spell.CastTime.Unit = SpellTimeUnit.BonusAction; break;
                                    case "reaction": spell.CastTime.Unit = SpellTimeUnit.Reaction; break;
                                    case "action": spell.CastTime.Unit = SpellTimeUnit.Action; break;
                                    case "round": spell.CastTime.Unit = SpellTimeUnit.Round; break;
                                    case "minute": spell.CastTime.Unit = SpellTimeUnit.Minute; break;
                                    case "hour": spell.CastTime.Unit = SpellTimeUnit.Hour; break;
                                }
                                break;
                            case "range":
                                value = reader.ReadString();
                                if (value.StartsWith("Self"))
                                {
                                    spell.Range.Unit = SpellRangeUnit.Self;
                                    break;
                                }
                                else if (value.StartsWith("Touch"))
                                {
                                    spell.Range.Unit = SpellRangeUnit.Touch;
                                    break;
                                }
                                else if (value.StartsWith("Sight"))
                                {
                                    spell.Range.Unit = SpellRangeUnit.Sight;
                                    break;
                                }
                                else if (value.StartsWith("Special"))
                                {
                                    spell.Range.Unit = SpellRangeUnit.Special;
                                    break;
                                }
                                else if (value.StartsWith("Unlimited"))
                                {
                                    spell.Range.Unit = SpellRangeUnit.Special;
                                    break;
                                }

                                tokens = value.Split(' ');
                                spell.Range.Amount = Int32.Parse(tokens[0]);
                                switch (tokens[1])
                                {
                                    case "feet": spell.Range.Unit = SpellRangeUnit.Foot; break;
                                    case "mile":
                                    case "miles":
                                        spell.Range.Unit = SpellRangeUnit.Mile;
                                        break;
                                }
                                break;
                            case "components":
                                value = reader.ReadString();
                                tokens = value.Split('(');
                                if (tokens.Length > 1)
                                    spell.Components.MaterialComponents.Add(new MaterialComponent() { Name = tokens[1].TrimEnd(')') });

                                tokens = tokens[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                spell.Components.Verbal = tokens.Contains("V");
                                spell.Components.Somatic = tokens.Contains("S");
                                spell.Components.Material = tokens.Contains("M");
                                break;
                            //                             < duration > Concentration, up to 1 hour </ duration >
                            case "classes":
                                value = reader.ReadString();

                                tokens = value.Split(',');
                                foreach (var _class in tokens.Select(p => p.Trim()))
                                {
                                    switch(_class)
                                    {
                                        case "Artificer": spell.Classes.Add(new Artificer()); break;
                                        case "Bard": spell.Classes.Add(new Bard()); break;
                                        case "Cleric": spell.Classes.Add(new Cleric()); break;
                                        case "Cleric (Arcana)": spell.Classes.Add(new ArcanaDomain()); break;
                                        case "Cleric (Death)": spell.Classes.Add(new DeathDomain()); break;
                                        case "Cleric (Knowledge)": spell.Classes.Add(new KnowledgeDomain()); break;
                                        case "Cleric (Life)": spell.Classes.Add(new LifeDomain()); break;
                                        case "Cleric (Light)": spell.Classes.Add(new LightDomain()); break;
                                        case "Cleric (Nature)": spell.Classes.Add(new NatureDomain()); break;
                                        case "Cleric (Tempest)": spell.Classes.Add(new TempestDomain()); break;
                                        case "Cleric (Trickery)": spell.Classes.Add(new TrickeryDomain()); break;
                                        case "Cleric (War)": spell.Classes.Add(new WarDomain()); break;
                                        case "Druid": spell.Classes.Add(new Druid()); break;
                                        case "Druid (Arctic)": spell.Classes.Add(new Arctic()); break;
                                        case "Druid (Coast)": spell.Classes.Add(new Coast()); break;
                                        case "Druid (Desert)": spell.Classes.Add(new Desert()); break;
                                        case "Druid (Grassland)": spell.Classes.Add(new Grassland()); break;
                                        case "Druid (Forest)": spell.Classes.Add(new Forest()); break;
                                        case "Druid (Mountain)": spell.Classes.Add(new Mountain()); break;
                                        case "Druid (Swamp)": spell.Classes.Add(new Swamp()); break;
                                        case "Druid (Underdark)": spell.Classes.Add(new Underdark()); break;
                                        case "Fighter (Eldritch Knight)": spell.Classes.Add(new EldritchKnight()); break;
                                        case "Paladin": spell.Classes.Add(new Paladin()); break;
                                        case "Paladin (Ancients)": spell.Classes.Add(new OathOfTheAncients()); break;
                                        case "Paladin (Crown)": spell.Classes.Add(new OathOfTheCrown()); break;
                                        case "Paladin (Devotion)": spell.Classes.Add(new OathOfDevotion()); break;
                                        case "Paladin (Oathbreaker)": spell.Classes.Add(new Oathbreaker()); break;
                                        case "Paladin (Vengeance)": spell.Classes.Add(new OathOfVengeance()); break;
                                        case "Ranger": spell.Classes.Add(new Ranger()); break;
                                        case "Rogue (Arcane Trickster)": spell.Classes.Add(new ArcaneTrickster()); break;
                                        case "Sorcerer": spell.Classes.Add(new Sorcerer()); break;
                                        case "Warlock": spell.Classes.Add(new Warlock()); break;
                                        case "Warlock (Undying)": spell.Classes.Add(new TheUndying()); break;
                                        case "Warlock (Archfey)": spell.Classes.Add(new TheArchfey()); break;
                                        case "Warlock (Fiend)": spell.Classes.Add(new TheFiend()); break;
                                        case "Warlock (Great Old One)": spell.Classes.Add(new TheGreatOldOne()); break;
                                        case "Wizard": spell.Classes.Add(new Wizard()); break;
                                    }
                                }
                                break;
                            case "text":
                                spell.Description += reader.ReadString() + "\r\n";
                                break;
                        }
                    }
                }

                if (spell != null)
                {
                    result.Add(spell);
                }
            }
            finally
            {
                reader.Close();
            }

            loaded = true;

            return await Task.FromResult(result);
        }

        private async Task<IEnumerable<Spell>> LoadSpellsSynteticAsync()
        {
            loaded = false;

            var result = new List<Spell>();

            const int maxLevel = 9;
            const int spellsPerLevel = 15;

            for (var i = 0; i <= maxLevel; i++)
            {
                for (var j = 0; j <= spellsPerLevel; j++)
                {
                    result.Add(new Spell
                    {
                        Name = string.Format("Spell [{0}]", j + i * spellsPerLevel),
                        Level = i,
                        School = (SpellSchool)(j % Enum.GetNames(typeof(SpellSchool)).Length),
                        Description = string.Format("Opis zaklecia [{0}]", j + i * spellsPerLevel),
                    });
                }
            }

            loaded = true;

            return await Task.FromResult(result);
        }
    }
}
