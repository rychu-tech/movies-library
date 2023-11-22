using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository) {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _movieRepository.Get();
        }

        [HttpGet("{MovieId}")]
        public async Task<ActionResult<Movie>> Get(int movieId)
        {
            return await _movieRepository.Get(movieId);
        }
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            var newMovie = await _movieRepository.Create(movie);
            return CreatedAtAction(nameof(GetMovies), new {Id = newMovie.Id}, newMovie);
        }
        [HttpPut("{movieId}")]
        public async Task<ActionResult> EditMovie(int movieId, [FromBody] Movie movie)
        {
            if (movieId != movie.Id)
            {
                return BadRequest();
            }
            await _movieRepository.Update(movie);
            return NoContent();
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> Delete(int movieId)
        {
            var movieToDelete = await _movieRepository.Get(movieId);
            if (movieToDelete == null)
            {
                return NotFound();
            }
            await _movieRepository.Delete(movieToDelete.Id);
            return NoContent();
        }
    }
}
