namespace KutuphaneYonetımSıstemı.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string genre_name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
