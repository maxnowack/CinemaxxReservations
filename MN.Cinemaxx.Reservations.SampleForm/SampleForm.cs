// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Max Nowack">
//   copyright notice
// </copyright>
// <summary>
//   Defines the Form1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MN.Cinemaxx.Reservations.SampleForm
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Windows.Forms.DataVisualization.Charting;
    using System.Xml.Serialization;

    using CristiPotlog.ChartControl;

    /// <summary>
    /// The sample form.
    /// </summary>
    public partial class SampleForm : Form
    {
        private static string CacheFile { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleForm"/> class.
        /// </summary>
        public SampleForm()
        {
            this.InitializeComponent();
            CacheFile = Directory.GetCurrentDirectory() + "\\data.xml"; //Path.GetTempFileName();
        }

        /// <summary>
        /// The btn refresh_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var m = new Cinema(int.Parse("8")).GetMovies(this.dtpDay.Value);
            var serializer = new XmlSerializer(typeof(List<Movie>));
            var stream = new FileStream(CacheFile, FileMode.Truncate);
            serializer.Serialize(stream, m);
            stream.Close();

            this.GenerateChart();
        }

        /// <summary>
        /// The generate chart.
        /// </summary>
        private void GenerateChart()
        {
            var serializer = new XmlSerializer(typeof(List<Movie>));
            var m = new List<Movie>();

            var stream = new FileStream(CacheFile, FileMode.OpenOrCreate);
            if (stream.Length > 0)
            {
                m = (List<Movie>)serializer.Deserialize(stream);
                stream.Close();
            }
            else
            {
                stream.Close();
                this.btnRefresh_Click(new object(), new EventArgs());
            }
            
            var times = m.Where(x => x.Name != string.Empty && x.Time >= this.dtpDay.Value)
                         .GroupBy(x => x.Time)
                         .Select(mov => new { Time = mov.Key, Reservations = mov.Sum(x => x.Reservations) })
                 .OrderBy(x => x.Time);

            var s = this.chart1.Series.FirstOrDefault();
            s.Points.Clear();
            foreach (var time in times)
            {
                var dp = new DataPoint();
                dp.SetValueXY(time.Time, time.Reservations);
                s.Points.Add(dp);
            }
        }

        private void SampleForm_Load(object sender, EventArgs e)
        {
            this.GenerateChart();
        }
    }
}
