using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> SearchAndFilter(string searchString, List<string> ratings)
        {
            if (searchString == null && ratings.Count == 0)
                return All;

            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                //Case 1: Search string and ratings
                if(ratings.Count > 0 && searchString != null)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) && ratings.Contains(movie.MPAA_Rating))
                        results.Add(movie);
                }
                //Case 2: Search string only
                else if(searchString != null)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                        results.Add(movie);
                }
                //case 3: Ratings only
                else if(ratings.Count > 0)
                {
                    if (ratings.Contains(movie.MPAA_Rating))
                        results.Add(movie);
                }
                
            }
            return results;
        }
    }
}
