using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrillBlockApp.Models
{
    public class Hole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Depth { get; set; }
        [ForeignKey("DrillBlock")]
        public int DrillBlockId { get; set; }
        public DrillBlock DrillBlock { get; set; }
    }
}
