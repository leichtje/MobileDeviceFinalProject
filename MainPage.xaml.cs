namespace MobileDeviceFinalProject
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            // onInit() --> retrieve db info and populate rows, get user profile info 
        }


        private void onNewMedicationButtonClicked(object sender, EventArgs e) {
            // push add medication page
        }

        private void onInfoButtonClicked(object sender, EventArgs e) {
            // get row from db
            // push info page, give it row info and display it 
        }

        private void onEditButtonClicked(object sender, EventArgs e) {
            // get row from db
            // push new edit page with info from row  
        }

        private void onDeleteButtonClicked(object sender, EventArgs e) {
            // get row from db 
            // push new "confirm" pop up page with details from db row. (or just don't confirm and nuke the entry?)
            // delete db row if "yes" button clicked. 
            // close pop up and do nothing if "no" is clicked
        }


    }

}
