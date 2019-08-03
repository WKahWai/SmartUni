using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class TutorStatus
    {
        public TutorStatus()
        {
            Tutor = new HashSet<Tutor>();
        }

        public int TutorStatusId { get; set; }
        public string TutorStatusDesc { get; set; }

        public virtual ICollection<Tutor> Tutor { get; set; }
    }
}
