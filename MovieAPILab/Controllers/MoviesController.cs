using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPILab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPILab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDBContext context; //EF guide

        public MoviesController(MovieDBContext _context)
        {
            context = _context;
        }

        #region ADD/CREATE
        //POST: api/Movie/
        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { Id = movie.Id }, movie);
        }

        #endregion

        #region READ
        //GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            var movie = await context.Movies.ToListAsync();
            return movie;
        }

        //GET: api/Movie/1
        [HttpGet("{Id}")] 
        public async Task<ActionResult<Movie>> GetMovie(int Id) 
        {
            var movie = await context.Movies.FindAsync(Id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return movie;
            }
        }
        #endregion
        
        #region UPDATE
        //PUT: api/Movie/{Id}
        [HttpPut("{Id}")]
        public async Task<ActionResult> PutMovie(int Id, [FromBody] Movie movie)
        {
            if (Id != movie.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Movie dbMovie = context.Movies.Find(Id);
                dbMovie.Title = movie.Title;
                dbMovie.Genre = movie.Genre;
                dbMovie.Runtime = movie.Runtime;

                context.Entry(dbMovie).State = EntityState.Modified;
                context.Update(dbMovie);
                await context.SaveChangesAsync();
                return NoContent();
            }
        }
        #endregion

        #region DELETE
        //DELETE: api/Movie/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int Id)
        {
            var movie = await context.Movies.FindAsync(Id);

            if(movie == null)
            {
                return NotFound();
            }
            else
            {
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
                return NoContent();
            }
        }
        #endregion
    }
}
