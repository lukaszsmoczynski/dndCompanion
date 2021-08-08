using dndCompanion.ViewModels.Spell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            BindingContext = _viewModel = new SpellsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}