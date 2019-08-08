using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class TutorType
    {
        public TutorType()
        {
            Tutor = new HashSet<Tutor>();
        }

        [DisplayName("Tutor Type ID")]
        public int TutorTypeId { get; set; }
        [Required]
        [DisplayName("Tutor Type Description")]
        public string TutorTypeDesc { get; set; }

        public virtual ICollection<Tutor> Tutor { get; set; }
    }
}
