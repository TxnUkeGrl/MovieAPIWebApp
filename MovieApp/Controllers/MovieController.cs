using Microsoft.AspNetCore.Mvc;
using MovieAPILab.Models;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieDAL _movie = new MovieDAL();

        public async Task<IActionResult> Index()
        {
            var movie = await _movie.GetMovies();
            return View(movie);
        }

        public IActionResult MovieForm()
        {
            return View(new Movie());
        }

        public async Task<IActionResult> AddMovie(Movie movie)
        {
            if(ModelState.IsValid)
            {
                await _movie.AddMovie(movie);
                return RedirectToAction("Index");
            }
            return View("AddMovie", movie);
        }

        public async Task<IActionResult> DeleteMovie(int Id)
        {
            await _movie.DeleteMovie(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditMovie(int Id)
        {
            var movie = await _movie.GetMovie(Id);
            return View("MovieForm", movie);
        }

        [HttpPost]
        public async Task<IActionResult> EditMovie(int Id, Movie editedMovie)
        {
            if(ModelState.IsValid)
            {
                await _movie.EditMovie(editedMovie, Id);
            }
            return RedirectToAction("Index");
        }
    }
}

       

