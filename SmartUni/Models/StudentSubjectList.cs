using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class StudentSubjectList
    {
        public List<StudentSubject> StudentSubjectListing { get; set; }
        public Subject Subject { get; set; }

        public Student Student { get; set; }

    }
}
