using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using dndCompanion.Services.DndDataStore.Spells;
using dndCompanion.Models.Spells;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dndCompanion.ViewModels.Spells
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class SpellDetailViewModel : BaseViewModel<Spell>
    {
        public ISpellsDataStore DataStore => DependencyService.Get<ISpellsDataStore>();

        private Spell _spell;
        private bool hasComponents;
        private string materialComponents;
        public int ClassesListViewHeight { get => 20 * SpellClasses.Count(); }

        public Command<string> ClassTapped { get; }
        public Spell Spell
        {
            get => _spell;
            set
            {
                HasComponents = value.Components?.Material == true
                    || value.Components?.Somatic == true
                    || value.Components?.Verbal == true;
                MaterialComponents = string.Join("\r\n",
                    value.Components?.MaterialComponents?.Select(
                        component =>
                        {
                            var result = component.Name;
                            if (component.Description == "")
                            {
                                result += $" ({component.Description})";
                            }
                            if (component.Value is not null || component.Consumed)
                            {
                                result += " [";

                                if (component.Value is not null)
                                {
                                    result += component.Value + " gp";
                                }
                                if (component.Consumed)
                                {
                                    if (component.Value is not null)
                                    {
                                        result += ",";
                                    }
                                    result += component.Consumed ? " consumed" : "";
                                }

                                result += "]";
                            }

                            return result;
                        })
                    ?? new string[] { });
                Level = value.Level switch
                {
                    0 => "Cantrip",
                    1 => value.Level + "st",
                    2 => value.Level + "nd",
                    3 => value.Level + "rd",
                    _ => value.Level + "th",
                };

                foreach (var @class in value.Classes)
                    SpellClasses.Add(@class);
                OnPropertyChanged(nameof(ClassesListViewHeight));

                SetProperty(ref _spell, value);
            }
        }
        private Guid id;
        private string level;
        private ObservableCollection<string> spellClasses = new ObservableCollection<string>();

        public ObservableCollection<string> SpellClasses
        {
            get => spellClasses;
            set => SetProperty(ref spellClasses, value);
        }

        public SpellDetailViewModel()
        {
            ClassTapped = new Command<string>(OnClassSelected);
        }

        private async void OnClassSelected(string casterClass)
        {
            if (casterClass == null)
                return;

            //await Shell.Current.GoToAsync($"{nameof(CharacterClassPage)}?{nameof(CharacterClassViewModel.CharacterClassName)}={casterClass.Name}");
        }

        public string Id
        {
            get => id.ToString();
            set
            {
                id = Guid.Parse(value);
                LoadSpell(id);
            }
        }

        public string Level
        {
            get => level;
            set => SetProperty(ref level, value);
        }
        public bool HasComponents
        {
            get => hasComponents;
            set => SetProperty(ref hasComponents, value);
        }
        public string MaterialComponents
        {
            get => materialComponents;
            set => SetProperty(ref materialComponents, value);
        }
        public async void LoadSpell(Guid id)
        {
            try
            {
                Spell = await DataStore.GetOneAsync(id);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Spell");
            }
        }
    }
}
