using Android.App;
using Android.OS;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        CreateNotificationChannel();

        var intent = Intent;
        if (intent != null && intent.HasExtra("title") && intent.HasExtra("message"))
        {
            var title = intent.GetStringExtra("title");
            var message = intent.GetStringExtra("message");
            ShowAlert(title, message);
        }
    }

    private void CreateNotificationChannel()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(
                "default_channel_id",
                "General Notifications",
                NotificationImportance.Default)
            {
                Description = "App notifications"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }

    private void ShowAlert(string title, string message)
    {
        var dialog = new AlertDialog.Builder(this)
            .SetTitle(title)
            .SetMessage(message)
            .SetPositiveButton("OK", (sender, args) => { })
            .Create();

        dialog.Show();
    }
}