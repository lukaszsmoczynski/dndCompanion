using dndCompanion.Models.Character.Class;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace dndCompanion.ViewModels.Character
{
    [QueryProperty(nameof(CharacterClassName), nameof(CharacterClassName))]
    class CharacterClassViewModel : BaseViewModel<CharacterClass>
    {
        private string characterClassName;
        private CharacterClass _class;

        public string CharacterClassName
        {
            get => characterClassName;
            set
            {
                characterClassName = value;
                LoadClassByName(value);
            }
        }

        public CharacterClass Class
        {
            get => _class;
            set
            {
                SetProperty(ref _class, value);
            }
        }
        public async void LoadClassByName(string name)
        {
            try
            {
                //Class = await DataStore.GetOneAsync(name);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Character Class");
            }
        }
    }
}
