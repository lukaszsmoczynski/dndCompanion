using dndCompanion.ViewModels.Character;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dndCompanion.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterClassPage : ContentPage
    {
        public CharacterClassPage()
        {
            InitializeComponent();
            BindingContext = new CharacterClassViewModel();
        }
    }
}