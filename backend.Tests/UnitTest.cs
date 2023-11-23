using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using backend.Controllers;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace backend.Tests
{
    [TestClass]
    public class MovieControllerTests
    {
        private readonly Fixture _fixture;

        public MovieControllerTests()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task GetMovies_ReturnsListOfMovies()
        {
            var movies = _fixture.CreateMany<Movie>().ToList();
            var movieRepositoryMock = new Mock<IMovieRepository>();
            var movieServiceMock = new Mock<IMovieService>();

            movieRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(movies);

            var controller = new MovieController(movieRepositoryMock.Object, movieServiceMock.Object);

            var result = await controller.GetMovies();

            Assert.IsNotNull(result);

            CollectionAssert.AllItemsAreInstancesOfType((System.Collections.ICollection)result, typeof(Movie));

            var model = (IEnumerable<Movie>)result;

            Assert.AreEqual(movies.Count, model.Count());
            CollectionAssert.AreEquivalent(movies, model.ToList());
        }

        [TestMethod]
        public async Task GetMovie_ReturnsMovie()
        {
            var movieId = _fixture.Create<int>();
            var movie = _fixture.Create<Movie>();
            var movieRepositoryMock = new Mock<IMovieRepository>();

            movieRepositoryMock.Setup(repo => repo.Get(movieId)).ReturnsAsync(movie);

            var controller = new MovieController(movieRepositoryMock.Object, Mock.Of<IMovieService>());

            var result = await controller.Get(movieId);

            Assert.IsInstanceOfType(result, typeof(ActionResult<Movie>));
            var actionResult = (ActionResult<Movie>)result;

            Assert.AreEqual(movie, actionResult.Value);
        }

        [TestMethod]
        public async Task EditMovie_ReturnsNoContent()
        {
            var movieId = _fixture.Create<int>();
            var movie = _fixture.Create<Movie>();
            movie.Id = movieId;

            var movieRepositoryMock = new Mock<IMovieRepository>();

            movieRepositoryMock.Setup(repo => repo.Update(movie)).Returns(Task.CompletedTask);

            var controller = new MovieController(movieRepositoryMock.Object, Mock.Of<IMovieService>());

            var result = await controller.EditMovie(movieId, movie);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));

            movieRepositoryMock.Verify(repo => repo.Update(movie), Times.Once);
        }


        [TestMethod]
        public async Task Delete_ReturnsNoContent()
        {
            var movieId = _fixture.Create<int>();
            var movieRepositoryMock = new Mock<IMovieRepository>();

            movieRepositoryMock.Setup(repo => repo.Get(movieId)).ReturnsAsync(_fixture.Create<Movie>());

            var controller = new MovieController(movieRepositoryMock.Object, Mock.Of<IMovieService>());

            var result = await controller.Delete(movieId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task CreateMovie_ReturnsCreatedAtAction()
        {
            // Arrange
            var movieToCreate = _fixture.Create<Movie>();
            movieToCreate.Title = "Test";
            movieToCreate.ReleaseYear = 1922;

            var createdMovie = _fixture.Create<Movie>();
            createdMovie.Id = 1;
            createdMovie.Title = "Test";
            createdMovie.ReleaseYear = 1922;
            createdMovie.Active = true;
            createdMovie.ExternalId = null;

            var movieRepositoryMock = new Mock<IMovieRepository>();
            movieRepositoryMock.Setup(repo => repo.Create(movieToCreate)).ReturnsAsync(createdMovie);

            var controller = new MovieController(movieRepositoryMock.Object, Mock.Of<IMovieService>());

            var result = await controller.CreateMovie(movieToCreate);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Movie>));
        }


    }
}
