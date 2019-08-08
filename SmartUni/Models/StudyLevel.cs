using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
    public partial class StudyLevel
    {
        public StudyLevel()
        {
            Class = new HashSet<Class>();
        }

        [DisplayName("Study Level ID")]
        public int StudyLevelId { get; set; }
        [Required]
        [DisplayName("Study Level Description")]
        public string StudyLevelDesc { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}
