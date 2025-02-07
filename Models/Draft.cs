using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    public enum EnumDraftType
    {
        Standard,
        SalaryCap,
        Auction
    }

    [Table("drafts")]
    public class Draft
    {
        [Key]
        [Column("draft_id")]
        public required int Id { get; set; }

        [ForeignKey("LeagueSeason")]
        [Column("league_season_id")]
        public required int LeagueSeasonId { get; set; }

        [Column("draft_type")]
        public EnumDraftType? DraftType { get; set; }

        [Column("num_rounds")]
        public int? NumRounds { get; set; }


        // Relationships

        public virtual required LeagueSeason LeagueSeason { get; set; }
    }
}
