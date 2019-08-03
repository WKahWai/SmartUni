using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class StudyStatus
    {
        public StudyStatus()
        {
            Student = new HashSet<Student>();
        }

        public int StudyStatusId { get; set; }
        public string StudyStatusDesc { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
