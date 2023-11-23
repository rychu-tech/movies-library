using backend.Models;

namespace backend.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> ImportFromExternalApi();
    }
}
