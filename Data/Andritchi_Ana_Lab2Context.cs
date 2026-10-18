using Microsoft.EntityFrameworkCore;
using Andritchi_Ana_Lab2.Models;

namespace Andritchi_Ana_Lab2.Data
{
    public class Andritchi_Ana_Lab2Context : DbContext
    {
        public DbSet<Andritchi_Ana_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public Andritchi_Ana_Lab2Context(DbContextOptions<Andritchi_Ana_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
    }
}