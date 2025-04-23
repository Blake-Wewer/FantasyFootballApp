namespace FantasyFootballApp
{
    using System;

    /// <summary>
    /// Defines the <see cref="Record" />
    /// </summary>
    internal class Record
    {
        /// <summary>
        /// Gets or sets the wins
        /// </summary>
        public int wins { get; set; }

        /// <summary>
        /// Gets or sets the losses
        /// </summary>
        public int losses { get; set; }

        /// <summary>
        /// Gets or sets the ties
        /// </summary>
        public int ties { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        public Record()
        {
            wins = 0;
            losses = 0;
            ties = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="wins">The wins<see cref="int"/></param>
        /// <param name="losses">The losses<see cref="int"/></param>
        /// <param name="ties">The ties<see cref="int"/></param>
        public Record(int wins, int losses, int ties)
        {
            this.wins = wins;
            this.losses = losses;
            this.ties = ties;
        }

        /// <summary>
        /// Adds one to the Win value
        /// </summary>
        public void Win()
        {
            this.wins++;
        }

        /// <summary>
        /// Adds one to the Loss value
        /// </summary>
        public void Loss()
        {
            this.losses++;
        }

        /// <summary>
        /// Adds one to the Tie value
        /// </summary>
        public void Tie()
        {
            this.ties++;
        }

        /// <summary>
        /// The Display
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string Display()
        {
            if (wins + losses + ties == 0) return "-";
            else if (ties > 0) return wins + "-" + losses + "-" + ties;
            else return wins + "-" + losses;
        }

        /// <summary>
        /// The WinPerc
        /// </summary>
        /// <param name="decimal_places">The decimal_places<see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string WinPerc(int decimal_places = 4)
        {
            if (wins + losses + ties == 0) return "-";
            else return Math.Round((wins + (0.5 * ties)) / (wins + losses + ties), decimal_places).ToString("F" + decimal_places);
        }

        public string FullDisplay(int decimal_places = 4)
        {
            if (wins + losses + ties == 0) return "-";
            else return this.Display + " (" + this.WinPerc(decimal_places) + ")";
        }
    }
}
