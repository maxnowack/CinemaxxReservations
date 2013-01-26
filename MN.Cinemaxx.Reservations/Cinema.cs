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

    /// <summary>
    /// The cinema.
    /// </summary>
    public class Cinema
    {
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
            var retVal = new List<Movie>();
            return retVal;
        }
    }
}
