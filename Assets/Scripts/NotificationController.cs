using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("NotifInitialized"))
        {
            var group = new AndroidNotificationChannelGroup()
            {
                Id = "Main",
                Name = "Main notifications"
            };
            AndroidNotificationCenter.RegisterNotificationChannelGroup(group);

            var channel = new AndroidNotificationChannel()
            {
                Id = "notif01",
                Name = "Canal por defecto",
                Importance = Importance.Default,
                Description = "Es un canal y es por defecto",
                Group = "Main"
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            StartCoroutine(RequedPermission());

            PlayerPrefs.SetString("NotifInitialized", "Yes");
            PlayerPrefs.Save();
        }
        else
        {
            ScheduleNotifications();
        }
    }

    private IEnumerator RequedPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return new WaitForEndOfFrame();

        ScheduleNotifications();
    }

    private void ScheduleNotifications()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        AndroidNotification noti01 = new();
        noti01.Title = "Clicker";
        noti01.Text = "Lautaro Gerez";
        noti01.FireTime = System.DateTime.Now.AddMinutes(10);

        AndroidNotificationCenter.SendNotification(noti01, "notif01");
    }
}
