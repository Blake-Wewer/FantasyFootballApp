using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("teams")]
    public class Team
    {
        [Key]
        [Column("team_id")]
        public required int Id { get; set; }

        [Column("yahoo_team_id")]
        public string? YahooTeamId { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [ForeignKey("Manager")]
        [Column("manager_id")]
        public required int ManagerId { get; set; }

        [ForeignKey("LeagueSeason")]
        [Column("league_season_id")]
        public required int LeagueSeasonId { get; set; }

        [ForeignKey("LeagueDivision")]
        [Column("division_id")]
        public int? DivisionId { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required Manager Manager { get; set; }

        public virtual required TeamDetail TeamDetail { get; set; }

        public virtual required LeagueSeason LeagueSeason { get; set; }

        public virtual LeagueDivision? LeagueDivision { get; set; }

        public virtual ICollection<DraftPick> DraftPicks { get; set; } = new List<DraftPick>();

        public virtual ICollection<MatchupDetail> MatchupDetails { get; set; } = new List<MatchupDetail>();
    }
}
