using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class TutorType
    {
        public TutorType()
        {
            Tutor = new HashSet<Tutor>();
        }

        public int TutorTypeId { get; set; }
        public string TutorTypeDesc { get; set; }

        public virtual ICollection<Tutor> Tutor { get; set; }
    }
}
