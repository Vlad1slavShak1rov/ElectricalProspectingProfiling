using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class Contract
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Client")]
        public int КлиентID { get; set; }

        [ForeignKey("GeologicalData")]
        public int ГеологическиеДанныеID { get; set; }

        [ForeignKey("Square")]
        public int ПлощадьID { get; set; }

        [Required]
        public string Контакты { get; set; }

        [Required]
        public DateTime НачалоДата { get; set; }

        [Required]
        public DateTime КонецДата { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual GeologicalData GeologicalData { get; set; }
        public virtual Square Square { get; set; }
    }
}
