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
    using System.Windows.Forms.DataVisualization.Charting;
    using System.Xml.Serialization;

    /// <summary>
    /// The sample form.
    /// </summary>
    public partial class SampleForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleForm"/> class.
        /// </summary>
        public SampleForm()
        {
            this.InitializeComponent();
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
            var m = new Cinema(int.Parse(this.cmbCinema.SelectedItem.ToString().Split('|').Last().Trim())).GetMovies(this.dtpDay.Value);
            var serializer = new XmlSerializer(typeof(List<Movie>));
            var stream = new FileStream(Directory.GetCurrentDirectory() + "\\data\\" + this.cmbCinema.SelectedItem.ToString().Split('|').Last().Trim() + ".xml", FileMode.Truncate);
            serializer.Serialize(stream, m);
            stream.Close();

            this.GenerateChart();
        }

        /// <summary>
        /// The generate chart.
        /// </summary>
        private void GenerateChart()
        {
            this.disableControls(false);
            var serializer = new XmlSerializer(typeof(List<Movie>));
            var m = new List<Movie>();

            var stream = new FileStream(Directory.GetCurrentDirectory() + "\\data\\" + this.cmbCinema.SelectedItem.ToString().Split('|').Last().Trim() + ".xml", FileMode.OpenOrCreate);
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

            var times =
                m.Where(
                    x => x.Name != string.Empty && x.Time >= this.dtpDay.Value && x.Time <= this.dtpDay.Value.AddDays(1))
                 .GroupBy(x => x.Time)
                 .Select(mov => new { Time = mov.Key, Reservations = mov.Sum(x => x.Reservations) })
                 .OrderBy(x => x.Time);

            var s = this.chart1.Series.FirstOrDefault();
            s.Points.Clear();
            this.chart1.ChartAreas[0].AxisX.Maximum = this.dtpDay.Value.AddDays(1).ToOADate();
            this.chart1.ChartAreas[0].AxisX.Minimum = this.dtpDay.Value.AddHours(12).ToOADate();
            foreach (var time in times)
            {
                var dp = new DataPoint();
                dp.SetValueXY(time.Time, time.Reservations);
                s.Points.Add(dp);
            }
            this.disableControls(true);
        }

        private void SampleForm_Load(object sender, EventArgs e)
        {
            this.cmbCinema.SelectedIndex = 7;
            this.dtpDay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void dtpDay_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateChart();
        }

        private void cmbCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GenerateChart();
        }

        private void disableControls(bool enable)
        {
            this.cmbCinema.Enabled = enable;
            this.dtpDay.Enabled = enable;
            this.btnRefresh.Enabled = enable;
            this.chart1.Enabled = enable;
            
        }
    }
}
