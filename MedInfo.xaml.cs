namespace MobileDeviceFinalProject;

public partial class MedInfo : ContentPage
{
	public MedInfo(Medication med)
	{
		InitializeComponent();
		BindingContext = med;
	}

   private async void OnBackClicked(object sender, EventArgs e)
   {
      await Navigation.PopAsync();
   }
}