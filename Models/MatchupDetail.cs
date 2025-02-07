using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballApp.Models
{
    [Table("matchup_details")]
    public class MatchupDetail
    {
        [Key]
        [Column("matchup_detail_id")]
        public required int Id { get; set; }

        [ForeignKey("Matchup")]
        [Column("matchup_id")]
        public required int MatchupId { get; set; }

        [ForeignKey("Team")]
        [Column("team_id")]
        public required int TeamId { get; set; }

        [Column("team_proj_points")]
        public double? TeamProjPoints { get; set; }

        [Column("team_points")]
        public required double TeamPoints { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required Matchup Matchup { get; set; }

        public virtual required Team Team { get; set; }
    }
}
