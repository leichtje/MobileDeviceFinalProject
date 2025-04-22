using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace MobileDeviceFinalProject
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class NotificationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var title = intent.GetStringExtra("title");
            var message = intent.GetStringExtra("message");

            Log.Info("NotificationReceiver", "Alarm triggered for: " + title);

            if (IsAppInForeground(context))
            {
                // Display an in-app alert since notifications may not be visible in foreground
                ShowInAppAlert(context, title, message);
            }
            else
            {
                // Show standard Android notification
                ShowNotification(context, title, message);
            }
        }

        private bool IsAppInForeground(Context context)
        {
            var activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            var appProcesses = activityManager.RunningAppProcesses;

            if (appProcesses != null)
            {
                var packageName = context.PackageName;
                foreach (var appProcess in appProcesses)
                {
                    if (appProcess.ProcessName == packageName && appProcess.Importance == Importance.Foreground)
                    {
                        return true; // App is running in foreground
                    }
                }
            }
            return false; // App is not foregrounded
        }

        private void ShowInAppAlert(Context context, string title, string message)
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            intent.PutExtra("title", title);
            intent.PutExtra("message", message);

            context.StartActivity(intent);
        }

        private void ShowNotification(Context context, string title, string message)
        {
            var notificationManager = NotificationManager.FromContext(context);

            var notification = new Notification.Builder(context, "default_channel_id")
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Android.Resource.Drawable.IcDialogAlert)
                .SetAutoCancel(true)
                .Build();

            notificationManager.Notify(1, notification);
        }
    }
}