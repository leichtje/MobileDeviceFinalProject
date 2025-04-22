using Android.App;
using Android.Content;
using Android.OS;
using MobileDeviceFinalProject;
using Application = Android.App.Application;

public class AndroidNotificationService : INotificationService
{
    public void ScheduleNotification(string title, string message, DateTime notifyTime)
    {
        var context = Application.Context;

        // Create an intent for the notification
        var intent = new Intent(context, typeof(NotificationReceiver));
        intent.PutExtra("title", title);
        intent.PutExtra("message", message);

        // Create a PendingIntent to trigger the broadcast
        var pendingIntent = PendingIntent.GetBroadcast(
            context, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

        // Schedule the notification using AlarmManager
        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
        var triggerTime = (long)(notifyTime.ToUniversalTime() - DateTime.UnixEpoch).TotalMilliseconds;

        alarmManager?.SetExact(AlarmType.RtcWakeup, triggerTime, pendingIntent);
    }
}