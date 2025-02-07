using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("transaction_details")]
    public class TransactionDetail
    {
        [Key]
        [Column("transaction_detail_id")]
        public required int Id { get; set; }

        [ForeignKey("Transaction")]
        [Column("transaction_id")]
        public required int TransactionId { get; set; }

        [ForeignKey("Team")]
        [Column("team_id")]
        public required int TeamId { get; set; }

        [Column("acquired_player_ids")]
        public string? AcquiredPlayerIds { get; set; }

        [Column("surrendered_player_ids")]
        public string? SurrenderedPlayerIds { get; set; }


        // Relationships

        public virtual required Transaction Transaction { get; set; }

        public virtual required Team Team { get; set; }
    }
}
