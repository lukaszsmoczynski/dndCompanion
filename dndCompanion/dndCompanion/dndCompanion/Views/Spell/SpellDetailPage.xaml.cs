using dndCompanion.ViewModels.Spells;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dndCompanion.Views.Spell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellDetailPage : ContentPage
    {
        public SpellDetailPage()
        {
            InitializeComponent();
            BindingContext = new SpellDetailViewModel();
        }
    }
}