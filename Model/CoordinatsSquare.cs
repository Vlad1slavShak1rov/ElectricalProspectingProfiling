using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public double XКоордината { get; set; }

        [Required]
        public double YКоордината { get; set; }

        [ForeignKey("Square")]
        public int SquareID { get; set; }

        public virtual Square Square { get; set; }
    }
}
