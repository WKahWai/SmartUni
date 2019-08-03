using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class Tutor
    {
        public Tutor()
        {
            Class = new HashSet<Class>();
            Subject = new HashSet<Subject>();
        }

        public string TutorId { get; set; }
        public string TutorName { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public int TutorStatusId { get; set; }
        public int TutorTypeId { get; set; }

        public virtual TutorStatus TutorStatus { get; set; }
        public virtual TutorType TutorType { get; set; }
        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
    }
}
