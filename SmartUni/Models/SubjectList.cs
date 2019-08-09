using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUni.Models
{
    public class SubjectList
    {
        [Key]
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }
    }
}
