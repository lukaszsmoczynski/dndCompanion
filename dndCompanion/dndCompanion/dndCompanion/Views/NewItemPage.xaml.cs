using dndCompanion.Models;
using dndCompanion.ViewModels;
using Xamarin.Forms;

namespace dndCompanion.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}