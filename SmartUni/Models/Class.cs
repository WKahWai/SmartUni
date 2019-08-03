using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class Class
    {
        public Class()
        {
            Student = new HashSet<Student>();
        }

        public string ClassId { get; set; }
        public string ClassDesc { get; set; }
        public int StudyLevelId { get; set; }
        public string TutorId { get; set; }
        public int Year { get; set; }

        public virtual StudyLevel StudyLevel { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
