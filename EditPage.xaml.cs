using Entry = Microsoft.Maui.Controls.Entry;

namespace MobileDeviceFinalProject;

public partial class EditPage : ContentPage {
    
    private readonly LocalDbService _dbService;
    private readonly Medication _medication;
    public EditPage(Medication medication, LocalDbService dbService) {
        InitializeComponent();
        
        _dbService = dbService;
        _medication = medication;
        
        string[] constructorVariables = [medication.MedName, medication.Dosage, medication.MedInstructions, medication.TimeTaken, medication.DaysTaken];
        Entry[] entries = [MedNameEntry, DosageEntry, SpecialInstructionsEntry, TimeTakenEntry, DaysTakenEntry];

        // populate entries
        for (var i = 0; i < constructorVariables.Length; i++) {
            if (constructorVariables[i] != null) {
                entries[i].Text = constructorVariables[i];
            } else {
                entries[i].Text = "";
            }
        }
    }
    private void OnBackButtonClicked(object sender, EventArgs e) {
        Navigation.PopAsync();
    }

    private void OnSaveChangesClicked(object sender, EventArgs e) {
        var editedMedication = new Medication {
            EntryId = _medication.EntryId, // EntryId of the Medication object originally passed to the page constructor
            MedName = MedNameEntry.Text,   // rest of these are just what was input on the edit page 
            Dosage = DosageEntry.Text,
            MedInstructions = SpecialInstructionsEntry.Text,
            TimeTaken = TimeTakenEntry.Text,
            DaysTaken = DaysTakenEntry.Text
        };
        
        // update then go back to home page
        Task.Run(async () => await _dbService.Update(editedMedication));
        Navigation.PopAsync();
    }
}