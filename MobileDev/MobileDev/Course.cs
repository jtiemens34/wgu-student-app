using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
