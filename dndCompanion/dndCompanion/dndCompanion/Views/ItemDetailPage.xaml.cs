using dndCompanion.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace dndCompanion.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}