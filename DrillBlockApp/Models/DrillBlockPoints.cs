using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrillBlockApp.Models
{
    public class DrillBlockPoints
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("DrillBlock")]
        public int DrillBlockId { get; set; }
        public DrillBlock DrillBlock { get; set; }
        public int Sequence { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
