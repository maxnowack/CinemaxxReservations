// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Movie.cs" company="Max Nowack">
//   copyright notice
// </copyright>
// <summary>
//   Defines the Movie type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MN.Cinemaxx.Reservations
{
    using System;

    /// <summary>
    /// The movie.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The id.
        /// </summary>
        private readonly string id;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets the reservations.
        /// </summary>
        public long Reservations 
        { 
            get { return GetReservations(this.id); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public Movie(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// The get reservations.
        /// </summary>
        /// <param name="movieId">
        /// The movie Id.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        private static long GetReservations(string movieId)
        {
            throw new NotImplementedException();
        }
    }
}
