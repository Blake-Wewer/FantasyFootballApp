using FantasyFootballApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FantasyFootballApp
{
    public partial class HomeForm : Form
    {
        public int selectedLeague = -1;
        public string lastReportRan = string.Empty;
        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            LoadLeagueComboBox();

            dataGridViewHomeForm.AutoGenerateColumns = true;
        }

        private void TextBoxSeason_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, Backspace, and Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true; // Block the input
            } else if (e.KeyChar == (char)Keys.Enter) {
                if (lastReportRan != string.Empty) {
                    switch (lastReportRan) {
                        case "btnManagers_Click":
                            this.btnManagers_Click(sender, e);
                            break;
                        case "buttonStandings_Click":
                            this.buttonStandings_Click(sender, e);
                            break;
                        case "btnPowerRankings_Click":
                            this.btnPowerRankings_Click(sender, e);
                            break;
                        case "buttonDraftResults_Click":
                            this.buttonDraftResults_Click(sender, e);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void LoadLeagueComboBox()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var leagues_query = context.Leagues.AsEnumerable();
                if (Permissions.league_permissions != 0)
                {
                    leagues_query = leagues_query.Where(l => (l.Permissions & Permissions.league_permissions) != 0);
                }
                List<League> leagues = leagues_query.ToList();
                // Display or manipulate the data here
                List<ComboBoxItem> formattedLeagues = new List<ComboBoxItem>();
                formattedLeagues.Add(new ComboBoxItem { ID = -1, Display = " - Select a League - " });
                foreach (League league in leagues)
                {
                    formattedLeagues.Add(new ComboBoxItem { ID = league.Id, Display = league.Name });
                }

                comboBoxLeague.DataSource = formattedLeagues.ToArray();
                comboBoxLeague.DisplayMember = "Display";
                comboBoxLeague.ValueMember = "ID";
            }
        }

        public class ComboBoxItem
        {
            public required string Display { get; set; }
            public required int ID { get; set; }

            public override string ToString() => Display;
            public int Value() => ID;
        }

        private void ManagerTableQuery()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var managers_query = context.Managers.Include(m => m.Teams)
                                        .ThenInclude(t => t.LeagueSeason)
                                        .AsQueryable();

                if (selectedLeague != -1)
                {
                    managers_query = managers_query.Where(m => m.Teams.Any(t => t.LeagueSeason.LeagueId == selectedLeague));
                }

                int season = -1;
                if (textBoxSeason.Text.Length > 0 && int.TryParse(textBoxSeason.Text.ToString(), out season))
                {
                    managers_query = managers_query.Where(m => m.Teams.Any(t => t.LeagueSeason.Season.Name == season));
                }

                List<Manager> managers = managers_query.ToList();

                // Display or manipulate the data here
                dataGridViewHomeForm.DataSource = managers;
                dataGridViewHomeForm.Columns.Remove("Teams");
            }
        }

        private void btnManagers_Click(object sender, EventArgs e)
        {
            ManagerTableQuery();

            lastReportRan = "btnManagers_Click";
        }

        private void btnManagerVsManager_Click(object sender, EventArgs e)
        {
            ManagerComparisonForm managerComparisonForm = new ManagerComparisonForm();
            managerComparisonForm.ShowDialog();
        }

        private class PowerRankingDisplay
        {
            public int Rank;
            public double Score;
            public String ManagerName;

            public PowerRankingDisplay(int r, double s, String n)
            {
                Rank = r;
                Score = s;
                ManagerName = n;
            }
        }

        private void buttonStandings_Click(object sender, EventArgs e)
        {
            if (selectedLeague == -1)
            {
                ErrorMessages.LeagueRequiredMessageBox();
            }
            else
            {
                using (AppDbContext context = new AppDbContext())
                {
                    var standings_query = context.Teams
                                            .Include(t => t.TeamDetail)
                                            .Include(t => t.Manager)
                                            .Include(t => t.LeagueSeason)
                                                .ThenInclude(ls => ls.Season)
                                            .Where(t => t.LeagueSeason.LeagueId == selectedLeague)
                                            .OrderBy(t => t.TeamDetail.Finish)
                                                .ThenBy(t => t.LeagueSeason.Season.Name)
                                            .AsQueryable();
                    int season = -1;
                    if (textBoxSeason.Text.Length > 0 && int.TryParse(textBoxSeason.Text.ToString(), out season))
                    {
                        standings_query = standings_query.Where(t => t.LeagueSeason.Season.Name == season);
                    }

                    List<Team> standings = standings_query.ToList();
                    List<int> seasons = standings.Select(t => t.LeagueSeason.Season.Name).Distinct().ToList();
                    int max_finish = standings.Select(t => t.TeamDetail.Finish).Max();

                    DataTable results = new DataTable();
                    dataGridViewHomeForm.Columns.Clear();

                    results.Columns.Add("Finish", typeof(int));
                    foreach (int s in seasons)
                    {
                        results.Columns.Add(s.ToString(), typeof(string));
                    }
                    for (int i = 1; i <= max_finish; i++)
                    {
                        List<object> row = new List<object>() { i };
                        foreach (int s in seasons)
                        {
                            Team team = standings.Where(t => t.TeamDetail.Finish == i && t.LeagueSeason.Season.Name == s).FirstOrDefault();
                            if (team != null)
                            {
                                string record = team.TeamDetail.Wins != null
                                                ? "(" + (team.TeamDetail.Ties == 0
                                                    ? team.TeamDetail.Wins + "-" + team.TeamDetail.Losses
                                                    : team.TeamDetail.Wins + "-" + team.TeamDetail.Losses + "-" + team.TeamDetail.Ties) + ")"
                                                : "";
                                row.Add(team.Manager.FirstName + " " + team.Manager.LastName + "   " + record);
                            }
                            else
                            {
                                row.Add("");
                            }
                        }
                        results.Rows.Add(row.ToArray());
                    }

                    // Display or manipulate the data here
                    dataGridViewHomeForm.DataSource = results;
                    dataGridViewHomeForm.Sort(dataGridViewHomeForm.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                }
            }

            lastReportRan = "buttonStandings_Click";
        }

        private void btnPowerRankings_Click(object sender, EventArgs e)
        {
            if (selectedLeague == -1)
            {
                ErrorMessages.LeagueRequiredMessageBox();
            }
            else
            {
                using (AppDbContext context = new AppDbContext())
                {
                    int season = -1;
                    Dictionary<int, double> power_rankings;
                    if (textBoxSeason.Text.Length > 0 && int.TryParse(textBoxSeason.Text.ToString(), out season))
                    {
                        power_rankings = Calculations.CalculateLeaguePowerRankings(selectedLeague, season);
                    }
                    else
                    {
                        power_rankings = Calculations.CalculateLeaguePowerRankings(selectedLeague);
                    }
                    List<Manager> managers = context.Managers.Where(m => power_rankings.Keys.Contains(m.Id)).ToList();
                    DataTable results = new DataTable();
                    results.Columns.Add("Rank", typeof(int));
                    results.Columns.Add("Power Ranking Score", typeof(double));
                    results.Columns.Add("Manager Name", typeof(String));
                    foreach (Manager manager in managers)
                    {
                        results.Rows.Add(power_rankings.Keys.Cast<object>().ToList().IndexOf(manager.Id) + 1, Math.Round(power_rankings[manager.Id], 5), manager.FirstName + " " + manager.LastName);
                    }

                    // Display or manipulate the data here
                    dataGridViewHomeForm.DataSource = results;
                    dataGridViewHomeForm.Sort(dataGridViewHomeForm.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                }
            }

            lastReportRan = "btnPowerRankings_Click";
        }

        private void buttonDraftResults_Click(object sender, EventArgs e)
        {
            if (selectedLeague == -1)
            {
                ErrorMessages.LeagueRequiredMessageBox();
            }
            else
            {
                using (AppDbContext context = new AppDbContext())
                {
                    int season = -1;
                    DataTable results = new DataTable();
                    dataGridViewHomeForm.Columns.Clear();
                    var draft_results_query = context.DraftPicks
                                                .Include(dp => dp.Draft.LeagueSeason.Season)
                                                .Include(dp => dp.Team.Manager)
                                                .Include(dp => dp.Player)
                                                    .ThenInclude(p => p.Seasons)
                                                .Where(dp => dp.Draft.LeagueSeason.LeagueId == selectedLeague)
                                                .AsQueryable();
                    if (textBoxSeason.Text.Length > 0 && int.TryParse(textBoxSeason.Text.ToString(), out season))
                    {
                        draft_results_query = draft_results_query.Where(dp => dp.Draft.LeagueSeason.Season.Name == season);
                    }
                    else
                    {
                        results.Columns.Add("Season", typeof(string));
                    }
                    results.Columns.Add("Round", typeof(int));
                    results.Columns.Add("Pick", typeof(int));
                    results.Columns.Add("Manager", typeof(string));
                    results.Columns.Add("Player", typeof(string));
                    results.Columns.Add("Position", typeof(string));
                    results.Columns.Add("ADR", typeof(string));
                    results.Columns.Add("ADP", typeof(string));
                    results.Columns.Add("ADP vs Pick Diff", typeof(double));
                    results.Columns.Add("% Max ADP Gained", typeof(double));
                    results.Columns.Add("Overall Rank Finish", typeof(int));
                    results.Columns.Add("Position Rank Finish", typeof(int));

                    var draft_results = draft_results_query.ToList();
                    foreach (DraftPick pick in draft_results)
                    {
                        if (season != -1)
                        {
                            results.Rows.Add(pick.Round,
                                         pick.Pick,
                                         pick.Team.Manager.FirstName + " " + pick.Team.Manager.LastName,
                                         pick.Player.FirstName + " " + pick.Player.LastName,
                                         pick.Player.Position ?? "-",
                                         pick.AvgRound != null ? Math.Round((double)pick.AvgRound, 2).ToString("F1") : "-",
                                         pick.AvgPick != null ? Math.Round((double)pick.AvgPick, 2).ToString("F2") : "-",
                                         pick.AvgRound != null && pick.AvgPick != null ? Math.Round((double)pick.AvgPick - (double)pick.Pick, 2).ToString("F2") : Math.Round((double)(pick.Draft.NumRounds * pick.Draft.LeagueSeason.NumTeams + 1) - (double)pick.Pick, 2).ToString("F2"),
                                         pick.Pick != pick.Round
                                                        ? Math.Round((((double)(pick.Pick - (pick.AvgPick ?? pick.Draft.NumRounds * pick.Draft.LeagueSeason.NumTeams + 1)) / (double)(pick.Pick - pick.Round)) * 100), 2)
                                                        : Math.Round(((double)(pick.Pick - (pick.AvgPick ?? pick.Draft.NumRounds * pick.Draft.LeagueSeason.NumTeams + 1)) * 100), 2),
                                         pick.Player.Seasons.Where(ps => ps.SeasonId == pick.Draft.LeagueSeason.SeasonId).First().OverallRanking,
                                         pick.Player.Seasons.Where(ps => ps.SeasonId == pick.Draft.LeagueSeason.SeasonId).First().PositionRanking
                            );
                        }
                        else
                        {
                            results.Rows.Add(pick.Draft.LeagueSeason.Season.Name,
                                         pick.Round,
                                         pick.Pick,
                                         pick.Team.Manager.FirstName + " " + pick.Team.Manager.LastName,
                                         pick.Player.FirstName + " " + pick.Player.LastName,
                                         pick.Player.Position ?? "-",
                                         pick.AvgRound != null ? Math.Round((double)pick.AvgRound, 2).ToString("F1") : "-",
                                         pick.AvgPick != null ? Math.Round((double)pick.AvgPick, 2).ToString("F2") : "-",
                                         pick.AvgRound != null && pick.AvgPick != null ? Math.Round((double)pick.AvgPick - (double)pick.Pick, 2).ToString("F2") : Math.Round((double)(pick.Draft.NumRounds * pick.Draft.LeagueSeason.NumTeams + 1) - (double)pick.Pick, 2).ToString("F2"),
                                         pick.Pick != pick.Round
                                                        ? Math.Round((((double)(pick.Pick - (pick.AvgPick ?? pick.Draft.NumRounds * pick.Draft.LeagueSeason.NumTeams + 1)) / (double)(pick.Pick - pick.Round)) * 100), 2)
                                                        : Math.Round(((double)(pick.Pick - (pick.AvgPick ?? pick.Draft.NumRounds * pick.Draft.LeagueSeason.NumTeams + 1)) * 100), 2),
                                         pick.Player.Seasons.Where(ps => ps.SeasonId == pick.Draft.LeagueSeason.SeasonId).First().OverallRanking,
                                         pick.Player.Seasons.Where(ps => ps.SeasonId == pick.Draft.LeagueSeason.SeasonId).First().PositionRanking
                            );
                        }
                    }

                    // Display or manipulate the data here
                    dataGridViewHomeForm.DataSource = results;
                    dataGridViewHomeForm.Columns["ADP vs Pick Diff"].DefaultCellStyle.Format = "N1";
                    dataGridViewHomeForm.Columns["% Max ADP Gained"].DefaultCellStyle.Format = "N2";
                    if (season != -1)
                    {
                        dataGridViewHomeForm.Sort(dataGridViewHomeForm.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
                    }
                    else
                    {
                        dataGridViewHomeForm.Sort(dataGridViewHomeForm.Columns[2], System.ComponentModel.ListSortDirection.Ascending);
                        dataGridViewHomeForm.Sort(dataGridViewHomeForm.Columns[0], System.ComponentModel.ListSortDirection.Descending);
                    }
                }
            }

            lastReportRan = "buttonDraftResults_Click";
        }

        private void buttonAllPlay_Click(object sender, EventArgs e)
        {
            AllPlayForm allPlayForm = new AllPlayForm();
            allPlayForm.ShowDialog();
        }

        private void comboBoxLeague_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxLeague.SelectedValue != null)
            {
                selectedLeague = (int)comboBoxLeague.SelectedValue;
            }
        }
    }
}
