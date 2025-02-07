﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyFootballApp.Models
{
    [Table("team_details")]
    public class TeamDetail
    {
        [Key]
        [Column("team_detail_id")]
        public required int Id { get; set; }

        [ForeignKey("Team")]
        [Column("team_id")]
        public required int TeamId { get; set; }

        [Column("wins")]
        public int? Wins { get; set; }

        [Column("losses")]
        public int? Losses { get; set; }

        [Column("ties")]
        public int? Ties { get; set; }

        [Column("division_wins")]
        public int? DivisionWins { get; set; }

        [Column("division_losses")]
        public int? DivisionLosses { get; set; }

        [Column("division_ties")]
        public int? DivisionTies { get; set; }

        [Column("division_finish")]
        public int? DivisionFinish { get; set; }

        [Column("longest_winning_streak")]
        public int? LongestWinningStreak { get; set; }

        [Column("longest_losing_streak")]
        public int? LongestLosingStreak { get; set; }

        [Column("finish")]
        public required int Finish { get; set; }

        [Column("playoffs")]
        public int? Playoffs { get; set; }

        [Column("playoff_seed")]
        public int? PlayoffSeed { get; set; }

        [Column("points_scored")]
        public required double PointsScored { get; set; }

        [Column("points_scored_rank")]
        public required int PointsScoredRank { get; set; }

        [Column("points_against")]
        public double? PointsAgainst { get; set; }

        [Column("moves")]
        public int? Moves { get; set; }

        // Needs Rivalry Week Data and Projected Data

        [Column("create_date")]
        public required DateTime CreateDate { get; set; }

        [Column("modify_date")]
        public required DateTime ModifyDate { get; set; }


        // Relationships

        public virtual required Team Team { get; set; }
    }
}
