using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class SelectedStudentList
    {
        public int StudId { get; set; }
        public string StudName { get; set; }
        public int ClassId { get; set; }
        public int StudSubjectId { get; set;  }
        public bool Selected { get; set; }
    }
}
