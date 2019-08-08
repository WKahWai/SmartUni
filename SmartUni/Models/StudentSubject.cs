using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartUni.Models
{
    public partial class StudentSubject
    {
        public StudentSubject()
        {
            ExamSubject = new HashSet<ExamSubject>();
        }

        public int StudSubjectId { get; set; }
        [DisplayName("Student ID")]
        public int StudId { get; set; }
        [DisplayName("Subject ID")]
        public int SubjectId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<ExamSubject> ExamSubject { get; set; }
    }
}
