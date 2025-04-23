namespace FantasyFootballApp
{
    using FantasyFootballApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using static FantasyFootballApp.HomeForm;

    /// <summary>
    /// Defines the <see cref="KeeperCalculationForm" />
    /// </summary>
    public partial class KeeperCalculationForm : Form
    {
        /// <summary>
        /// Defines the selectedLeague
        /// </summary>
        public int selectedLeague = -1;

        /// <summary>
        /// Defines the selectedManager
        /// </summary>
        public int selectedManager = -1;

        /// <summary>
        /// Defines the selectedPlayer
        /// </summary>
        public int selectedPlayer = -1;

        /// <summary>
        /// Defines the currentTeamID
        /// </summary>
        public int currentTeamID = -1;

        /// <summary>
        /// Defines the currentADP
        /// </summary>
        public double currentADP = -1;

        /// <summary>
        /// Defines the waiverKeeperValue
        /// </summary>
        private int waiverKeeperValue = 13;

        /// <summary>
        /// Defines the waiverSecondKeeperValue
        /// </summary>
        private int waiverSecondKeeperValue = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeeperCalculationForm"/> class.
        /// </summary>
        public KeeperCalculationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The KeeperCalculationForm_Load
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void KeeperCalculationForm_Load(object sender, EventArgs e)
        {
            listViewFinalRoster.Columns.Add("Player", listViewFinalRoster.Width - 100);
            listViewFinalRoster.Columns.Add("Pick (Round)", 100);

            LoadLeagueComboBox();
            LoadManagerComboBox();
        }

        /// <summary>
        /// The LoadLeagueComboBox
        /// </summary>
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

        /// <summary>
        /// The LoadManagerComboBox
        /// </summary>
        private void LoadManagerComboBox()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var manager_query = context.Managers
                                .Include(m => m.Teams)
                                    .ThenInclude(t => t.LeagueSeason)
                                    .AsQueryable();
                if (selectedLeague != -1)
                {
                    manager_query = manager_query.Where(m => m.Teams.Any(t => t.LeagueSeason.LeagueId == selectedLeague));
                }
                else
                {
                    manager_query = manager_query.Where(m => false);
                }
                List<Manager> managers = manager_query.ToList();
                List<ComboBoxItem> formattedManagers = new List<ComboBoxItem>();
                formattedManagers.Add(new ComboBoxItem { ID = -1, Display = " - Select a Manager - " });
                foreach (Manager manager in managers)
                {
                    formattedManagers.Add(new ComboBoxItem { ID = manager.Id, Display = manager.FirstName + " " + manager.LastName });
                }

                comboBoxManager.DataSource = formattedManagers.ToArray();
                comboBoxManager.DisplayMember = "Display";
                comboBoxManager.ValueMember = "ID";
            }
        }

        /// <summary>
        /// The LoadKeeperEligibleFinalRoster
        /// </summary>
        private void LoadKeeperEligibleFinalRoster()
        {
            listViewFinalRoster.Items.Clear();
            using (AppDbContext context = new AppDbContext())
            {
                currentTeamID = context.Teams
                                .Include(t => t.LeagueSeason)
                                    .ThenInclude(ls => ls.Season)
                                .Where(t => t.LeagueSeason.LeagueId == selectedLeague && t.ManagerId == selectedManager)
                                .OrderByDescending(t => t.LeagueSeason.Season.Name)
                                .Select(t => t.Id)
                                .First();

                // TODO: In the following query, we need to still get details if player was received in a trade and if they are Keeper Eligible
                var keeper_eligible_players = context.FinalRosters
                                                .Include(fr => fr.Player)
                                                .Include(fr => fr.Team)
                                                    .ThenInclude(t => t.DraftPicks)
                                                .Where(fr => fr.TeamId == currentTeamID)
                                                .Select(fr => new
                                                {
                                                    FinalRoster = fr,
                                                    OrderedDraftPicks = fr.Team.DraftPicks.OrderBy(dp => dp.Pick).ToList()
                                                })
                                                .ToList();

                // Project the data first with pick details
                var rosterWithPickDetails = keeper_eligible_players
                                                .Select(r => new
                                                {
                                                    PlayerId = r.FinalRoster.PlayerId,
                                                    PlayerName = r.FinalRoster.Player.FullName(),
                                                    PickDetails = r.OrderedDraftPicks.FirstOrDefault(dp => dp.PlayerId == r.FinalRoster.PlayerId)
                                                })
                                                .OrderBy(r => r.PickDetails?.Pick ?? int.MaxValue) // "FA" goes to the bottom
                                                .ToList();

                // Now add to the ListView in sorted order
                foreach (var r in rosterWithPickDetails)
                {
                    ListViewItem item = new ListViewItem(r.PlayerName);
                    item.SubItems.Add(r.PickDetails != null
                        ? $"{r.PickDetails.Pick} ({r.PickDetails.Round})"
                        : "FA");
                    item.Tag = r.PlayerId;
                    listViewFinalRoster.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// The LoadKeeperValues
        /// </summary>
        private void LoadKeeperValues()
        {
            using (AppDbContext context = new AppDbContext())
            {
                DraftPick? pick = context.DraftPicks
                                        .Include(dp => dp.Team)
                                        .Where(dp => dp.PlayerId == selectedPlayer && dp.TeamId == currentTeamID)
                                        .FirstOrDefault();

                FinalRoster fr_player = context.FinalRosters.Where(fr => fr.TeamId == currentTeamID && fr.PlayerId == selectedPlayer).First();
                if (pick != null)
                {
                    textBoxPick.Text = pick.Pick.ToString();
                    textBoxRound.Text = pick.Round.ToString();
                }
                else
                {
                    textBoxPick.Text = "-";
                    textBoxRound.Text = "-";
                }
                textBoxSeasonsKept.Text = fr_player.YearsAsKeeper.ToString();
            }
        }

        /// <summary>
        /// The comboBoxLeague_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void comboBoxLeague_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxLeague.SelectedValue != null)
            {
                selectedLeague = (int)comboBoxLeague.SelectedValue;

                LoadManagerComboBox();
            }
        }

        /// <summary>
        /// The comboBoxManager_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void comboBoxManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxManager.SelectedValue != null)
            {
                selectedManager = (int)comboBoxManager.SelectedValue;

                LoadKeeperEligibleFinalRoster();
            }
        }

        /// <summary>
        /// The listViewFinalRoster_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void listViewFinalRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (listViewFinalRoster.SelectedItems.Count > 0)
            {
                selectedPlayer = (int)(listViewFinalRoster.SelectedItems[0].Tag ?? -1);

                LoadKeeperValues();

                // Enable the button if we have a valid selected player and adp value
                buttonCalculateKeeperValue.Enabled = currentADP != -1 && selectedPlayer != -1;
                checkBoxSecondWaiverKeeper.Enabled = currentADP != -1 && selectedPlayer != -1;
            }
            else
            {
                selectedPlayer = -1;
            }
        }

        /// <summary>
        /// The textBoxCurrentADP_TextChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void textBoxCurrentADP_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCurrentADP.Text.Length > 0 && double.TryParse(textBoxCurrentADP.Text.ToString(), out double adp)) currentADP = adp;
            else currentADP = -1;

            // Enable the button if we have a valid selected player and adp value
            buttonCalculateKeeperValue.Enabled = currentADP != -1 && selectedPlayer != -1;
            checkBoxSecondWaiverKeeper.Enabled = currentADP != -1 && selectedPlayer != -1;
        }

        /// <summary>
        /// The buttonCalculateKeeperValue_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void buttonCalculateKeeperValue_Click(object sender, EventArgs e)
        {
            using (AppDbContext context = new AppDbContext())
            {
                FinalRoster fr_player = context.FinalRosters.Where(fr => fr.TeamId == currentTeamID && fr.PlayerId == selectedPlayer).First();
                League? league = context.Leagues.Find(selectedLeague);
                if (league != null)
                {
                    if (fr_player.YearsAsKeeper >= league.KeeperMaxYears)
                    {
                        textBoxMethod1Value.Text = "-";
                        textBoxMethod2Value.Text = "-";
                        textBoxKeeperValue.Text = "-";
                        ErrorMessages.PlayerKeeperEligibilityExhaustedMessageBox();
                    }
                }

                DraftPick? pick = context.DraftPicks
                                    .Include(dp => dp.Team)
                                    .Where(dp => dp.PlayerId == selectedPlayer && dp.TeamId == currentTeamID)
                                    .FirstOrDefault();
                LeagueSeason most_recent_season = context.LeagueSeasons.Include(ls => ls.Season).Where(ls => ls.LeagueId == selectedLeague).OrderByDescending(ls => ls.Season.Name).First();

                int previous_pick_value = -1;
                int previous_round_value = -1;

                if (pick != null)
                {
                    previous_pick_value = pick.Pick;
                    previous_round_value = pick.Round;
                }
                else if (checkBoxSecondWaiverKeeper.Checked == true)
                {
                    previous_round_value = waiverSecondKeeperValue;
                }
                else
                {
                    previous_round_value = waiverKeeperValue;
                }

                // Method 1 (Round Bumping)
                int method1Value = previous_round_value - (2 * (fr_player.YearsAsKeeper)) > 0 ? previous_round_value - (2 * (fr_player.YearsAsKeeper)) : 1;

                // Method 2 (ADP Avg. Calculation)
                if (previous_pick_value == -1) previous_pick_value = 1 + (previous_round_value - 1) * most_recent_season.NumTeams;
                int method2Value = (int)Math.Truncate((((Math.Truncate(((double)previous_pick_value + currentADP) / 2) - 1) / most_recent_season.NumTeams) + 1));

                // Find min
                int keeper_value = Math.Min(method1Value, method2Value);

                // Display Values
                textBoxMethod1Value.Text = method1Value.ToString();
                textBoxMethod2Value.Text = method2Value.ToString();
                textBoxKeeperValue.Text = keeper_value.ToString();
            }
        }
    }
}
