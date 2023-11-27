using System.Text.Json;
using backend.Contexts;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        public MovieRepository(MovieContext context) { 
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            _context.movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task Delete(int movieId)
        {
            // Soft delete
            var movieToDelete = await _context.movies.FindAsync(movieId);
            if (movieToDelete != null)
            {
                movieToDelete.Active = false;
                _context.Entry(movieToDelete).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Restore(int movieId)
        {
            var movieToRestore = await _context.movies.FindAsync(movieId);
            if (movieToRestore != null)
            {
                movieToRestore.Active = true;
                _context.Entry(movieToRestore).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Movie>> Get()
        {
            return await _context.movies.ToListAsync();  
        }

        public async Task<Movie> Get(int movieId)
        {
            return await _context.movies.FindAsync(movieId);
        }

        public async Task Update(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetByExternalId(int externalId)
        {
            return await _context.movies.FirstOrDefaultAsync(m => m.ExternalId == externalId);
        }
    }
}
