using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class ExamStudentList
    {
        public int StudId { get; set; }
        public string StudName { get; set; }
        public int StudSubjectId { get; set; }
        public int ExamId { get; set; }
        public int? Mark { get; set; }
        public string Grade { get; set; }
    }
}
