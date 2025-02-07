using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{

    [Table("players")]
    public class Player
    {
        // Details

        [Key]
        [Column("player_id")]
        public required int Id { get; set; }

        [Column("yahoo_player_id")]
        public required int YahooPlayerId { get; set; }

        [Column("last_name")]
        public required string LastName { get; set; }

        [Column("first_name")]
        public required string FirstName { get; set; }

        [Column("team")]
        public required string Team { get; set; }

        [Column("position")]
        public required EnumPosition Position { get; set; }

        [Column("image")]
        public required string Image { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual ICollection<PlayerSeason>? Seasons { get; set; }
    }
}
