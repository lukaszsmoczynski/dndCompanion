namespace dndCompanion.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new dndCompanion.App());
        }
    }
}
