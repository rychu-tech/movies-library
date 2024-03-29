﻿using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieService _movieService;
        public MovieController(IMovieRepository movieRepository, IMovieService movieService) {
            _movieRepository = movieRepository;
            _movieService = movieService;
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
        [HttpPut("{MovieId}")]
        public async Task<ActionResult> EditMovie(int MovieId, [FromBody] Movie movie)
        {
            if (MovieId != movie.Id)
            {
                return BadRequest();
            }
            await _movieRepository.Update(movie);
            return NoContent();
        }

        [HttpDelete("{MovieId}")]
        public async Task<ActionResult> Delete(int MovieId)
        {
            var movieToDelete = await _movieRepository.Get(MovieId);
            if (movieToDelete == null)
            {
                return NotFound();
            }
            await _movieRepository.Delete(movieToDelete.Id);
            return NoContent();
        }

        [HttpPatch("{MovieId}")]
        public async Task<ActionResult> Restore(int MovieId)
        {
            var movieToRestore = await _movieRepository.Get(MovieId);
            if (movieToRestore == null)
            {
                return NotFound();
            }
            await _movieRepository.Restore(movieToRestore.Id);
            return NoContent();
        }

        [HttpGet("import")]
        public async Task<ActionResult<IEnumerable<Movie>>> ImportMoviesFromExternalApi()
        {
            try
            {
                var createdMovies = await _movieService.ImportFromExternalApi();

                return CreatedAtAction(nameof(GetMovies), createdMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during movie import: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during movie import.");
            }
        }
    }
}
