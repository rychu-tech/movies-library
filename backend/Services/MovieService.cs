using backend.Models;
using backend.Repositories;
using System.Text.Json;

namespace backend.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<Movie>> ImportFromExternalApi()
        {
            const string apiUrl = "https://filmy.programdemo.pl/MyMovies";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var fetchedMovies = JsonSerializer.Deserialize<ExternalMovie[]>(json);
                        var createdMovies = new List<Movie>();
                        foreach (var externalMovieInfo in fetchedMovies)
                        {
                            var existingMovie = await _movieRepository.GetByExternalId(externalMovieInfo.id);
                            if (existingMovie == null)
                            {
                                var newMovie = MovieMapper.MapToMovie(externalMovieInfo);
                                newMovie = await _movieRepository.Create(newMovie);
                                createdMovies.Add(newMovie);
                            }
                            else
                            {
                                Console.WriteLine($"Movie with ExternalId {externalMovieInfo.id} already exists.");
                            }
                        }
                        return createdMovies;
                    }
                    else
                    {
                        throw new Exception($"Error fetching data");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
