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

        [Column("name")]
        public string Name { get; set; }
        [Column("type")]
        public AssessmentType Type { get; set;}
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
