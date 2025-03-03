using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    public class FinalRoster
    {
        [Key]
        [Column("final_roster_id")]
        public required int Id { get; set; }

        [ForeignKey("Team")]
        [Column("team_id")]
        public required int TeamId { get; set; }

        [ForeignKey("Player")]
        [Column("player_id")]
        public required int PlayerId { get; set; }

        [Column("roster_position")]
        [EnumDataType(typeof(string))]
        public required string RosterPosition { get; set; }


        // Relationships

        public virtual required Team Team { get; set; }

        public virtual required Player Player { get; set; }
    }
}
