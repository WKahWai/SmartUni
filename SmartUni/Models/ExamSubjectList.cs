using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class ExamSubjectList
    {
        public int ExamId { get; set; }

        public List<ExamSubject> ExamSubjectListing { get; set; }
    }
}
