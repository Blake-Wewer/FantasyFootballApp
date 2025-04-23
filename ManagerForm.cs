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
    /// Defines the <see cref="ManagerForm" />
    /// </summary>
    public partial class ManagerForm : Form
    {
        /// <summary>
        /// Defines the selectedManager
        /// </summary>
        public int selectedManager = -1;

        /// <summary>
        /// Defines the selectedReport
        /// </summary>
        public int selectedReport = -1;

        /// <summary>
        /// Defines the selectedLeagues
        /// </summary>
        public List<int> selectedLeagues = new List<int>();

        /// <summary>
        /// Defines the permittedLeagues
        /// </summary>
        public List<int> permittedLeagues = new List<int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerForm"/> class.
        /// </summary>
        public ManagerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The ManagerForm_Load
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void ManagerForm_Load(object sender, EventArgs e)
        {
            LoadLeagueListBox();
            LoadManagerComboBox();
            LoadReportComboBox();
        }

        /// <summary>
        /// The LoadLeagueListBox
        /// </summary>
        /// <param name="manager_id">The manager_id<see cref="int"/></param>
        private void LoadLeagueListBox(int manager_id = -1)
        {
            using (var context = new AppDbContext())
            {
                listBoxLeagues.ClearSelected();

                var leagues_query = context.Leagues
                                        .Include(l => l.LeagueSeasons)
                                            .ThenInclude(ls => ls.Teams)
                                        .AsQueryable();
                if (Permissions.league_permissions != 0)
                {
                    leagues_query = leagues_query.Where(l => (l.Permissions & Permissions.league_permissions) != 0);
                }
                if (manager_id != -1)
                {
                    leagues_query = leagues_query.Where(l => l.LeagueSeasons.Any(ls => ls.Teams.Any(t => t.ManagerId == manager_id)));
                }
                List<League> leagues = leagues_query.ToList();
                // Display or manipulate the data here
                List<ComboBoxItem> formattedLeagues = new List<ComboBoxItem>();
                foreach (League league in leagues)
                {
                    formattedLeagues.Add(new ComboBoxItem { ID = league.Id, Display = league.Name });
                }
                permittedLeagues = formattedLeagues.Select(l => l.ID).ToList();

                listBoxLeagues.DataSource = formattedLeagues.ToArray();
                listBoxLeagues.DisplayMember = "Display";
                listBoxLeagues.ValueMember = "ID";
                listBoxLeagues.SelectedIndex = -1;

                listBoxLeagues.Refresh();
            }
        }

        /// <summary>
        /// The LoadManagerComboBox
        /// </summary>
        private void LoadManagerComboBox()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<Manager> managers = context.Managers
                                .Include(m => m.Teams)
                                    .ThenInclude(t => t.LeagueSeason)
                                .Where(m => m.Teams.Any(t => permittedLeagues.Contains(t.LeagueSeason.LeagueId)))
                                .ToList();
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
        /// The LoadReportComboBox
        /// </summary>
        private void LoadReportComboBox()
        {
            // Generate Report ComboBox Items
            List<ComboBoxItem> reports = new List<ComboBoxItem>();

            reports.Add(new ComboBoxItem { ID = 1, Display = "Record Breakdown" });
            reports.Add(new ComboBoxItem { ID = 2, Display = "Titles" });
            reports.Add(new ComboBoxItem { ID = 3, Display = "Season Results" });       // Season Results + First Draft Pick History

            // TODO: Need to put the Rivalry Week report behind a query that checks existence based on permitted leagues
            reports.Add(new ComboBoxItem { ID = 4, Display = "Rivalry Week" });

            reports.Add(new ComboBoxItem { ID = 5, Display = "Personal Record Book" });
            //reports.Add(new ComboBoxItem { ID = 6, Display = "All-Time VS Records" });
            //reports.Add(new ComboBoxItem { ID = 7, Display = "All-Time VS Stats" });

            comboBoxReport.DataSource = reports.ToArray();
            comboBoxReport.DisplayMember = "Display";
            comboBoxReport.ValueMember = "ID";
        }

        /// <summary>
        /// The checkBoxUseAllLeagues_CheckedChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void checkBoxUseAllLeagues_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseAllLeagues.Checked == true)
            {
                labelSelectLeagues.Visible = false;
                listBoxLeagues.Enabled = false;
                listBoxLeagues.Visible = false;
            }
            else
            {
                labelSelectLeagues.Visible = true;
                listBoxLeagues.Enabled = true;
                listBoxLeagues.Visible = true;
                //selectedLeagues.Clear();
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
                LoadLeagueListBox(selectedManager);
            }
        }

        /// <summary>
        /// The comboBoxReport_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void comboBoxReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxReport.SelectedValue != null)
            {
                selectedReport = (int)comboBoxReport.SelectedValue;
                if (selectedReport == 3)
                {
                    checkBoxUseAllLeagues.Checked = false;
                    checkBoxUseAllLeagues.Enabled = false;
                    listBoxLeagues.SelectionMode = SelectionMode.One;
                    if (listBoxLeagues.Items.Count > 1) listBoxLeagues.SelectedIndex = -1;
                    else listBoxLeagues.SelectedIndex = 0;
                    labelSingleLeagueNote.Visible = true;
                }
                else
                {
                    checkBoxUseAllLeagues.Enabled = true;
                    listBoxLeagues.SelectionMode = SelectionMode.MultiSimple;
                    labelSingleLeagueNote.Visible = false;
                }
            }
        }

        /// <summary>
        /// The listBoxLeagues_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void listBoxLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLeagues = listBoxLeagues.SelectedItems.Cast<ComboBoxItem>().Select(l => l.ID).ToList();
        }

        /// <summary>
        /// The buttonManagerReport_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void buttonManagerReport_Click(object sender, EventArgs e)
        {
            if (selectedManager == -1)
            {
                ErrorMessages.ManagerRequiredMessageBox();
            }
            else if (!checkBoxUseAllLeagues.Checked && selectedLeagues.Count() == 0)
            {
                ErrorMessages.LeagueRequiredFromCheckOrListMessageBox();
            }
            else
            {
                switch (selectedReport)
                {
                    case 1:
                        ManagerRecordBreakdown();
                        break;
                    case 2:
                        ManagerTitleBreakdown();
                        break;
                    case 3:
                        SeasonResults();
                        break;
                    case 4:
                        RivalryWeekResults();
                        break;
                    case 5:
                        PersonalRecordBook();
                        break;
                    case 6:
                        //AllTimeVSRecords();
                        break;
                    case 7:
                        //AllTimeVSStats();
                        break;
                    default:
                        ErrorMessages.ReportCouldNotBeRanMessageBox();
                        break;
                }
            }
        }

        /// <summary>
        /// The ManagerRecordBreakdown
        /// 
        /// NOTE: Currently only adds Nut Cup fields if the manager has participated in one vs if the league has ever had one, may want to consider revisiting this should we wish
        /// </summary>
        private void ManagerRecordBreakdown()
        {
            using (AppDbContext context = new AppDbContext())
            {
                // Gather all selected Manager matchup details
                var matchup_detail_query = context.MatchupDetails
                                            .Include(md => md.Matchup)
                                                .ThenInclude(mat => mat.MatchupDetails)
                                            .Where(md => md.Team.Manager.Id == selectedManager
                                                    && md.Matchup.MatchupDetails.Count > 1
                                                    && permittedLeagues.Contains(md.Matchup.LeagueSeason.LeagueId))
                                            .AsQueryable();

                if (!checkBoxUseAllLeagues.Checked)
                {
                    matchup_detail_query = matchup_detail_query.Where(md => selectedLeagues.Contains(md.Matchup.LeagueSeason.LeagueId));
                }

                List<int> matchup_detail_ids = matchup_detail_query.Select(md => md.Id)
                                            .Distinct()
                                            .ToList();
                List<MatchupDetail> manager_matchups = context.MatchupDetails
                                                .Where(md => matchup_detail_ids.Contains(md.Id))
                                                .Include(md => md.Matchup)
                                                    .ThenInclude(m => m.MatchupDetails)
                                                    .ThenInclude(md => md.Team)
                                                    .ThenInclude(t => t.Manager)
                                                .Include(md => md.Team)
                                                    .ThenInclude(t => t.TeamDetail)
                                                .ToList();

                Record regular_season_record = new Record();
                Record playoff_record = new Record();
                Record championship_playoff_record = new Record();
                Record consolation_playoff_record = new Record();
                Record total_record = new Record();
                Record rivalry_week_record = new Record();
                Record nut_cup_record = new Record();

                bool has_nut_cup = false;

                foreach (MatchupDetail md in manager_matchups)
                {
                    MatchupDetail? opp_md = md.Matchup.MatchupDetails.Where(opp_md => opp_md.Id != md.Id).FirstOrDefault();
                    Matchup mat = md.Matchup;
                    if (mat.NutCupMatchup == 1) has_nut_cup = true;
                    if (opp_md != null)
                    {
                        if (md.TeamPoints > opp_md.TeamPoints)
                        {
                            total_record.Win();
                            if (mat.PlayoffMatchup == 0)
                            {
                                regular_season_record.Win();
                            }
                            if (mat.PlayoffMatchup == 1)
                            {
                                playoff_record.Win();
                                if (mat.ChampionshipPlayoffMatchup == 0)
                                {
                                    consolation_playoff_record.Win();
                                }
                                if (mat.ChampionshipPlayoffMatchup == 1)
                                {
                                    championship_playoff_record.Win();
                                }
                            }
                            if (mat.RivalryWeekMatchup == 1)
                            {
                                rivalry_week_record.Win();
                            }
                            if (mat.NutCupMatchup == 1)
                            {
                                nut_cup_record.Win();
                            }
                        }
                        else if (md.TeamPoints < opp_md.TeamPoints)
                        {
                            total_record.Loss();
                            if (mat.PlayoffMatchup == 0)
                            {
                                regular_season_record.Loss();
                            }
                            if (mat.PlayoffMatchup == 1)
                            {
                                playoff_record.Loss();
                                if (mat.ChampionshipPlayoffMatchup == 0)
                                {
                                    consolation_playoff_record.Loss();
                                }
                                if (mat.ChampionshipPlayoffMatchup == 1)
                                {
                                    championship_playoff_record.Loss();
                                }
                            }
                            if (mat.RivalryWeekMatchup == 1)
                            {
                                rivalry_week_record.Loss();
                            }
                            if (mat.NutCupMatchup == 1)
                            {
                                nut_cup_record.Loss();
                            }
                        }
                        else if (md.TeamPoints == opp_md.TeamPoints)
                        {
                            total_record.Tie();
                            if (mat.PlayoffMatchup == 0)
                            {
                                regular_season_record.Tie();
                            }
                            if (mat.PlayoffMatchup == 1)
                            {
                                playoff_record.Tie();
                                if (mat.ChampionshipPlayoffMatchup == 0)
                                {
                                    consolation_playoff_record.Tie();
                                }
                                if (mat.ChampionshipPlayoffMatchup == 1)
                                {
                                    championship_playoff_record.Tie();
                                }
                            }
                            if (mat.RivalryWeekMatchup == 1)
                            {
                                rivalry_week_record.Tie();
                            }
                            if (mat.NutCupMatchup == 1)
                            {
                                nut_cup_record.Tie();
                            }
                        }
                    }
                }

                DataTable results = new DataTable();
                results.Columns.Add("Record Type", typeof(string));
                results.Columns.Add("Record", typeof(string));
                results.Columns.Add("Win %", typeof(string));

                // Populate DataGridView
                results.Rows.Add("All-Time Regular Season", regular_season_record.Display(), regular_season_record.WinPerc());
                results.Rows.Add("All-Time Playoff", playoff_record.Display(), playoff_record.WinPerc());
                results.Rows.Add("    All-Time Championship Playoff", championship_playoff_record.Display(), championship_playoff_record.WinPerc());
                results.Rows.Add("    All-Time Consolation Playoff", consolation_playoff_record.Display(), consolation_playoff_record.WinPerc());
                results.Rows.Add("All-Time Total", total_record.Display(), total_record.WinPerc());
                results.Rows.Add("All-Time Rivalry Week", rivalry_week_record.Display(), rivalry_week_record.WinPerc());

                // Only add this if necessary
                if (has_nut_cup) results.Rows.Add("All-Time Nut Cup", nut_cup_record.Display(), nut_cup_record.WinPerc());

                dataGridViewManager.DataSource = results;
            }
        }

        /// <summary>
        /// The ManagerTitleBreakdown
        /// </summary>
        private void ManagerTitleBreakdown()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var team_details_query = context.Teams
                                            .Include(t => t.TeamDetail)
                                            .Include(t => t.LeagueSeason)
                                            .Where(t => t.ManagerId == selectedManager && permittedLeagues.Contains(t.LeagueSeason.LeagueId))
                                            .AsQueryable();

                if (!checkBoxUseAllLeagues.Checked)
                {
                    team_details_query = team_details_query.Where(t => selectedLeagues.Contains(t.LeagueSeason.LeagueId));
                }

                List<Team> teams = team_details_query.ToList();

                int scoring = 0;
                int nut_cup = 0;
                int division = 0;
                int consolation = 0;
                int championship = 0;

                bool has_nut_cup = false;

                foreach (Team t in teams)
                {
                    if (t.TeamDetail.Finish == 1) championship++;
                    if (t.TeamDetail.PointsScoredRank == 1) scoring++;
                    if (t.TeamDetail.DivisionFinish == 1) division++;
                    if (t.TeamDetail.Finish == t.LeagueSeason.NumPlayoffTeams + 1) consolation++;

                    // Nut Cup Calculations if needed
                    if (t.LeagueSeason.NutCupWeek != null)
                    {
                        has_nut_cup = true;
                        nut_cup += t.TeamDetail.NutCupWinner ?? 0;
                    }
                }

                DataTable results = new DataTable();
                results.Columns.Add("Title", typeof(string));
                results.Columns.Add("#", typeof(int));

                // Populate DataGridView
                results.Rows.Add("Scoring", scoring);
                if (has_nut_cup) results.Rows.Add("Nut Cup", nut_cup);
                results.Rows.Add("Division", division);
                results.Rows.Add("Consolation", consolation);
                results.Rows.Add("Championship", championship);

                dataGridViewManager.DataSource = results;
            }
        }

        private void SeasonResults()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var team_details_query = context.Teams
                                            .Include(t => t.TeamDetail)
                                            .Include(t => t.LeagueSeason)
                                                .ThenInclude(ls => ls.Season)
                                            .Where(t => t.ManagerId == selectedManager && permittedLeagues.Contains(t.LeagueSeason.LeagueId))
                                            .AsQueryable();
                if (!checkBoxUseAllLeagues.Checked)
                {
                    team_details_query = team_details_query.Where(t => selectedLeagues.Contains(t.LeagueSeason.LeagueId));
                }
                List<Team> teams = team_details_query.ToList();

                DataTable results = new DataTable();
                results.Columns.Add("Season", typeof(int));
                results.Columns.Add("Record", typeof(string));
                results.Columns.Add("Div. Finish", typeof(string));
                results.Columns.Add("Playoff Seed \n* = Made Playoffs", typeof(string));
                results.Columns.Add("Finish", typeof(string));
                results.Columns.Add("First Pick", typeof(string));
                results.Columns.Add("First Pick Player", typeof(string));

                // Iterate through teams and create rows for each year
                List<int> division_finishes = new List<int>();
                List<int> playoff_seeds = new List<int>();
                List<int> finishes = new List<int>();
                foreach (Team t in teams)
                {
                    Record team_record = new Record(t.TeamDetail.Wins ?? 0, t.TeamDetail.Losses ?? 0, t.TeamDetail.Ties ?? 0);
                    int first_draft_pick = context.DraftPicks.Include(dp => dp.Draft).Where(dp => dp.TeamId == t.Id && dp.Round == 1 && dp.Draft.LeagueSeasonId == t.LeagueSeasonId).Select(dp => dp.Pick).First();
                    results.Rows.Add(t.LeagueSeason.Season.Name,
                                        team_record.Display(),
                                        t.TeamDetail.DivisionFinish.ToString() ?? "-",
                                        t.TeamDetail.PlayoffSeed.ToString() + (t.TeamDetail.Playoffs == 1 ? "*" : "") ?? "-",
                                        t.TeamDetail.Finish,
                                        first_draft_pick,
                                        t.TeamDetail.FirstRoundDraftPick);
                    if (t.TeamDetail.DivisionFinish != null) division_finishes.Add((int)t.TeamDetail.DivisionFinish);
                    if (t.TeamDetail.PlayoffSeed != null) playoff_seeds.Add((int)t.TeamDetail.PlayoffSeed);
                    finishes.Add(t.TeamDetail.Finish);
                }
                results.Rows.Add(null, "Avg.",
                                    division_finishes.Count() > 0 ? Math.Round(division_finishes.Average(), 3).ToString("F3") : "-",
                                    playoff_seeds.Count() > 0 ? Math.Round(playoff_seeds.Average(), 3).ToString("F3") : "-",
                                    Math.Round(finishes.Average(), 3).ToString("F3"),
                                    "", "");
                dataGridViewManager.DataSource = results;
            }
        }

        private void RivalryWeekResults()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var team_details_query = context.Teams
                                            .Include(t => t.TeamDetail)
                                            .Include(t => t.LeagueSeason)
                                                .ThenInclude(ls => ls.Season)
                                            .Where(t => t.ManagerId == selectedManager && permittedLeagues.Contains(t.LeagueSeason.LeagueId))
                                            .AsQueryable();
                if (!checkBoxUseAllLeagues.Checked)
                {
                    team_details_query = team_details_query.Where(t => selectedLeagues.Contains(t.LeagueSeason.LeagueId));
                }
                List<Team> teams = team_details_query.ToList();

                List<MatchupDetail> rivalry_week_matchup_details = context.MatchupDetails
                                                                        .Include(md => md.Matchup)
                                                                            .ThenInclude(mat => mat.MatchupDetails)
                                                                            .ThenInclude(mat_det => mat_det.Team)
                                                                            .ThenInclude(t => t.Manager)
                                                                        .Include(md => md.Matchup)
                                                                            .ThenInclude(mat => mat.LeagueSeason)
                                                                            .ThenInclude(ls => ls.Season)
                                                                        .Where(md => md.Matchup.RivalryWeekMatchup == 1
                                                                            && teams.Select(t => t.Id).ToList().Contains(md.TeamId))
                                                                        .ToList();
                DataTable results = new DataTable();
                results.Columns.Add("Season", typeof(int));
                results.Columns.Add("Week", typeof(int));
                results.Columns.Add("Opponent", typeof(string));
                results.Columns.Add("Result \n* = Nut Cup Game", typeof(string));
                results.Columns.Add("Score", typeof(string));

                foreach(MatchupDetail md in rivalry_week_matchup_details)
                {
                    MatchupDetail opp_md = md.Matchup.MatchupDetails.Where(o => o.Id != md.Id).First();
                    string result = (md.TeamPoints > opp_md.TeamPoints ? "W" : (md.TeamPoints < opp_md.TeamPoints ? "L" : (md.TeamPoints == opp_md.TeamPoints ? "T" : "-"))) + (md.Matchup.NutCupMatchup == 1 ? "*" : "");
                    results.Rows.Add(md.Matchup.LeagueSeason.Season.Name,
                                        md.Matchup.Week,
                                        opp_md.Team.Manager.ShortName(),
                                        result,
                                        md.TeamPoints + " - " + opp_md.TeamPoints
                        );
                }

                dataGridViewManager.DataSource = results;
            }
        }

        private void PersonalRecordBook()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var teams_query = context.Teams
                                        .Include(t => t.TeamDetail)
                                        .Include(t => t.LeagueSeason)
                                            .ThenInclude(ls => ls.Season)
                                        .Include(t => t.MatchupDetails)
                                            .ThenInclude(md => md.Matchup)
                                            .ThenInclude(mat => mat.MatchupDetails)
                                            .ThenInclude(md => md.Team)
                                            .ThenInclude(t => t.Manager)
                                        .Where(t => t.ManagerId == selectedManager && permittedLeagues.Contains(t.LeagueSeason.LeagueId))
                                        .AsQueryable();

                if (!checkBoxUseAllLeagues.Checked)
                {
                    teams_query = teams_query.Where(t => selectedLeagues.Contains(t.LeagueSeason.LeagueId));
                }

                List<Team> teams = teams_query.ToList();

                DataTable results = new DataTable();
                results.Columns.Add("Category", typeof(string));
                results.Columns.Add("Best", typeof(string));
                results.Columns.Add("Worst", typeof(string));

                // Record
                (string best, string worst) records = (teams.Where(t => t.TeamDetail.Wins + t.TeamDetail.Losses + t.TeamDetail.Ties > 0).OrderByDescending(t => t.TeamDetail.RecordWinPerc()).First().TeamDetail.RecordDisplayWithYear(),
                                                        teams.Where(t => t.TeamDetail.Wins + t.TeamDetail.Losses + t.TeamDetail.Ties > 0).OrderBy(t => t.TeamDetail.RecordWinPerc()).First().TeamDetail.RecordDisplayWithYear());
                results.Rows.Add("Record", records.best, records.worst);

                // Winning/Losing Streak
                Team longest_winning_streak = teams.Where(t => t.TeamDetail.Wins + t.TeamDetail.Losses + t.TeamDetail.Ties > 0).OrderByDescending(t => t.TeamDetail.LongestWinningStreak).First();
                Team longest_losing_streak = teams.Where(t => t.TeamDetail.Wins + t.TeamDetail.Losses + t.TeamDetail.Ties > 0).OrderByDescending(t => t.TeamDetail.LongestLosingStreak).First();
                (string best, string worst) streaks = (longest_winning_streak.TeamDetail.LongestWinningStreak + " (" + longest_winning_streak.LeagueSeason.Season.Name + ")",
                                                        longest_losing_streak.TeamDetail.LongestLosingStreak + " (" + longest_losing_streak.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Winning/Losing Streak", streaks.best, streaks.worst);

                // Points Scored
                Team best_points_scored = teams.OrderByDescending(t => t.TeamDetail.PointsScored).First();
                Team worst_points_scored = teams.OrderBy(t => t.TeamDetail.PointsScored).First();
                (string best, string worst) points_scored = (best_points_scored.TeamDetail.PointsScored.ToString() + " (" + best_points_scored.LeagueSeason.Season.Name + ")",
                                                        worst_points_scored.TeamDetail.PointsScored.ToString() + " (" + worst_points_scored.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Points Scored (Season)", points_scored.best, points_scored.worst);

                // Points Scored Per Week
                Team best_points_scored_per_week = teams.OrderByDescending(t => t.TeamDetail.PointsScored / (double)t.LeagueSeason.NumWeeks).First();
                Team worst_points_scored_per_week = teams.OrderBy(t => t.TeamDetail.PointsScored / (double)t.LeagueSeason.NumWeeks).First();
                (string best, string worst) points_scored_per_week = (Math.Round(best_points_scored_per_week.TeamDetail.PointsScored / (double)best_points_scored_per_week.LeagueSeason.NumWeeks, 2).ToString("F2") + " (" + best_points_scored_per_week.LeagueSeason.Season.Name + ")",
                                                                        Math.Round(worst_points_scored_per_week.TeamDetail.PointsScored / (double)worst_points_scored_per_week.LeagueSeason.NumWeeks, 2).ToString("F2") + " (" + worst_points_scored_per_week.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Points Scored Per Week", points_scored_per_week.best, points_scored_per_week.worst);

                // Relative Points Scored (Season)
                Team best_rel_points_scored = teams.OrderByDescending(t => t.TeamDetail.PointsScored / (double)t.LeagueSeason.AvgPoints).First();
                Team worst_rel_points_scored = teams.OrderBy(t => t.TeamDetail.PointsScored / (double)t.LeagueSeason.AvgPoints).First();
                (string best, string worst) rel_points_scored = (Math.Round(((best_rel_points_scored.TeamDetail.PointsScored / (double)best_rel_points_scored.LeagueSeason.AvgPoints) - 1) * 100, 3).ToString("F3") + "% (" + best_rel_points_scored.LeagueSeason.Season.Name + ")",
                                                                        Math.Round(((worst_rel_points_scored.TeamDetail.PointsScored / (double)worst_rel_points_scored.LeagueSeason.AvgPoints) - 1) * 100, 3).ToString("F3") + "% (" + worst_rel_points_scored.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Relative Points Scored (Season)", rel_points_scored.best, rel_points_scored.worst);

                List<Team> points_against_teams = teams.Where(t => t.TeamDetail.PointsAgainst != null).ToList();
                // Points Against
                Team best_points_against = points_against_teams.OrderByDescending(t => t.TeamDetail.PointsAgainst).First();
                Team worst_points_against = points_against_teams.OrderBy(t => t.TeamDetail.PointsAgainst).First();
                (string best, string worst) points_against = (best_points_against.TeamDetail.PointsAgainst.ToString() + " (" + best_points_against.LeagueSeason.Season.Name + ")",
                                                        worst_points_against.TeamDetail.PointsAgainst.ToString() + " (" + worst_points_against.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Points Against (Season)", points_against.best, points_against.worst);

                // Points Against Per Week
                Team best_points_against_per_week = points_against_teams.OrderByDescending(t => t.TeamDetail.PointsAgainst / (double)t.LeagueSeason.NumWeeks).First();
                Team worst_points_against_per_week = points_against_teams.OrderBy(t => t.TeamDetail.PointsAgainst / (double)t.LeagueSeason.NumWeeks).First();
                (string best, string worst) points_against_per_week = (Math.Round((double)best_points_against_per_week.TeamDetail.PointsAgainst / (double)best_points_against_per_week.LeagueSeason.NumWeeks, 2).ToString("F2") + " (" + best_points_against_per_week.LeagueSeason.Season.Name + ")",
                                                                        Math.Round((double)worst_points_against_per_week.TeamDetail.PointsAgainst / (double)worst_points_against_per_week.LeagueSeason.NumWeeks, 2).ToString("F2") + " (" + worst_points_against_per_week.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Points Against Per Week", points_against_per_week.best, points_against_per_week.worst);

                // Points Against Per Week
                Team best_rel_points_against = points_against_teams.OrderByDescending(t => t.TeamDetail.PointsAgainst / (double)t.LeagueSeason.AvgPoints).First();
                Team worst_rel_points_against = points_against_teams.OrderBy(t => t.TeamDetail.PointsAgainst / (double)t.LeagueSeason.AvgPoints).First();
                (string best, string worst) rel_points_against = (Math.Round((((double)best_rel_points_against.TeamDetail.PointsAgainst / (double)best_rel_points_against.LeagueSeason.AvgPoints) - 1) * 100, 3).ToString("F3") + "% (" + best_rel_points_against.LeagueSeason.Season.Name + ")",
                                                                        Math.Round((((double)worst_rel_points_against.TeamDetail.PointsAgainst / (double)worst_rel_points_against.LeagueSeason.AvgPoints) - 1) * 100, 3).ToString("F3") + "% (" + worst_rel_points_against.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Relative Points Against (Season)", rel_points_against.best, rel_points_against.worst);

                // Gathering matchup details for later records
                var all_matchup_details = teams.SelectMany(team => team.MatchupDetails.Select(md => new
                                             {
                                                 Team = team,
                                                 MatchupDetail = md,
                                                 Opponent = md.Matchup.MatchupDetails.FirstOrDefault(opp => opp.TeamId != md.TeamId)
                                             }))
                                            .Where(x => x.Opponent != null)
                                            .Select(x => new
                                            {
                                                Points = x.MatchupDetail.TeamPoints,
                                                ProjPoints = x.MatchupDetail.TeamProjPoints,
                                                OppPoints = x.Opponent.TeamPoints,
                                                ProjOppPoints = x.Opponent.TeamProjPoints,
                                                Week = x.MatchupDetail.Matchup.Week,
                                                LeagueSeasonId = x.MatchupDetail.Matchup.LeagueSeasonId,
                                                Season = x.Team.LeagueSeason.Season.Name,
                                                OpponentManager = x.Opponent.Team.Manager.Nickname ?? x.Opponent.Team.Manager.FirstName,
                                                isRivalryWeek = x.MatchupDetail.Matchup.RivalryWeekMatchup,
                                                isNutCup = x.MatchupDetail.Matchup.NutCupMatchup
                                            })
                                            .ToList();
                // Gathering average points scored in each week for later records
                var avg_points_scored_week_query = context.MatchupDetails
                                                .Include(md => md.Matchup)
                                                    .ThenInclude(mat => mat.LeagueSeason)
                                                .Where(md => permittedLeagues.Contains(md.Matchup.LeagueSeason.LeagueId))
                                                .AsQueryable();

                if (!checkBoxUseAllLeagues.Checked)
                {
                    avg_points_scored_week_query = avg_points_scored_week_query.Where(t => selectedLeagues.Contains(t.Matchup.LeagueSeason.LeagueId));
                }

                var avg_points_scored_week = avg_points_scored_week_query.GroupBy(md => new
                                                 {
                                                     md.Team.LeagueSeasonId,
                                                     md.Matchup.Week
                                                 })
                                                .Select(g => new
                                                {
                                                    LeagueSeasonId = g.Key.LeagueSeasonId,
                                                    Week = g.Key.Week,
                                                    AveragePoints = g.Average(md => md.TeamPoints)
                                                })
                                                .ToList();

                // Points Scored (Week)
                var best_points_scored_week = all_matchup_details.OrderByDescending(x => x.Points).FirstOrDefault();
                var worst_points_scored_week = all_matchup_details.OrderBy(x => x.Points).FirstOrDefault();
                (string best, string worst) points_scored_week = (best_points_scored_week.Points.ToString() + " (vs " + best_points_scored_week.OpponentManager + ", W" + best_points_scored_week.Week + ", " + best_points_scored_week.Season + ")",
                                                        worst_points_scored_week.Points.ToString() + " (vs " + worst_points_scored_week.OpponentManager + ", W" + worst_points_scored_week.Week + ", " + worst_points_scored_week.Season + ")");
                results.Rows.Add("Points Scored (Week)", points_scored_week.best, points_scored_week.worst);

                // Relative Points Scored (Week)
                var best_rel_points_scored_week = all_matchup_details.OrderByDescending(t => t.Points / avg_points_scored_week.Where(w => w.LeagueSeasonId == t.LeagueSeasonId && w.Week == t.Week).First().AveragePoints).First();
                var worst_rel_points_scored_week = all_matchup_details.OrderBy(t => t.Points / avg_points_scored_week.Where(w => w.LeagueSeasonId == t.LeagueSeasonId && w.Week == t.Week).First().AveragePoints).First();
                (string best, string worst) rel_points_scored_week = (Math.Round(((best_rel_points_scored_week.Points / avg_points_scored_week.Where(w => w.LeagueSeasonId == best_rel_points_scored_week.LeagueSeasonId && w.Week == best_rel_points_scored_week.Week).First().AveragePoints) - 1) * 100, 3).ToString("F3") + "% (vs " + best_rel_points_scored_week.OpponentManager + ", W" + best_rel_points_scored_week.Week + ", " + best_rel_points_scored_week.Season + ")",
                                                                        Math.Round(((worst_rel_points_scored_week.Points / avg_points_scored_week.Where(w => w.LeagueSeasonId == worst_rel_points_scored_week.LeagueSeasonId && w.Week == worst_rel_points_scored_week.Week).First().AveragePoints) - 1) * 100, 3).ToString("F3") + "% (vs " + worst_rel_points_scored_week.OpponentManager + ", W" + worst_rel_points_scored_week.Week + ", " + worst_rel_points_scored_week.Season + ")");
                results.Rows.Add("Relative Points Scored (Week)", rel_points_scored_week.best, rel_points_scored_week.worst);

                // Relative Points Scored vs Proj (Week)
                var best_rel_points_scored_vs_proj_week = all_matchup_details.OrderByDescending(t => t.Points / t.ProjPoints).First();
                var worst_rel_points_scored_vs_proj_week = all_matchup_details.OrderBy(t => t.Points / t.ProjPoints).First();
                (string best, string worst) rel_points_scored_vs_proj_week = (Math.Round(((best_rel_points_scored_vs_proj_week.Points / (double)best_rel_points_scored_vs_proj_week.ProjPoints) - 1) * 100, 3).ToString("F3") + "% (vs " + best_rel_points_scored_vs_proj_week.OpponentManager + ", W" + best_rel_points_scored_vs_proj_week.Week + ", " + best_rel_points_scored_vs_proj_week.Season + ")",
                                                                        Math.Round(((worst_rel_points_scored_vs_proj_week.Points / (double)worst_rel_points_scored_vs_proj_week.ProjPoints) - 1) * 100, 3).ToString("F3") + "% (vs " + worst_rel_points_scored_vs_proj_week.OpponentManager + ", W" + worst_rel_points_scored_vs_proj_week.Week + ", " + worst_rel_points_scored_vs_proj_week.Season + ")");
                results.Rows.Add("Relative Points Scored vs Proj (Week)", rel_points_scored_vs_proj_week.best, rel_points_scored_vs_proj_week.worst);

                // Relative Points Scored vs Proj (Season)
                results.Rows.Add("Relative Points Scored vs Proj (Season)", "-", "-");

                // Points Against (Week)
                var best_points_against_week = all_matchup_details.OrderByDescending(x => x.OppPoints).FirstOrDefault();
                var worst_points_against_week = all_matchup_details.OrderBy(x => x.OppPoints).FirstOrDefault();
                (string best, string worst) points_against_week = (best_points_against_week.OppPoints.ToString() + " (vs " + best_points_against_week.OpponentManager + ", W" + best_points_against_week.Week + ", " + best_points_against_week.Season + ")",
                                                        worst_points_against_week.OppPoints.ToString() + " (vs " + worst_points_against_week.OpponentManager + ", W" + worst_points_against_week.Week + ", " + worst_points_against_week.Season + ")");
                results.Rows.Add("Points Against (Week)", points_against_week.best, points_against_week.worst);

                // Relative Points Against (Week)
                var best_rel_points_against_week = all_matchup_details.OrderByDescending(t => t.OppPoints / avg_points_scored_week.Where(w => w.LeagueSeasonId == t.LeagueSeasonId && w.Week == t.Week).First().AveragePoints).First();
                var worst_rel_points_against_week = all_matchup_details.OrderBy(t => t.OppPoints / avg_points_scored_week.Where(w => w.LeagueSeasonId == t.LeagueSeasonId && w.Week == t.Week).First().AveragePoints).First();
                (string best, string worst) rel_points_against_week = (Math.Round(((best_rel_points_against_week.OppPoints / avg_points_scored_week.Where(w => w.LeagueSeasonId == best_rel_points_against_week.LeagueSeasonId && w.Week == best_rel_points_against_week.Week).First().AveragePoints) - 1) * 100, 3).ToString("F3") + "% (vs " + best_rel_points_against_week.OpponentManager + ", W" + best_rel_points_against_week.Week + ", " + best_rel_points_against_week.Season + ")",
                                                                        Math.Round(((worst_rel_points_against_week.OppPoints / avg_points_scored_week.Where(w => w.LeagueSeasonId == worst_rel_points_against_week.LeagueSeasonId && w.Week == worst_rel_points_against_week.Week).First().AveragePoints) - 1) * 100, 3).ToString("F3") + "% (vs " + worst_rel_points_against_week.OpponentManager + ", W" + worst_rel_points_against_week.Week + ", " + worst_rel_points_against_week.Season + ")");
                results.Rows.Add("Relative Points Against (Week)", rel_points_against_week.best, rel_points_against_week.worst);

                // Relative Points Against vs Proj (Week)
                var best_rel_points_against_vs_proj_week = all_matchup_details.OrderByDescending(t => t.OppPoints / t.ProjOppPoints).First();
                var worst_rel_points_against_vs_proj_week = all_matchup_details.OrderBy(t => t.OppPoints / t.ProjOppPoints).First();
                (string best, string worst) rel_points_against_vs_proj_week = (Math.Round(((best_rel_points_against_vs_proj_week.OppPoints / (double)best_rel_points_against_vs_proj_week.ProjOppPoints) - 1) * 100, 3).ToString("F3") + "% (vs " + best_rel_points_against_vs_proj_week.OpponentManager + ", W" + best_rel_points_against_vs_proj_week.Week + ", " + best_rel_points_against_vs_proj_week.Season + ")",
                                                                        Math.Round(((worst_rel_points_against_vs_proj_week.OppPoints / (double)worst_rel_points_against_vs_proj_week.ProjOppPoints) - 1) * 100, 3).ToString("F3") + "% (vs " + worst_rel_points_against_vs_proj_week.OpponentManager + ", W" + worst_rel_points_against_vs_proj_week.Week + ", " + worst_rel_points_against_vs_proj_week.Season + ")");
                results.Rows.Add("Relative Points Against vs Proj (Week)", rel_points_against_vs_proj_week.best, rel_points_against_vs_proj_week.worst);

                // Relative Points Against vs Proj (Season)
                results.Rows.Add("Relative Points Against vs Proj (Season)", "-", "-");

                // Moves
                Team best_moves = teams.OrderByDescending(t => t.TeamDetail.Moves).First();
                Team worst_moves = teams.OrderBy(t => t.TeamDetail.Moves).First();
                (string best, string worst) moves = (best_moves.TeamDetail.Moves.ToString() + " (" + best_moves.LeagueSeason.Season.Name + ")",
                                                        worst_moves.TeamDetail.Moves.ToString() + " (" + worst_moves.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Moves", moves.best, moves.worst);

                // Relative Moves (Season)
                Team best_rel_moves = teams.OrderByDescending(t => t.TeamDetail.Moves / (double)t.LeagueSeason.AvgMoves).First();
                Team worst_rel_moves = teams.OrderBy(t => t.TeamDetail.Moves / (double)t.LeagueSeason.AvgMoves).First();
                (string best, string worst) rel_moves = (Math.Round(((double)(best_rel_moves.TeamDetail.Moves / (double)best_rel_moves.LeagueSeason.AvgMoves) - 1) * 100, 3).ToString("F3") + "% (" + best_rel_moves.LeagueSeason.Season.Name + ")",
                                                                        Math.Round(((double)(worst_rel_moves.TeamDetail.Moves / (double)worst_rel_moves.LeagueSeason.AvgMoves) - 1) * 100, 3).ToString("F3") + "% (" + worst_rel_moves.LeagueSeason.Season.Name + ")");
                results.Rows.Add("Relative Moves (Season)", rel_moves.best, rel_moves.worst);

                // Margin of Victory
                var best_mov= all_matchup_details.OrderByDescending(x => x.Points - x.OppPoints).FirstOrDefault();
                var worst_mov= all_matchup_details.OrderBy(x => x.Points - x.OppPoints).FirstOrDefault();
                (string best, string worst) mov= (Math.Round(best_mov.Points - best_mov.OppPoints, 2).ToString("F2") + " (vs " + best_mov.OpponentManager + ", W" + best_mov.Week + ", " + best_mov.Season + ")",
                                                        Math.Round(worst_mov.Points - worst_mov.OppPoints, 2).ToString("F2") + " (vs " + worst_mov.OpponentManager + ", W" + worst_mov.Week + ", " + worst_mov.Season + ")");
                results.Rows.Add("Margin of Victory", mov.best, mov.worst);

                // Rivalry Week Margin of Victory
                var best_rivalry_week_mov = all_matchup_details.Where(x => x.isRivalryWeek == 1).OrderByDescending(x => x.Points - x.OppPoints).FirstOrDefault();
                var worst_rivalry_week_mov = all_matchup_details.Where(x => x.isRivalryWeek == 1).OrderBy(x => x.Points - x.OppPoints).FirstOrDefault();
                (string best, string worst) rivalry_week_mov = (Math.Round(best_rivalry_week_mov.Points - best_rivalry_week_mov.OppPoints, 2).ToString("F2") + " (vs " + best_rivalry_week_mov.OpponentManager + ", W" + best_rivalry_week_mov.Week + ", " + best_rivalry_week_mov.Season + ")",
                                                        Math.Round(worst_rivalry_week_mov.Points - worst_rivalry_week_mov.OppPoints, 2).ToString("F2") + " (vs " + worst_rivalry_week_mov.OpponentManager + ", W" + worst_rivalry_week_mov.Week + ", " + worst_rivalry_week_mov.Season + ")");
                results.Rows.Add("Rivalry Week Margin of Victory", rivalry_week_mov.best, rivalry_week_mov.worst);

                // Nut Cup Margin of Victory
                var best_nut_cup_mov = all_matchup_details.Where(x => x.isNutCup == 1).OrderByDescending(x => x.Points - x.OppPoints).FirstOrDefault();
                var worst_nut_cup_mov = all_matchup_details.Where(x => x.isNutCup == 1).OrderBy(x => x.Points - x.OppPoints).FirstOrDefault();
                (string best, string worst) nut_cup_mov = (Math.Round(best_nut_cup_mov.Points - best_nut_cup_mov.OppPoints, 2).ToString("F2") + " (vs " + best_nut_cup_mov.OpponentManager + ", W" + best_nut_cup_mov.Week + ", " + best_nut_cup_mov.Season + ")",
                                                        Math.Round(worst_nut_cup_mov.Points - worst_nut_cup_mov.OppPoints, 2).ToString("F2") + " (vs " + worst_nut_cup_mov.OpponentManager + ", W" + worst_nut_cup_mov.Week + ", " + worst_nut_cup_mov.Season + ")");
                results.Rows.Add("Nut Cup Margin of Victory", nut_cup_mov.best, nut_cup_mov.worst);


                dataGridViewManager.DataSource = results;
            }
        }
    }
}
