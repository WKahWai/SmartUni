using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class Subject
    {
        public Subject()
        {
            StudentSubject = new HashSet<StudentSubject>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TutorId { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual ICollection<StudentSubject> StudentSubject { get; set; }
    }
}
