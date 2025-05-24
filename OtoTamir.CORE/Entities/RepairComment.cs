﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.CORE.Entities
{
    public class RepairComment:BaseEntity
    {
  
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int SymptomId{ get; set; }
        public Symptom Symptom { get; set; }

    }
}
