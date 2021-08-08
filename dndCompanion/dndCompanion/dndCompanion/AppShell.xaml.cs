using dndCompanion.ViewModels;
using dndCompanion.Views;
using dndCompanion.Views.Spell;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace dndCompanion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(SpellDetailPage), typeof(SpellDetailPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
