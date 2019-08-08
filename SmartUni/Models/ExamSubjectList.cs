using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class ExamSubjectList
    {
        [DisplayName("Exam ID")]
        public int ExamId { get; set; }

        public List<ExamSubject> ExamSubjectListing { get; set; }
    }
}
