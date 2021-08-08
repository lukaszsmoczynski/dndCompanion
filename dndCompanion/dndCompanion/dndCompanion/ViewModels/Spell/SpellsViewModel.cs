using dndCompanion.Views.Spell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dndCompanion.ViewModels.Spell
{
    public class SpellGroup : ObservableCollection<Models.Spell.Spell>, INotifyPropertyChanged
    {
        private bool _expanded;
        public int Level { get; private set; }
        public string Title { get => Level == 0 ? "Cantrip" : "Level " + Level; }
        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                }
            }
        }
        public SpellGroup(int level, List<Models.Spell.Spell> spells, bool expanded = false) : base(spells)
        {
            Level = level;
            _expanded = expanded;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class SpellsViewModel : BaseViewModel
    {
        private Models.Spell.Spell _selectedSpell;

        public List<SpellGroup> AllSpells { get; private set; } = new List<SpellGroup>();
        public List<SpellGroup> Spells { get; private set; } = new List<SpellGroup>();
        //public ObservableCollection<Models.Spell.Spell> Spells { get; }
        public Command LoadSpellsCommand { get; }
        public Command<Models.Spell.Spell> ItemTapped { get; }
        public Command<SpellGroup> HeaderTapped { get; }

        public SpellsViewModel()
        {
            Title = "Spells";
            //Spells = new ObservableCollection<Models.Spell.Spell>();
            LoadSpellsCommand = new Command(async () => await ExecuteLoadSpellsCommand());

            ItemTapped = new Command<Models.Spell.Spell>(OnItemSelected);
            HeaderTapped = new Command<SpellGroup>(OnHeaderSelected);
        }

        async Task ExecuteLoadSpellsCommand()
        {
            IsBusy = true;

            try
            {
                Spells.Clear();
                var spells = await DataStore.GetSpellsAsync(true);

                var spellsDictionary = new Dictionary<int, List<Models.Spell.Spell>>();
                foreach (var spell in spells)
                {
                    if (!spellsDictionary.ContainsKey(spell.Level))
                    {
                        spellsDictionary.Add(spell.Level, new List<Models.Spell.Spell>());
                    }
                    else
                    {
                        spellsDictionary[spell.Level].Add(spell);
                    }
                }

                foreach (var spellList in spellsDictionary)
                {
                    Spells.Add(new SpellGroup(spellList.Key, new List<Models.Spell.Spell>()));
                    AllSpells.Add(new SpellGroup(spellList.Key, spellList.Value));
                }
                Spells.Sort((l, r) => l.Level.CompareTo(r.Level));
                AllSpells.Sort((l, r) => l.Level.CompareTo(r.Level));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Models.Spell.Spell SelectedItem
        {
            get => _selectedSpell;
            set
            {
                SetProperty(ref _selectedSpell, value);
                OnItemSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async void OnItemSelected(Models.Spell.Spell spell)
        {
            if (spell == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(SpellDetailPage)}?{nameof(SpellDetailViewModel.SpellName)}={spell.Name}");
        }

        void OnHeaderSelected(SpellGroup header)
        {
            if (header == null)
                return;

            var x = Spells.Find(level => level.Level.Equals(header.Level));
            x.Expanded = !x.Expanded;
            if (x.Expanded)
            { 
                foreach (var spell in AllSpells.Find(level => level.Level.Equals(header.Level)))
                {
                    header.Add(spell);
                }
            }
            else
            {
                header.Clear();
            }
        }
    }
}
