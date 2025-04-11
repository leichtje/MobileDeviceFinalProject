namespace MobileDeviceFinalProject;

public partial class AddMed : ContentPage
{
	public AddMed()
	{
      InitializeComponent();
      BindingContext = new Medication();
   }

   private async void OnBackClicked(object sender, EventArgs e)
   {
      await Navigation.PopAsync();
   }

   private void OnSaveClicked(object sender, EventArgs e)
   {
      
   }
}