namespace MobileDeviceFinalProject;

    public partial class MainPage : ContentPage {
        
        private readonly LocalDbService _dbService;
        
        public MainPage(LocalDbService dbService) {
            InitializeComponent();
            _dbService = dbService;
        }
    }


