using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using RentAMovie.Dtos;
using RentAMovie.Models;

namespace RentAMovie.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies(string query = null,bool availability = false)
        {
            var moviesQuery = _context.Movies.Include(m => m.Genre);

            if (!query.IsNullOrWhiteSpace())
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

            if (availability)
                moviesQuery = moviesQuery.Where(m => m.NumberAvailability > 0);

           var movies = moviesQuery.ToList().
                     Select(Mapper.Map<Movie, MovieDto>);

            return movies;
        }

        //GET /api/movies/1
        public IHttpActionResult GetMoviesId(int id)
        {
            var moviesInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (moviesInDb == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(moviesInDb));
        }

        //POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.NumberAvailability = movie.Stock;
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        //PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var moviesInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (moviesInDb == null)
                return NotFound();


            Mapper.Map(movieDto, moviesInDb);

            _context.SaveChanges();
            return Ok();

        }

        //DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();

        }
    }
}
