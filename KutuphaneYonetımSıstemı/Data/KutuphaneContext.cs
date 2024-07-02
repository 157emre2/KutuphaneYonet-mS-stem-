using KutuphaneYonetımSıstemı.Models;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetımSıstemı.Data
{
    public class KutuphaneContext : DbContext
    {
        public KutuphaneContext(DbContextOptions<KutuphaneContext> options) : base(options) {}
        public DbSet<Book> Book { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}
