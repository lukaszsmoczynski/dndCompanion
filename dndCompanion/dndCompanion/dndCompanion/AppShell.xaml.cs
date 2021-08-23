using dndCompanion.Views;
using dndCompanion.Views.Spell;
using dndCompanion.Views.Character;
using System;
using Xamarin.Forms;

namespace dndCompanion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(SpellDetailPage), typeof(SpellDetailPage));
            Routing.RegisterRoute(nameof(CharacterClassPage), typeof(CharacterClassPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
