using MovieAPILab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class MovieDAL
    {
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            return client;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var client = GetHttpClient();

            var response = await client.GetAsync("/api/Movies");
            //install-package Microsoft.AspNet.WebAPI.Client
            var movies = await response.Content.ReadAsAsync<List<Movie>>();
            return movies;
        }

        public async Task<Movie> GetMovie(int Id)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"/api/Movies/{Id}");
            var movie = await response.Content.ReadAsAsync<Movie>();
            return movie;
        }

        public async Task AddMovie(Movie movie)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("/api/Movies", movie);
        }

        public async Task DeleteMovie(int Id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"/api/Movies/{Id}");
        }

        public async Task EditMovie(Movie newMovie, int Id)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync($"/api/Movies/{Id}", newMovie);
        }
    }
}
