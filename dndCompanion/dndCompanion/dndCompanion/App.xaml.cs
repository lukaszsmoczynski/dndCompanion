using dndCompanion.Externals.DndDatabase;
using dndCompanion.Services.DndDataStore.Character;
using dndCompanion.Services.DndDataStore.Spells;
using System.Net;
using Xamarin.Forms;

namespace dndCompanion
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
            //DependencyService.Register<MockSpellsDataStore>();
            DependencyService.Register<ISpellsDataStore, SpellsDataStore>();
            DependencyService.Register<MockClassesDataStore>();

            DependencyService.RegisterSingleton<IDndDatabaseClient>(new DndDatabaseClient());
#else
            DependencyService.Register<DataStore>();
            DependencyService.Register<ClassesDataStore>();
#endif
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
