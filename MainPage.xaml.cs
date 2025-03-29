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
    }


