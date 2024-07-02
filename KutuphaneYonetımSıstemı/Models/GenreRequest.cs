using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetımSıstemı.Models
{
    public class GenreRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string genre_name { get; set; }
    }
}
