using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class Exam
    {
        public Exam()
        {
            ExamSubject = new HashSet<ExamSubject>();
        }

        public int ExamId { get; set; }
        public string ExamDesc { get; set; }
        public int ExamYear { get; set; }
        public string ExamTerm { get; set; }

        public virtual ICollection<ExamSubject> ExamSubject { get; set; }
    }
}
