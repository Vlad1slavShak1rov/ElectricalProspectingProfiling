using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class CoordinatsSquare
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public decimal XКоордината { get; set; }
        [Required]
        public decimal YКоордината { get; set; }

        public virtual Square Square { get; set; }
    }
}
