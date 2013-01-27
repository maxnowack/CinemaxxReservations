// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cinema.cs" company="Max Nowack">
//   copyright notice
// </copyright>
// <summary>
//   Defines the Cinema type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MN.Cinemaxx.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CsQuery;

    /// <summary>
    /// The cinema.
    /// </summary>
    public class Cinema
    {
        private const string ScheduleUrl = "http://cinemaxx.de/Programm/AktuellesProgramm?switchCinemaId={0}&WeekStart={1:yyyyMMdd}";

        /// <summary>
        /// The cinema id.
        /// </summary>
        private int cinemaId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cinema"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public Cinema(int id)
        {
            this.cinemaId = id;
        }

        /// <summary>
        /// The get movies.
        /// </summary>
        /// <param name="week">
        /// The week.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Movie> GetMovies(DateTime week)
        {
            var cq = CQ.CreateFromUrl(string.Format(ScheduleUrl, this.cinemaId, week));
            var movieLinks = cq.Select(".reserv");
            var ids = movieLinks.Select(movie => movie.Attributes["href"].Split('/').Last()).ToList();
            
            return ids.Distinct().Select(id => new Movie(id)).ToList();
        }
    }
}
