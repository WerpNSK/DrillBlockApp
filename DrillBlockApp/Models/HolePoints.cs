using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrillBlockApp.Models
{
    public class HolePoints
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int X { get; set; }
        [Required]
        public int Y { get; set; }
        [Required]
        public int Z { get; set; }
        [ForeignKey("Hole")]
        public int HoleId { get; set; }
        public Hole Hole { get; set; }
    }
}
