using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("seasons")]
    public class Season
    {
        [Key]
        [Column("season_id")]
        public required int Id { get; set; }

        [Column("yahoo_season_id")]
        public int? YahooSeasonId { get; set; }

        [Column("name")]
        public required int Name { get; set; }

        [Column("start_date")]
        public required DateOnly StartDate { get; set; }

        [Column("end_date")]
        public required DateOnly EndDate { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual ICollection<LeagueSeason> LeagueSeasons { get; set; }
    }
}
