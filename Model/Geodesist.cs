using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class Geodesist
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Имя { get; set; }
        [Required]
        public string Контакты { get; set; }
    }
}
