using backend.Models;

namespace backend.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Get();
        Task<Movie> Get(int movieId);
        Task<Movie> Create(Movie movie);
        Task Update(Movie movie);
        Task Delete(int movieId);
    }
}
