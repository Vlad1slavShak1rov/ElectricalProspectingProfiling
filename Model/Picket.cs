using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class Picket
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Profile")]
        public int ПрофильID { get; set; }

        [Required]
        public string Координаты { get; set; }

        public int Номер { get; set; }

        public decimal Дистанция { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual List<Measurement> Measurements { get; set; }
    }
}
