using FantasyFootballApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FantasyFootballApp
{
    public partial class HomeForm : Form
    {
        public int selectedLeague = -1;
        public HomeForm()
        {
            InitializeComponent();

            // Attach KeyPress event handler
            textBoxSeason.KeyPress += TextBoxSeason_KeyPress;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            LoadLeagueComboBox();

            dataGridView1.AutoGenerateColumns = true;
        }

        private void TextBoxSeason_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, Backspace, and Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }

        private void LoadLeagueComboBox()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<League> leagues = context.Leagues.ToList();
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
                dataGridView1.DataSource = managers;
                dataGridView1.Columns.Remove("Teams");
            }
        }

        private void btnManagers_Click(object sender, EventArgs e)
        {
            ManagerTableQuery();
        }

        private void comboBoxLeague_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxLeague.SelectedValue != null)
            {
                selectedLeague = (int)comboBoxLeague.SelectedValue;
            }
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
                    dataGridView1.DataSource = results;
                    dataGridView1.Sort(dataGridView1.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
        }
    }
}
