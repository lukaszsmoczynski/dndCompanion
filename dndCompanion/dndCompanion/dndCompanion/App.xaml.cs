using dndCompanion.Services;
using dndCompanion.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dndCompanion
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDndDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
