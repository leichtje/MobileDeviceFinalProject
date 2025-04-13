using Microsoft.Maui.Controls;

namespace MobileDeviceFinalProject
{
    public partial class AddMedicationPage : ContentPage
    {
        private readonly LocalDbService _dbService;

        public AddMedicationPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var newMed = new Medication
            {
                MedName = MedNameEntry.Text,
                Dosage = DosageEntry.Text,
                MedInstructions = InstructionsEntry.Text,
                TimeTaken = TimeTakenEntry.Text,
                DaysTaken = DaysTakenEntry.Text
            };

            await _dbService.Create(newMed);
            await Shell.Current.GoToAsync(".."); // goes back to previous page
        }
    }
}
