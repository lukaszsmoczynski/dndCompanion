using dndCompanion.Models.Spells;
using dndCompanion.Services.DndDataStore.Spells;
using dndCompanion.Views.Spell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dndCompanion.ViewModels.Spells
{
    public class SpellGroup : ObservableCollection<Spell>, INotifyPropertyChanged
    {
        private bool _expanded;
        public int Level { get; private set; }
        public string Title => Level == 0 ? "Cantrip" : "Level " + Level;
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
        public SpellGroup(int level, List<Spell> spells, bool expanded = false) : base(spells)
        {
            Level = level;
            _expanded = expanded;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class SpellsViewModel : BaseViewModel<Spell>
    {
        public ISpellsDataStore DataStore => DependencyService.Get<ISpellsDataStore>();

        private Spell _selectedSpell;

        public Dictionary<int, IEnumerable<Spell>> AllSpells { get; private set; }
        public Dictionary<int, IEnumerable<Spell>> FilteredSpells { get; private set; }
        public ObservableCollection<SpellGroup> VisibleSpells { get; private set; } = new ObservableCollection<SpellGroup>();

        public Command LoadSpellsCommand { get; }
        public Command<Spell> ItemTapped { get; }
        public Command<SpellGroup> HeaderTapped { get; }
        public SpellsViewModel()
        {
            Title = "Spells";

            LoadSpellsCommand = new Command(async () => await ExecuteLoadSpellsCommand());

            ItemTapped = new Command<Spell>(OnItemSelected);
            HeaderTapped = new Command<SpellGroup>(OnHeaderSelected);
        }

        async Task ExecuteLoadSpellsCommand()
        {
            IsBusy = true;

            try
            {
                VisibleSpells.Clear();
                var spells = await DataStore.GetAllAsync();
                AllSpells = spells
                    .GroupBy(spell => spell.Level)
                    .OrderBy(group => group.Key)
                    .ToDictionary(group => group.Key, group => group.Select(x => x));
                FilteredSpells = new(AllSpells);

                VisibleSpells.Clear();
                foreach (var group in FilteredSpells)
                {
                    VisibleSpells.Add(new(group.Key, new()));
                }
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

        public Spell SelectedSpell
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
            SelectedSpell = null;
        }

        async void OnItemSelected(Spell spell)
        {
            if (spell == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(SpellDetailPage)}?{nameof(SpellDetailViewModel.Id)}={spell.Id}");
        }

        void OnHeaderSelected(SpellGroup header)
        {
            if (header == null)
                return;

            header.Expanded = !header.Expanded;
            if (header.Expanded)
            {
                var spellGroup = FilteredSpells.First(level => level.Key.Equals(header.Level));
                foreach (var spell in spellGroup.Value)
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
