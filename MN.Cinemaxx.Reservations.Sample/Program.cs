// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Max Nowack">
//   copyright notice
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MN.Cinemaxx.Reservations.Sample
{
    using System;

    using MN.Cinemaxx.Reservations;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var cinema = new Cinema(8);
            var movies = cinema.GetMovies(DateTime.Now);

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Time.ToString("dd.MM.yyyy HH:mm ") + movie.Name + ": " + movie.Reservations);
            }
        }
    }
}
