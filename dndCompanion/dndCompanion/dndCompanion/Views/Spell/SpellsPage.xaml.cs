using dndCompanion.ViewModels.Spells;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dndCompanion.Views.Spell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellsPage : ContentPage
    {
        readonly SpellsViewModel _viewModel;

        public SpellsPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SpellDetailPage), typeof(SpellDetailPage));

            BindingContext = _viewModel = new SpellsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}