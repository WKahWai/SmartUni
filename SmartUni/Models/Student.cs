using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentSubject = new HashSet<StudentSubject>();
        }

        public string StudId { get; set; }
        public string StudName { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public int StudyStatusId { get; set; }
        public string ClassId { get; set; }

        public virtual Class Class { get; set; }
        public virtual StudyStatus StudyStatus { get; set; }
        public virtual ICollection<StudentSubject> StudentSubject { get; set; }
    }
}
