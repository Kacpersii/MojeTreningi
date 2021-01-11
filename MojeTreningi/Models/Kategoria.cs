using System.ComponentModel.DataAnnotations;

namespace MojeTreningi.Models
{
    public class Kategoria
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nazwa { get; set; }
    }
}