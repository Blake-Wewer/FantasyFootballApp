using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("player_seasons")]
    public class PlayerSeason
    {
        [Key]
        [Column("player_season_id")]
        public required int Id { get; set; }

        [Column("yahoo_player_season_id")]
        public required string YahooPlayerSeasonId { get; set; }

        [ForeignKey("Player")]
        [Column("player_id")]
        public required int PlayerId { get; set; }

        [ForeignKey("Season")]
        [Column("season_id")]
        public required int SeasonId { get; set; }

        [Column("overall_ranking")]
        public required int OverallRanking { get; set; }

        [Column("position_ranking")]
        public required int PositionRanking { get; set; }

        [Column("team")]
        public required string Team { get; set; }

        [Column("status")]
        public required string Status { get; set; }

        [Column("status_details")]
        public required string StatusDetails { get; set; }

        [Column("image")]
        public required string Image { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required Player Player { get; set; }

        public virtual required Season Season { get; set; }

    }
}
