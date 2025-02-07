using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("managers")]
    public class Manager {
        [Key]
        [Column("manager_id")]
        public required int Id { get; set; }

        [Column("first_name")]
        public required string FirstName { get; set; }

        [Column("last_name")]
        public required string LastName { get; set; }

        [Column("nickname")]
        public string? Nickname { get; set; }

        [Column("email")]
        public required string Email{ get; set; }

        [Column("spreadsheet_eligible")]
        public required int SpreadsheetEligible { get; set; }

        [Column("aggregation_ranking_eligible")]
        public required int AggregationRankingEligible { get; set; }

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required ICollection<Team> Teams { get; set; }
    }
}
