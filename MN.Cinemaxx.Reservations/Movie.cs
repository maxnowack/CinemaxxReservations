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
    using System.Globalization;
    using System.Linq;

    using CsQuery;

    /// <summary>
    /// The movie.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Gets the reservations.
        /// </summary>
        public long Reservations { get; set; }

        /// <summary>
        /// The buy url.
        /// </summary>
        private const string BuyUrl = "https://ticket.cinemaxx.de/portal/index.html?performanceOId={0}";

        /// <summary>
        /// The seat url.
        /// </summary>
        private const string SeatUrl = "https://ticket.cinemaxx.de/index.html?performanceOId={0}&intern=default&update=updateSeatingplan";

        /// <summary>
        /// The id.
        /// </summary>
        private readonly string id;

        public Movie()
        {
            
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

            var cq = CQ.CreateFromUrl(string.Format(BuyUrl, this.id));

            if (cq.Select(".http500").Length > 0)
            {
                this.Name = "Error";
                this.Reservations = 0;
                return;
            }

            this.Name = cq.Select("div#ticketInfos h3")[0].InnerText.Trim();

            var date = cq.Select("div#ticketInfos .date")[0].InnerText.Split(',')
                                                     .Last()
                                                     .Replace(" Uhr", string.Empty)
                                                     .Replace("&nbsp;", string.Empty)
                                                     .Replace("\t", string.Empty)
                                                     .Replace("\n", " ");
            this.Time = DateTime.Parse(date.Trim(), new CultureInfo("de-DE"));

            var res = CQ.CreateFromUrl(string.Format(SeatUrl, this.id));
            this.Reservations = res.Select("seat").Length;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }
    }
}
