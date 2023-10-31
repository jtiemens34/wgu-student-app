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
    }
}
