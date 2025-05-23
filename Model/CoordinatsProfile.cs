﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class CoordinatsProfile
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public double XКоордината { get; set; }
        [Required]
        public double YКоордината { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
