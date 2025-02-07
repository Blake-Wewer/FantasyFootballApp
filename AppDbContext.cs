namespace FantasyFootballApp
{
    using System.Configuration;
    using FantasyFootballApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;

    /// <summary>
    /// Defines the <see cref="AppDbContext" />
    /// </summary>
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<LeagueSeason>()
            //    .HasOne(ls => ls.Season);
        }

        public DbSet<Season> Seasons { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSeason> PlayerSeasons { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueSeason> LeagueSeasons { get; set; }
        public DbSet<LeagueDivision> LeagueDivisions { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamDetail> TeamDetails { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<DraftPick> DraftPicks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<FinalRoster> FinalRosters { get; set; }
        public DbSet<Matchup> Matchups { get; set; }
        public DbSet<MatchupDetail> MatchupDetails { get; set; }
    }
}
