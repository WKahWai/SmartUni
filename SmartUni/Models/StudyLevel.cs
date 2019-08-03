using System;
using System.Collections.Generic;

namespace SmartUni.Models
{
    public partial class StudyLevel
    {
        public StudyLevel()
        {
            Class = new HashSet<Class>();
        }

        public int StudyLevelId { get; set; }
        public string StudyLevelDesc { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}
