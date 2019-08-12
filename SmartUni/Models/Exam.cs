using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class Exam
    {
        public Exam()
        {
            ExamSubject = new HashSet<ExamSubject>();
        }

        [DisplayName("Exam ID")]
        public int ExamId { get; set; }
        [Required]
        [DisplayName("Exam Description")]
        public string ExamDesc { get; set; }
        [Required]
        [DisplayName("Year")]
        [RegularExpression("^(2019|202[0-5])$",
        ErrorMessage = "Year must be valid integer within the range 2019 and 2025.")]
        public int ExamYear { get; set; }
        [Required]
        [DisplayName("Term")]
        public string ExamTerm { get; set; }

        public virtual ICollection<ExamSubject> ExamSubject { get; set; }
    }
}
