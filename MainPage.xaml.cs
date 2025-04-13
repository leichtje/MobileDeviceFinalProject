using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MobileDeviceFinalProject;

public partial class MainPage : ContentPage
{

    private readonly LocalDbService _dbService;

    public MainPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        
        // get all rows in DB and bind to MedicationListView ItemSource
        Task.Run(async() => MedicationListView.ItemsSource = await _dbService.GetMedications());

        // test
        /*
        Medication med = new Medication
        {
            MedName = "Adderall",
            Dosage = "9",
            MedInstructions = "Yes",
            TimeTaken = "3:00PM",
            DaysTaken = "MWF"
        };
        */
        
    }
    protected override async void OnAppearing() {
        base.OnAppearing();
        await LoadMedications();
        
        // get current values from database everytime page appears, otherwise old data is shown after an edit
        var medications = await _dbService.GetMedications();
        MedicationListView.ItemsSource = medications;
    }

    private async Task LoadMedications() {
        var meds = await _dbService.GetMedications();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            MedicationListView.ItemsSource = meds;
        });
    }
    
    // Edit button event handler
    private void OnEditButtonClicked(object sender, EventArgs args) {

            var button = sender as Button;
            var medication = (Medication)button.BindingContext;
            
            Navigation.PushAsync(new EditPage(medication, _dbService));
    }

    // X button event handler
    private async void Xbutton_Clicked(object sender, EventArgs e) {
        
        if ((sender as Button)?.BindingContext is Medication med) {
            await _dbService.Delete(med);
            await LoadMedications();
        }
    }

    // add new button event handler
    private async void AddNew_Button_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync(nameof(AddMedicationPage));
    }
    
    // item tapped event handler
    private async void OnInfoTapped(object sender, ItemTappedEventArgs e) {
        
        if (e.Item is Medication medication)
        {
            await Navigation.PushAsync(new MedInfo(medication));
        }
    }
}


