using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("league_seasons")]
    public class LeagueSeason
    {
        [Key]
        [Column("league_season_id")]
        public required int Id { get; set; }

        [ForeignKey("League")]
        [Column("league_id")]
        public required int LeagueId { get; set; }

        [ForeignKey("Season")]
        [Column("season_id")]
        public required int SeasonId { get; set; }

        [Column("yahoo_league_season_id")]
        public string? YahooLeagueSeasonId { get; set; }

        [Column("num_teams")]
        public required int NumTeams { get; set; }

        [Column("num_playoff_teams")]
        public int? NumPlayoffTeams { get; set; }

        [Column("num_weeks")]
        public required int NumWeeks { get; set; }

        [Column("rivalry_weeks")]
        public string? RivalryWeeks { get; set; }

        [Column("nut_cup_week")]
        public int? NutCupWeek { get; set; }

        [Column("avg_points")]
        public double? AvgPoints { get; set; }

        [Column("avg_moves")]
        public double? AvgMoves { get; set; }

        [Column("completed")]
        public required int Completed { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required League League { get; set; }

        public virtual required Season Season { get; set; }

        public virtual required ICollection<Team> Teams { get; set; }
    }
}
