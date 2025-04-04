namespace MobileDeviceFinalProject;

    public partial class MainPage : ContentPage {
        
        private readonly LocalDbService _dbService;
        
        public MainPage(LocalDbService dbService) {
            InitializeComponent();
            _dbService = dbService;

            // test
            Medication med = new Medication {
                MedName = "Adderall",
                Dosage = "9",
                MedInstructions = "Yes",
                TimeTaken = "3:00PM",
                DaysTaken = "MWF"
            };

            // new row
            Task.Run(async() => await _dbService.Create(med));

            // get all rows in DB and bind to MedicationListView ItemSource
            Task.Run(async() => MedicationListView.ItemsSource = await _dbService.GetMedications());
            
        }
        
        private void OnEditButtonClicked(object sender, EventArgs args) {

            var button = sender as Button;
            var medication = (Medication)button.BindingContext;
            
            Navigation.PushAsync(new EditPage(medication, _dbService));
        }
        
        protected override async void OnAppearing() {
            // get current values from database everytime page appears, otherwise old data is shown after an edit
            base.OnAppearing();
            var medications = await _dbService.GetMedications();
            MedicationListView.ItemsSource = medications;
        }
    }


