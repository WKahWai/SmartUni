using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartUni.Models
{
    public partial class ExamSubject
    {
        [DisplayName("Exam ID")]
        public int ExamId { get; set; }
        public int StudSubjectId { get; set; }
        [DisplayName("Mark")]
        public int? Mark { get; set; }
        [DisplayName("Grade")]
        public string Grade { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual StudentSubject StudSubject { get; set; }
    }
}
