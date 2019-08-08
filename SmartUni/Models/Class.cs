using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class Class
    {
        public Class()
        {
            Student = new HashSet<Student>();
        }

        [DisplayName("Class ID")]
        public int ClassId { get; set; }
        [Required]
        [DisplayName("Class Name")]
        public string ClassDesc { get; set; }
        [Required]
        [DisplayName("Study Level")]
        public int StudyLevelId { get; set; }
        [Required]
        [DisplayName("Tutor")]
        public int TutorId { get; set; }
        [Required]
        public int Year { get; set; }

        public virtual StudyLevel StudyLevel { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
