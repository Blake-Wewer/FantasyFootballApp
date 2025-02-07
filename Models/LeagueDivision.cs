using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballApp.Models
{
    [Table("league_divisions")]
    public class LeagueDivision
    {
        [Key]
        [Column("league_division_id")]
        public required int Id { get; set; }

        [ForeignKey("LeagueSeason")]
        [Column("league_season_id")]
        public required int LeagueSeasonId { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required LeagueSeason LeagueSeason { get; set; }

        public virtual required ICollection<Team> Teams { get; set; }
    }
}
