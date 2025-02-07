using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("leagues")]
    public class League {
        [Key]
        [Column("league_id")]
        public required int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships
        public virtual ICollection<LeagueSeason>? LeagueSeasons { get; set; }
    }
}
