using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class GeologicalData
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Geodesist")]
        public int ГеодезистID { get; set; }

        [Required]
        public string ТипПороды { get; set; }

        public string ОписаниеСтруктуры { get; set; }

        public string Загрязнение { get; set; }

        public string Info { get => $"Номер исследования {ID}"; }
        public virtual List<Measurement> Measurements { get; set; }
        public virtual Geodesist Geodesist { get; set; }
    }

}
