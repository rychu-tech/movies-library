using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Contexts
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options): base(options) {
            
        }
        public DbSet<Movie> movies { get; set; }

    }
}
