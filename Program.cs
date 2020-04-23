using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie("Blood Diamond", "Leonardo DiCaprio", 8, 2006),
                new Movie("Gladiator", "Russell Crowe", 8.5, 2000),
                new Movie("The Departed", "Leonardo DiCaprio", 8.5, 2006),
                new Movie("A Beautiful Mind", "Russell Crowe", 8.2, 2001),
                new Movie("Good Will Hunting", "Matt Damon", 8.3, 1997),
                new Movie("The Martian", "Matt Damon", 8, 2015),
                new Movie("Fake Movie", "Fake Matt Damon", 8, 1997),
            };

            List<Actor> actors = new List<Actor>
            {
                new Actor { FullName = "Matt Damon", Age = 28 },
                new Actor { FullName = "Leonardo DiCaprio", Age = 44 },
            };

            Movie selectedMovie = movies.FirstOrDefault(m => m.Title == "abc");
            selectedMovie = movies.FirstOrDefault(movie => movie.Title == "The Martian");
            // Console.WriteLine("selected movie: " + selectedMovie);


            // int oldestMovieYear = movies.Min(movie => movie.Year);
            // Movie oldestMovie = movies.FirstOrDefault(movie => movie.Year == oldestMovieYear);
            // IEnumerable<Movie> moviesFrom1997 = movies.Where(movie => movie.Year == oldestMovieYear);

            // 2 ways to do get moviesFrom1997  ⬆⬇

            IEnumerable<Movie> moviesFrom1997 = movies.Where(movie => movie.Year == movies.Min(m => m.Year));

            // Console.WriteLine("oldest movie: " + oldestMovie);



            List<Movie> moviesOrderedByTitle = movies.OrderBy(movie => movie.Title).ToList();

            IEnumerable<Movie> orderedMoviesByLeo = movies
                .Where(movie => movie.LeadActor == "Leonardo DiCaprio")
                .OrderBy(movie => movie.Title);

            IEnumerable<Movie> filteredMovies = movies.Where(m => m.Title.StartsWith("T") && m.Year == 2006);

            //  .Select Examples
            IEnumerable<string> selected = movies.Select(m => m.Title);

            selected = movies
                .Where(m => m.LeadActor == "Russell Crowe" && m.Year > 2000)
                .Select(m => m.Title);

            selected = movies
                .Where(m => m.LeadActor == "Matt Damon")
                .Select(m => m.Title)
                .OrderBy(title => title)
                .ToList();

            var moviesAndActors = movies
                .Join(actors, // join movies with actors list
                    movie => movie.LeadActor, // movie.LeadActor ==
                    actor => actor.FullName, // actor.FullName
                    (movie, actor) => new { movie, actor } // return new dict with movie and actor inside
                ).Where(movieAndActorDict => movieAndActorDict.actor.FullName == "Leonardo DiCaprio")
                .ToList();


            PrintEach(moviesAndActors);
        }

        public static void PrintEach(IEnumerable<dynamic> items, string msg = "")
        {
            Console.WriteLine("\n");
            Console.WriteLine(msg);

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}