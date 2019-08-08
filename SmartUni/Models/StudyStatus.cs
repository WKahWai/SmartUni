using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class StudyStatus
    {
        public StudyStatus()
        {
            Student = new HashSet<Student>();
        }
        [DisplayName("Study Status ID")]
        public int StudyStatusId { get; set; }
        [Required]
        [DisplayName("Study Status Description")]
        public string StudyStatusDesc { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
