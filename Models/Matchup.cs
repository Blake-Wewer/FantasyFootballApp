using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("matchups")]
    public class Matchup
    {
        [Key]
        [Column("matchup_id")]
        public required int Id { get; set; }

        [ForeignKey("LeagueSeason")]
        [Column("league_season_id")]
        public required int LeagueSeasonId { get; set; }

        [Column("week")]
        public required int Week { get; set; }

        [ForeignKey("Team")]
        [Column("team1_id")]
        public required int Team1Id { get; set; }

        [ForeignKey("Team")]
        [Column("team2_id")]
        public int? Team2Id { get; set; }

        [Column("margin_of_victory")]
        public double? MarginOfVictory { get; set; }

        [Column("division_matchup")]
        public required int DivisionMatchup { get; set; }

        [Column("rivalry_week_matchup")]
        public required int RivalryWeekMatchup { get; set; }

        [Column("nut_cup_matchup")]
        public required int NutCupMatchup { get; set; }

        [Column("bye_week")]
        public required int ByeWeek { get; set; }

        [Column("playoff_matchup")]
        public required int PlayoffMatchup { get; set; }

        [Column("championship_playoff_matchup")]
        public required int ChampionshipPlayoffMatchup { get; set; }

        [Column("medal_playoff_matchup")]
        public required int MedalPlayoffMatchup { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required List<MatchupDetail> MatchupDetails { get; set; }

        public virtual required LeagueSeason LeagueSeason { get; set; }

        public virtual required Team Team1 { get; set; }

        public virtual Team? Team2 { get; set; }
    }
}
