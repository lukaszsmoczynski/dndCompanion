using dndCompanion.Models.Spells;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace dndCompanion.Services.DndDataStore.Spells
{
    class MockSpellsDataStore : ISpellsDataStore
    {
        private readonly string fileName = "spells.xml";
        public List<Spell> Spells { get; private set; }
        private bool loaded = false;

        public MockSpellsDataStore()
        {
        }
        public async Task<bool> AddAsync(Spell item)
        {
            Spells.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Id == id).FirstOrDefault();
            Spells.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Spell> GetOneAsync(Guid id)
        {
            return await Task.FromResult(Spells.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Spell>> GetAllAsync(bool forceRefresh = false)
        {
            if (forceRefresh || !loaded)
                Spells = (await LoadSpellsAsync()).ToList();
            return await Task.FromResult(Spells);
        }

        public async Task<bool> UpdateAsync(Spell spell)
        {
            var oldItem = Spells.Where((Spell arg) => arg.Name == spell.Name).FirstOrDefault();
            Spells.Remove(oldItem);
            Spells.Add(spell);

            return await Task.FromResult(true);
        }

        private async Task<IEnumerable<Spell>> LoadSpellsAsync(bool loadSyntetic = true)
        {
            return (!loadSyntetic && File.Exists(fileName)) ? await LoadSpellsFromFileAsync() : await LoadSpellsSynteticAsync();
        }
        private async Task<IEnumerable<Spell>> LoadSpellsFromFileAsync()
        {
            loaded = false;

            var result = new List<Spell>();
            #region komentarz
            //var reader = XmlReader.Create(fileName);
            //try
            //{
            //    var c = new List<string>();

            //    Spell spell = null;
            //    while (reader.Read())
            //    {
            //        if (reader.IsStartElement())
            //        {
            //            string value;
            //            string[] tokens;

            //            switch (reader.Name.ToString())
            //            {
            //                case "spell":
            //                    if (spell != null)
            //                    {
            //                        result.Add(spell);
            //                    }

            //                    spell = new Spell();
            //                    break;
            //                case "name":
            //                    spell.Name = reader.ReadString();
            //                    break;
            //                case "level":
            //                    spell.Level = Int32.Parse(reader.ReadString());
            //                    break;

            //                case "school":
            //                    switch (reader.ReadString())
            //                    {
            //                        case "A": spell.School = School.Abjuration; break;
            //                        case "C": spell.School = School.Conjuration; break;
            //                        case "D": spell.School = School.Divination; break;
            //                        case "EN": spell.School = School.Enchantment; break;
            //                        case "EV": spell.School = School.Evocation; break;
            //                        case "I": spell.School = School.Illusion; break;
            //                        case "N": spell.School = School.Necromancy; break;
            //                        case "T": spell.School = School.Transmutation; break;
            //                    }
            //                    break;
            //                case "time":
            //                    value = reader.ReadString();

            //                    tokens = value.Split(' ');
            //                    spell.CastTime.Amount = Int32.Parse(tokens[0]);
            //                    switch (tokens[1])
            //                    {
            //                        case "bonus": spell.CastTime.Unit = TimeUnit.BonusAction; break;
            //                        case "reaction": spell.CastTime.Unit = TimeUnit.Reaction; break;
            //                        case "action": spell.CastTime.Unit = TimeUnit.Action; break;
            //                        case "round": spell.CastTime.Unit = TimeUnit.Round; break;
            //                        case "minute": spell.CastTime.Unit = TimeUnit.Minute; break;
            //                        case "hour": spell.CastTime.Unit = TimeUnit.Hour; break;
            //                    }
            //                    break;
            //                case "range":
            //                    value = reader.ReadString();
            //                    if (value.StartsWith("Self"))
            //                    {
            //                        spell.Range.Unit = RangeUnit.Self;
            //                        break;
            //                    }
            //                    else if (value.StartsWith("Touch"))
            //                    {
            //                        spell.Range.Unit = RangeUnit.Touch;
            //                        break;
            //                    }
            //                    else if (value.StartsWith("Sight"))
            //                    {
            //                        spell.Range.Unit = RangeUnit.Sight;
            //                        break;
            //                    }
            //                    else if (value.StartsWith("Special"))
            //                    {
            //                        spell.Range.Unit = RangeUnit.Special;
            //                        break;
            //                    }
            //                    else if (value.StartsWith("Unlimited"))
            //                    {
            //                        spell.Range.Unit = RangeUnit.Special;
            //                        break;
            //                    }

            //                    tokens = value.Split(' ');
            //                    spell.Range.Amount = Int32.Parse(tokens[0]);
            //                    switch (tokens[1])
            //                    {
            //                        case "feet": spell.Range.Unit = RangeUnit.Foot; break;
            //                        case "mile":
            //                        case "miles":
            //                            spell.Range.Unit = RangeUnit.Mile;
            //                            break;
            //                    }
            //                    break;
            //                case "components":
            //                    value = reader.ReadString();
            //                    tokens = value.Split('(');
            //                    if (tokens.Length > 1)
            //                        spell.Components.MaterialComponents.Add(new MaterialComponent() { Name = tokens[1].TrimEnd(')') });

            //                    tokens = tokens[0].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //                    spell.Components.Verbal = tokens.Contains("V");
            //                    spell.Components.Somatic = tokens.Contains("S");
            //                    spell.Components.Material = tokens.Contains("M");
            //                    break;
            //                //                             < duration > Concentration, up to 1 hour </ duration >
            //                case "classes":
            //                    value = reader.ReadString();

            //                    tokens = value.Split(',');
            //                    foreach (var _class in tokens.Select(p => p.Trim()))
            //                    {
            //                        //switch(_class)
            //                        //{
            //                        //    case "Artificer": spell.Classes.Add(new Artificer()); break;
            //                        //    case "Bard": spell.Classes.Add(new Bard()); break;
            //                        //    case "Cleric": spell.Classes.Add(new Cleric()); break;
            //                        //    case "Cleric (Arcana)": spell.Classes.Add(new ArcanaDomain()); break;
            //                        //    case "Cleric (Death)": spell.Classes.Add(new DeathDomain()); break;
            //                        //    case "Cleric (Knowledge)": spell.Classes.Add(new KnowledgeDomain()); break;
            //                        //    case "Cleric (Life)": spell.Classes.Add(new LifeDomain()); break;
            //                        //    case "Cleric (Light)": spell.Classes.Add(new LightDomain()); break;
            //                        //    case "Cleric (Nature)": spell.Classes.Add(new NatureDomain()); break;
            //                        //    case "Cleric (Tempest)": spell.Classes.Add(new TempestDomain()); break;
            //                        //    case "Cleric (Trickery)": spell.Classes.Add(new TrickeryDomain()); break;
            //                        //    case "Cleric (War)": spell.Classes.Add(new WarDomain()); break;
            //                        //    case "Druid": spell.Classes.Add(new Druid()); break;
            //                        //    case "Druid (Arctic)": spell.Classes.Add(new Arctic()); break;
            //                        //    case "Druid (Coast)": spell.Classes.Add(new Coast()); break;
            //                        //    case "Druid (Desert)": spell.Classes.Add(new Desert()); break;
            //                        //    case "Druid (Grassland)": spell.Classes.Add(new Grassland()); break;
            //                        //    case "Druid (Forest)": spell.Classes.Add(new Forest()); break;
            //                        //    case "Druid (Mountain)": spell.Classes.Add(new Mountain()); break;
            //                        //    case "Druid (Swamp)": spell.Classes.Add(new Swamp()); break;
            //                        //    case "Druid (Underdark)": spell.Classes.Add(new Underdark()); break;
            //                        //    case "Fighter (Eldritch Knight)": spell.Classes.Add(new EldritchKnight()); break;
            //                        //    case "Paladin": spell.Classes.Add(new Paladin()); break;
            //                        //    case "Paladin (Ancients)": spell.Classes.Add(new OathOfTheAncients()); break;
            //                        //    case "Paladin (Crown)": spell.Classes.Add(new OathOfTheCrown()); break;
            //                        //    case "Paladin (Devotion)": spell.Classes.Add(new OathOfDevotion()); break;
            //                        //    case "Paladin (Oathbreaker)": spell.Classes.Add(new Oathbreaker()); break;
            //                        //    case "Paladin (Vengeance)": spell.Classes.Add(new OathOfVengeance()); break;
            //                        //    case "Ranger": spell.Classes.Add(new Ranger()); break;
            //                        //    case "Rogue (Arcane Trickster)": spell.Classes.Add(new ArcaneTrickster()); break;
            //                        //    case "Sorcerer": spell.Classes.Add(new Sorcerer()); break;
            //                        //    case "Warlock": spell.Classes.Add(new Warlock()); break;
            //                        //    case "Warlock (Undying)": spell.Classes.Add(new TheUndying()); break;
            //                        //    case "Warlock (Archfey)": spell.Classes.Add(new TheArchfey()); break;
            //                        //    case "Warlock (Fiend)": spell.Classes.Add(new TheFiend()); break;
            //                        //    case "Warlock (Great Old One)": spell.Classes.Add(new TheGreatOldOne()); break;
            //                        //    case "Wizard": spell.Classes.Add(new Wizard()); break;
            //                        //}
            //                    }
            //                    break;
            //                case "text":
            //                    spell.Description += reader.ReadString() + "\r\n";
            //                    break;
            //            }
            //        }
            //    }

            //    if (spell != null)
            //    {
            //        result.Add(spell);
            //    }
            //}
            //finally
            //{
            //    reader.Close();
            //}
            #endregion

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
                        School = (School)(j % Enum.GetNames(typeof(School)).Length),
                        Description = string.Format("Opis zaklecia [{0}]", j + i * spellsPerLevel),
                    });
                }
            }

            var spell =new Spell()
            {
                Name = "Find Familiar",
                Level = 1,
                School = School.Conjuration,
            };
            spell.CastTime.Amount = 1;
            spell.CastTime.Unit = CastTimeUnit.Hour;
            spell.Range.Amount = 10;
            spell.Range.Unit = RangeUnit.Foot;
            spell.Components.Verbal = true;
            spell.Components.Somatic = true;
            spell.Components.Material = true;
            spell.Components.MaterialComponents = new List<MaterialComponent> 
            {
                new MaterialComponent()
                {
                    Name = "charcoal, incense and herbs that must be consum ed by fire in a brass brazier",
                    Value = 10,
                    Consumed = true
                }
            };
            spell.Duration.Unit = DurationUnit.Instant;
            spell.Description = @"You gain the service of a familiar, a spirit that takes an animal form you choose - bat, cat, crab, frog (toad), hawk, lizard, octopus, owl, poisonous snake, fish (quipper), rat, raven, sea horse, spider, or weasel. Appearing in an unoccupied space within range, the familiar has the statistics of the chosen form, though it is a celestial, fey or fiend (your choice) instead of a beast.

Your familiar acts independently of you, but it always obeys your commands. In combat, it rolls its own initiative and acts on its own turn. A familiar can't attack, but it can take other actions as normal.

When the familiar drops to 0 hit points, it disappears, leaving behind no physical form. It reappears after you cast this spell again. While your familiar is within 100 feet of you, you can communicate with it telepathically. Additionally, as an action, you can see through your familiar's eyes and hear what it hears until the start of your next turn, gaining the benefits of any special senses that the familiar has. During this time, you are deaf and blind with regard to your own senses.

As an action, you can temporarily dismiss your familiar. It disappears into a pocket dimension where it awaits you summons. Alternatively, you can dismiss it forever. As an action while it is temporarily dismissed, you can cause it to reappear in any unoccupied space within 30 feet of you.

You can't have more than one familiar at a time. If you cast this spell while you already have a familiar, you instead cause it to adopt a new form. Choose one of the forms from the above list. Your familiar transforms into the chosen creature.

Finally, when you cast a spell with a range of touch, your familiar can deliver the spell as if it had cast the spell. Your familiar must be within 100 feet of you, and it must use its reaction to deliver the spell when you cast it. If the spell requires an attack roll, you use your attack modifier for the roll.";
            spell.Classes = new List<string>
            {
                "Wizard"
            };
            spell.Image = ImageSource.FromResource(string.Format("dndCompanion.Services.DndDataStore.Spells.MockResources.{0}.png", spell.Name),
                typeof(MockSpellsDataStore).GetTypeInfo().Assembly);
            result.Add(spell);
            var assembly = typeof(MockSpellsDataStore).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }

            loaded = true;

            return await Task.FromResult(result);
        }
    }
}
