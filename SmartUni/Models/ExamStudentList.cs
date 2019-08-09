using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class ExamStudentList
    {
        [DisplayName("Student ID")]
        public int StudId { get; set; }
        [DisplayName("Student Name")]
        public string StudName { get; set; }
        [Key]
        public int StudSubjectId { get; set; }
        [DisplayName("Exam ID")]
        public int ExamId { get; set; }
        [DisplayName("Mark")]
        public int? Mark { get; set; }
        [DisplayName("Grade")]
        public string Grade { get; set; }
    }
}
