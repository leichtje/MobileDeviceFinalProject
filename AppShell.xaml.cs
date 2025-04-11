namespace MobileDeviceFinalProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddMedicationPage), typeof(AddMedicationPage));
        }
    }
}
