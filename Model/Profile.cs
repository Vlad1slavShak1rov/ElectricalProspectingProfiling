using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Model
{
    public class Profile
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Square")]
        public int ПлощадьID { get; set; }

        [ForeignKey("CoordinatsProfile")]
        public int КоординатыID { get; set; } 

        [Required]
        public string МетодПрофилирования { get; set; }

        public virtual Square Square { get; set; }
        public virtual CoordinatsProfile CoordinatsProfile { get; set; } 
        public virtual List<Picket> Pickets { get; set; }
    }

}
