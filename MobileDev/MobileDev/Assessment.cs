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
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("start_notification_enabled")]
        public bool StartNotificationEnabled { get; set; }
        [Column("end_notification_enabled")]
        public bool EndNotificationEnabled { get; set; }

        public async Task AddNotification()
        {
            if (!StartNotificationEnabled && !EndNotificationEnabled) return;
            if (DeviceInfo.Current.Platform != DevicePlatform.WinUI)
            {
                if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
                    await LocalNotificationCenter.Current.RequestNotificationPermission();
                if (StartNotificationEnabled)
                {
                    var startNotification = new NotificationRequest
                    {
                        NotificationId = CourseId,
                        Title = Name + " upcoming!",
                        Description = Name + " is happening soon!",
                        ReturningData = "Dummy data",
                        Schedule =
                        {
                            NotifyTime = StartDate
                        }
                    };
                    await LocalNotificationCenter.Current.Show(startNotification);
                }
                if (EndNotificationEnabled)
                {
                    var endNotification = new NotificationRequest
                    {
                        NotificationId = CourseId,
                        Title = Name + " ending!",
                        Description = Name + " is ending soon!",
                        ReturningData = "Dummy data",
                        Schedule =
                        {
                            NotifyTime = EndDate
                        }
                    };
                    await LocalNotificationCenter.Current.Show(endNotification);
                }
            }
        }
    }
}
