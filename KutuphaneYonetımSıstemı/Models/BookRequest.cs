using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetımSıstemı.Models
{
    public class BookRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string book_name { get; set; }

        [Required]
        [StringLength(50)]
        public string book_author { get; set; }

        [Required]
        public int genreId { get; set; }
    }
}
