using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MobileDeviceFinalProject;
using Plugin.LocalNotification;

namespace MobileDeviceFinalProject
{
   public partial class MainPage : ContentPage
   {
      private readonly LocalDbService _dbService;

#if ANDROID
      private readonly INotificationService _notificationService;
#endif

#if WINDOWS
        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
 
            // Get all rows in DB and bind to MedicationListView ItemSource
            Task.Run(async () => MedicationListView.ItemsSource = await _dbService.GetMedications());
        }
#endif

#if ANDROID
      public MainPage(LocalDbService dbService, INotificationService notificationService)
      {
         InitializeComponent();
         _dbService = dbService;
         _notificationService = notificationService;


         // Get all rows in DB and bind to MedicationListView ItemSource
         Task.Run(async () => MedicationListView.ItemsSource = await _dbService.GetMedications());
      }
#endif

      protected override async void OnAppearing()
      {
         base.OnAppearing();
         await LoadMedications();

         // Get current values from database every time page appears, ensuring fresh data after edits
         var medications = await _dbService.GetMedications();
         MedicationListView.ItemsSource = medications;
      }

      private async Task LoadMedications()
      {
         var meds = await _dbService.GetMedications();
         MainThread.BeginInvokeOnMainThread(() =>
         {
            MedicationListView.ItemsSource = meds;
         });
      }

      // Edit medication entry event handler
      private void OnEditButtonClicked(object sender, EventArgs args)
      {
         var button = sender as Button;
         var medication = (Medication)button.BindingContext;

         Navigation.PushAsync(new EditPage(medication, _dbService));
      }

      // Delete medication entry event handler
      private async void Xbutton_Clicked(object sender, EventArgs e)
      {
         if ((sender as Button)?.BindingContext is Medication med)
         {
            await _dbService.Delete(med);
            await LoadMedications();
         }
      }

      // Add new medication event handler
      private async void AddNew_Button_Clicked(object sender, EventArgs e)
      {
         await Shell.Current.GoToAsync(nameof(AddMedicationPage));
      }

      // Show medication details when tapped
      private async void OnInfoTapped(object sender, ItemTappedEventArgs e)
      {
         if (e.Item is Medication medication)
         {
            await Navigation.PushAsync(new MedInfo(medication));
         }
      }

#if WINDOWS
        private async void OnScheduleNotificationClicked(object sender, EventArgs e)
        { 
        }
#endif


#if ANDROID
      // Schedule notification event handler
      private async void OnScheduleNotificationClicked(object sender, EventArgs e)
      {
         if ((sender as Button)?.BindingContext is Medication medication)
         {
            try
            {
               // Validate and parse TimeTaken to DateTime
               if (!DateTime.TryParse(medication.TimeTaken, out var notifyTime))
               {
                  await DisplayAlert("Error", "Invalid time format. Please ensure TimeTaken is correctly set.", "OK");
                  return;
               }

               // Schedule the notification using the injected service
               _notificationService.ScheduleNotification(
                   $"Reminder: {medication.MedName}",
                   $"It's time to take {medication.MedName}. Dosage: {medication.Dosage}",
                   notifyTime);

               await DisplayAlert("Success", $"Notification scheduled for {notifyTime}.", "OK");
            }
            catch (Exception ex)
            {
               await DisplayAlert("Error", $"Failed to schedule notification: {ex.Message}", "OK");
            }
         }
      }
#endif
   }
}