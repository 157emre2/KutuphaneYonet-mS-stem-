namespace KutuphaneYonetımSıstemı.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string book_name { get; set; }
        public string book_author { get; set; }
        public int genreId { get; set; }

        public Genre genre { get; set; }
    }
}
