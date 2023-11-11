using Plugin.LocalNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev
{
    public enum AssessmentType
    {
        Performance,
        Objective
    }
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }
        [Column("completed")]
        public bool Completed { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("type")]
        public AssessmentType Type { get; set;}
        [Column("date")]
        public DateTime Date { get; set; }

        [Column("notification_enabled")]
        public bool NotificationEnabled { get; set; }

        public async Task AddNotification()
        {
            if (!NotificationEnabled) return;
            if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
            {
                if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
                    await LocalNotificationCenter.Current.RequestNotificationPermission();
                var notification = new NotificationRequest
                {
                    NotificationId = CourseId,
                    Title = Name + " upcoming!",
                    Description = Name + " is happening soon!",
                    ReturningData = "Dummy data",
                    Schedule =
                    {
                        NotifyTime = Date
                    }
                };
                await LocalNotificationCenter.Current.Show(notification);
            }
        }
    }
}
