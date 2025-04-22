namespace MobileDeviceFinalProject;

public interface INotificationService
{
    void ScheduleNotification(string title, string message, DateTime notifyTime);
}