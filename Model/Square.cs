using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ElectricalProspectingProfiling.Model
{
    public class Square
    {
        [Key]
        public int ID { get; set; }

        public int КоординатыID { get; set; } // Убрал ForeignKey, потому что связь с List

        [Required]
        public string Название { get; set; }

        [Required]
        public decimal Высота { get; set; }
        [Required]
        public byte? ФотоПлощади { get; set; }
        public virtual List<CoordinatsSquare> CoordinatsSquares { get; set; }
    }
}
