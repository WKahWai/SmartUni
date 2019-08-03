using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class StudentSubject
    {
        public StudentSubject()
        {
            ExamSubject = new HashSet<ExamSubject>();
        }

        public int StudSubjectId { get; set; }
        public string StudId { get; set; }
        public int SubjectId { get; set; }

        public virtual Student Stud { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<ExamSubject> ExamSubject { get; set; }
    }
}
