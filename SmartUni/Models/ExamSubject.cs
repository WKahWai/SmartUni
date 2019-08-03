using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class ExamSubject
    {
        public int ExamId { get; set; }
        public int StudSubjectId { get; set; }
        public int? Mark { get; set; }
        public string Grade { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual StudentSubject StudSubject { get; set; }
    }
}
