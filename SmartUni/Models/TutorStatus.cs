using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class TutorStatus
    {
        public TutorStatus()
        {
            Tutor = new HashSet<Tutor>();
        }

        [DisplayName("Tutor Status ID")]
        public int TutorStatusId { get; set; }
        [Required]
        [DisplayName("Tutor Status Description")]
        public string TutorStatusDesc { get; set; }

        public virtual ICollection<Tutor> Tutor { get; set; }
    }
}
