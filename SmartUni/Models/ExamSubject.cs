using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartUni.Models
{
    public partial class ExamSubject
    {
        [DisplayName("Exam")]
        public int ExamId { get; set; }
        [DisplayName("Subject")]
        public int StudSubjectId { get; set; }
        [DisplayName("Mark")]
        public int? Mark { get; set; }
        [DisplayName("Grade")]
        public string Grade { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual StudentSubject StudSubject { get; set; }
    }
}
