using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class ClassStudentList
    {
        [Key]
        [DisplayName("Student ID")]
        public int StudId { get; set; }
        [DisplayName("Student Name")]
        public string StudName { get; set; }
        [DisplayName("Study Status")]
        public string StudyStatusDesc { get; set; }
    }
}
