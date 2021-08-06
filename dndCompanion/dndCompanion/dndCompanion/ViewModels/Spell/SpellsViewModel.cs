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
    class SpellsViewModel : BaseViewModel
    {
        public class SpellGroup : ObservableCollection<Models.Spell.Spell>, INotifyPropertyChanged
        {
            private bool _expanded;
            public string Name { get; private set; }
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
            public SpellGroup(string name, List<Models.Spell.Spell> spells) : base(spells)
            {
                Name = name;
                _expanded = true;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Models.Spell.Spell _selectedSpell;

        public List<SpellGroup> AllSpells { get; private set; } = new List<SpellGroup>();
        public List<SpellGroup> Spells { get; private set; } = new List<SpellGroup>();
        //public ObservableCollection<Models.Spell.Spell> Spells { get; }
        public Command LoadSpellsCommand { get; }
        public Command<SpellGroup> HeaderTapped { get; }

        public SpellsViewModel()
        {
            Title = "Spells";
            //Spells = new ObservableCollection<Models.Spell.Spell>();
            LoadSpellsCommand = new Command(async () => await ExecuteLoadSpellsCommand());

            //ItemTapped = new Command<Models.Spell.Spell>(OnItemSelected);
            HeaderTapped = new Command<SpellGroup>(OnHeaderSelected);
        }

        async Task ExecuteLoadSpellsCommand()
        {
            IsBusy = true;

            try
            {
                Spells.Clear();
                var spells = await DataStore.GetItemsAsync(true);

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
                    Spells.Add(new SpellGroup(spellList.Key.ToString(), spellList.Value));
                    AllSpells.Add(new SpellGroup(spellList.Key.ToString(), spellList.Value));
                }
                Spells.Sort((l, r) => l.Name.CompareTo(r.Name));
                AllSpells.Sort((l, r) => l.Name.CompareTo(r.Name));
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

        async void OnItemSelected(Models.Spell.Spell item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Name)}={item.Name}");
        }

        //async 
            void OnHeaderSelected(SpellGroup header)
        {
            header = Spells[0];
            if (header == null)
                return;

            var x = Spells.Find(level => level.Name.Equals(header.Name));
            x.Expanded = !x.Expanded;
            if (x.Expanded)
            { 
                foreach (var spell in AllSpells.Find(level => level.Name.Equals(header.Name)))
                {
                    header.Add(spell);
                }
            }
            else
            {
                header.Clear();
            }

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Name)}={item.Name}");
        }
    }
}
