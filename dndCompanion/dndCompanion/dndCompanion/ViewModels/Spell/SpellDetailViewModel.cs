using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using dndCompanion.Models.Spell;

namespace dndCompanion.ViewModels.Spell
{
    [QueryProperty(nameof(SpellName), nameof(SpellName))]
    public class SpellDetailViewModel : BaseViewModel
    {
        private Models.Spell.Spell _spell;
        private string classNames;

        public Models.Spell.Spell Spell
        {
            get => _spell;
            set => SetProperty(ref _spell, value);
        }
        private string spellName;

        public string SpellName
        {
            get
            {
                return spellName;
            }
            set
            {
                spellName = value;
                LoadSpellByName(value);
            }
        }
        public string ClassNames { get => classNames; set => SetProperty(ref classNames, value); }
        public async void LoadSpellByName(string name)
        {
            try
            {
                Spell = await DataStore.GetSpellAsync(name);
                spellName = name;
                ClassNames = string.Join(", ", Spell?.Classes.Select(_class => _class.Name));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Spell");
            }
        }
    }
}
