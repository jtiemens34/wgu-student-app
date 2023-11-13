using Plugin.LocalNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MobileDev
{
    public enum CourseStatus
    {
        InProgress, 
        Completed, 
        Dropped, 
        PlanToTake
    }
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("term_id")]
        public int TermId { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("course_status")]
        public CourseStatus CourseStatus { get; set; }
        [Column("credits")]
        public int Credits { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("instructor_name")]
        public string InstructorName { get; set; }
        [Column("instructor_phone")]
        public string InstructorPhone { get; set; }
        [Column("instructor_email")]
        public string InstructorEmail { get; set; }

        [Column("notes")]
        public string Notes { get; set; }
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
                        NotificationId = Id,
                        Title = Name + " course upcoming!",
                        Description = Name + " is starting soon!",
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
                        NotificationId = Id,
                        Title = Name + " course ending!",
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
