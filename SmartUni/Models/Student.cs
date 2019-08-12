using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentSubject = new HashSet<StudentSubject>();
        }
        [DisplayName("Student ID")]
        public int StudId { get; set; }
        [Required]
        [DisplayName("Student Name")]
        public string StudName { get; set; }
        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Contact No.")]
        [RegularExpression("^\\d{10}$|\\d{11}$",
        ErrorMessage = "Contact No. must be within 10-11 digits.")]
        public int PhoneNo { get; set; }
        [Required]
        [DisplayName("Study Status")]
        public int StudyStatusId { get; set; }
        [Required]
        [DisplayName("Class")]
        public int ClassId { get; set; }

        public virtual Class Class { get; set; }
        public virtual StudyStatus StudyStatus { get; set; }
        public virtual ICollection<StudentSubject> StudentSubject { get; set; }
    }
}
