using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class SelectedStudentList
    {
        [DisplayName("Student ID")]
        public int StudId { get; set; }
        [DisplayName("Student Name")]
        public string StudName { get; set; }
        [DisplayName("Class ID")]
        public int ClassId { get; set; }
        public int StudSubjectId { get; set;  }
        [DisplayName("Select")]
        public bool Selected { get; set; }
    }
}
