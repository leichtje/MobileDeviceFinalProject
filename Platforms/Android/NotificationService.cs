using Android.App;
using Android.Content;
using MobileDeviceFinalProject;

public class NotificationService : INotificationService
{
    public void ScheduleNotification(string title, string message, DateTime notifyTime)
    {
        var context = Android.App.Application.Context;

        // Create an Intent for the notification
        var intent = new Intent(context, typeof(NotificationReceiver));
        intent.PutExtra("title", title);
        intent.PutExtra("message", message);

        // Create a PendingIntent for AlarmManager
        var pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

        // Schedule the notification using AlarmManager
        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
        var triggerTime = (long)(notifyTime.ToUniversalTime() - DateTime.UnixEpoch).TotalMilliseconds;

        alarmManager?.SetExact(AlarmType.RtcWakeup, triggerTime, pendingIntent);
    }
}