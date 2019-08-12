using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class ExamSubject
    {
        [DisplayName("Exam")]
        public int ExamId { get; set; }
        [DisplayName("Subject")]
        public int StudSubjectId { get; set; }
        [DisplayName("Mark")]
        [RegularExpression("^[0-9][0-9]?$|^100$",
        ErrorMessage = "Mark must be within the range 0 and 100.")]
        public int? Mark { get; set; }
        [DisplayName("Grade")]
        public string Grade { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual StudentSubject StudSubject { get; set; }
    }
}
