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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMedications();
    }

    private async Task LoadMedications()
    {
        var meds = await _dbService.GetMedications();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            MedicationListView.ItemsSource = meds;
        });
    }

    //add x button click event
    private async void Xbutton_Clicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is Medication med)
        {
            await _dbService.Delete(med);
            await LoadMedications();
        }
    }

    private async void AddNew_Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddMedicationPage));
    }
}


