using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentAMovie.Models;
using RentAMovie.ViewModels;

namespace RentAMovie.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            return View(User.IsInRole(RoleName.CanManageMovie) ? "List" : "ReadOnlyList");
        }
        public ActionResult Detail(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(x => x.Id.Equals(id));
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult New()
        {

            var viewModel = new MovieFromViewModel
            {
                Genres = _context.Genres.ToList(),

            };
            return View("New", viewModel);


        }

        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFromViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("New", viewModel);
        }



        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleasedDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovie)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFromViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("New", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailability = movie.Stock;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDB.Name = movie.Name;
                movieInDB.GenreId = movie.GenreId;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.Stock = movie.Stock;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}