using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class Tutor
    {
        public Tutor()
        {
            Class = new HashSet<Class>();
            Subject = new HashSet<Subject>();
        }

        [DisplayName("Tutor ID")]
        public int TutorId { get; set; }

        [Required]
        [DisplayName("Tutor Name")]
        public string TutorName { get; set; }
        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Contact No.")]
        [RegularExpression("^\\d{10}$|\\d{11}$",
        ErrorMessage = "Contact No. must be within 10-11 digits.")]
        public int PhoneNo { get; set; }
        [Required]
        [DisplayName("Tutor Status")]
        public int TutorStatusId { get; set; }
        [Required]
        [DisplayName("Tutor Type")]
        public int TutorTypeId { get; set; }

        public virtual TutorStatus TutorStatus { get; set; }
        public virtual TutorType TutorType { get; set; }
        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
    }
}
