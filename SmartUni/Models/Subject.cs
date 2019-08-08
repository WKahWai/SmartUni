using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class Subject
    {
        public Subject()
        {
            StudentSubject = new HashSet<StudentSubject>();
        }

        [DisplayName("Subject ID")]
        public int SubjectId { get; set; }
        [Required]
        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }
        [Required]
        [DisplayName("Tutor")]
        public int TutorId { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual ICollection<StudentSubject> StudentSubject { get; set; }
    }
}
