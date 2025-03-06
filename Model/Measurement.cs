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

        [ForeignKey("Picket")]
        public int ПикетID { get; set; }

        public DateTime Дата { get; set; }

        [Required]
        public string ТипПрофилирования { get; set; }

        public decimal ДистанцияМеждуЭлектродами { get; set; }

        public decimal Ток { get; set; }

        public decimal Вольтаж { get; set; }

        public decimal Сопротивление { get; set; }

        public decimal Температура { get; set; }

        public virtual Picket Picket { get; set; }
        public virtual List<GeologicalData> GeologicalDataList { get; set; }
    }

}
