using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public MovieDatabase movieDatabase = new MovieDatabase();

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> mpaa { get; set; } = new List<string>();
        
        [BindProperty]
        public float minIMDB { get; set; }

        [BindProperty]
        public float maxIMDB { get; set; }

        [BindProperty]
        public string order { get; set; }

        public IEnumerable<Movie> Movies;

        public void OnGet()
        {
            Movies = movieDatabase.All.OrderBy(movie => movie.Title);
        }

        public void OnPost(string search, List<string> rating, float minIMDB, float maxIMDB, string order)
        {
            Movies = movieDatabase.SearchAndFilter(search, rating);

            Movies = movieDatabase.All;

            if(search != null)
            {
                Movies = Movies.Where(movie => movie.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if(mpaa.Count() != 0)
            {
                Movies = Movies.Where(movie => mpaa.Contains(movie.MPAA_Rating));
            }

            if(minIMDB != null)
            {
                Movies = Movies.Where(movie => movie.IMDB_Rating != null && movie.IMDB_Rating >= minIMDB);
            }

            if (maxIMDB != null)
            {
                Movies = Movies.Where(movie => movie.IMDB_Rating != null && movie.IMDB_Rating >= maxIMDB);
            }

            if(order != null)
            {
                switch (order)
                {
                    case "title":
                        Movies.OrderBy(movie => movie.Title);
                        break;
                    case "director":
                        Movies.OrderBy(movie => movie.Director);
                        break;
                    case "year":
                        Movies.OrderBy(movie => movie.Year);
                        break;
                    case "imdb":
                        Movies.OrderBy(movie => movie.IMDB_Rating);
                        break;
                }
            }
        }
    }
}
