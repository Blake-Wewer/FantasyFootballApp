using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{

    [Table("draft_picks")]
    public class DraftPick
    {
        [Key]
        [Column("draft_pick_id")]
        public required int Id { get; set; }

        [ForeignKey("Draft")]
        [Column("draft_id")]
        public required int DraftId { get; set; }

        [Column("pick")]
        public required int Pick { get; set; }

        [Column("round")]
        public required int Round { get; set; }

        [ForeignKey("Team")]
        [Column("team_id")]
        public required int TeamId { get; set; }

        [ForeignKey("Player")]
        [Column("player_id")]
        public required int PlayerId { get; set; }

        [Column("avg_pick")]
        public double? AvgPick { get; set; }

        [Column("avg_round")]
        public double? AvgRound { get; set; }

        [Column("cost")]
        public double? Cost { get; set; }

        [Column("avg_cost")]
        public double? AvgCost { get; set; }

        [Column("percentage_drafted")]
        public required double PercentageDrafted { get; set; }


        // Relationships

        public virtual required Draft Draft { get; set; }

        public virtual required Team Team { get; set; }

        public virtual required Player Player { get; set; }
    }
}
