using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class Measurement
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("GeologicalData")]
        public int ГеологическиеДанныеID { get; set; }
        [ForeignKey("Picket")]
        public int ПикетыID { get; set; }
        public DateTime Дата { get; set; }

        [Required]
        public string ТипПрофилирования { get; set; }

        public decimal ДистанцияМеждуЭлектродами { get; set; }

        public decimal Ток { get; set; }

        public decimal Вольтаж { get; set; }

        public double Сопротивление { get; set; }


        public virtual Picket Picket { get; set; }
        public virtual GeologicalData GeologicalData { get; set; }
    }

}
