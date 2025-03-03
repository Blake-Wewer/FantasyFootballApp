using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Column("transaction_id")]
        public required int Id { get; set; }

        [Column("yahoo_transaction_id")]
        public string? YahooTransactionId { get; set; }

        [Column("timestamp")]
        public required DateTime Timestamp { get; set; }

        [Column("transaction_type")]
        [EnumDataType(typeof(string))]
        public required string TransactionType { get; set; }

        [ForeignKey("Team")]
        [Column("team1_id")]
        public required int Team1Id { get; set; }

        [ForeignKey("Team")]
        [Column("team2_id")]
        public int? Team2Id { get; set; }

        [Column("successful")]
        public required int Successful { get; set; }

        [Column("pending")]
        public required int Pending { get; set; }


        // Relationships

        public virtual required TransactionDetail TransactionDetail { get; set; }

        public virtual required Team Team1 { get; set; }

        public virtual Team? Team2 { get; set; }
    }
}
