using FantasyFootballApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using static FantasyFootballApp.HomeForm;

namespace FantasyFootballApp
{
    public partial class AllPlayForm : Form
    {
        public int selectedLeague = -1;
        public int selectedReport = -1;

        public AllPlayForm()
        {
            InitializeComponent();
        }

        public void AllPlayForm_Load(object sender, EventArgs e)
        {
            LoadLeagueComboBox();

            // Generate Report ComboBox Items
            List<ComboBoxItem> reports = new List<ComboBoxItem>();

            reports.Add(new ComboBoxItem { ID = 1, Display = "All-Time" });
            reports.Add(new ComboBoxItem { ID = 2, Display = "All-Time (Regular Season Only)" });
            reports.Add(new ComboBoxItem { ID = 3, Display = "All-Time (Playoffs Only)" });
            reports.Add(new ComboBoxItem { ID = 4, Display = "Seasons" });
            reports.Add(new ComboBoxItem { ID = 5, Display = "Seasons (Regular Season Only)" });
            reports.Add(new ComboBoxItem { ID = 6, Display = "Seasons (Playoffs Only)" });

            comboBoxReport.DataSource = reports.ToArray();
            comboBoxReport.DisplayMember = "Display";
            comboBoxReport.ValueMember = "ID";

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

        private void buttonAllPlayReport_Click(object sender, EventArgs e)
        {
            if (selectedLeague == -1)
            {
                ErrorMessages.LeagueRequiredMessageBox();
            }
            else
            {
                dataGridViewAllPlay.Columns.Clear();
                switch (selectedReport)
                {
                    case 1:
                        AllTimeAllPlayReport();
                        break;
                    case 2:
                        AllTimeAllPlayReport(true);
                        break;
                    case 3:
                        AllTimeAllPlayReport(false, true);
                        break;
                    case 4:
                        AllTimeSeasonsReport();
                        break;
                    case 5:
                        AllTimeSeasonsReport(true);
                        break;
                    case 6:
                        AllTimeSeasonsReport(false, true);
                        break;
                    default:
                        ErrorMessages.ReportCouldNotBeRanMessageBox();
                        break;

                }
            }
        }

        private void comboBoxLeague_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxLeague.SelectedValue != null)
            {
                selectedLeague = (int)comboBoxLeague.SelectedValue;
            }
        }

        private void comboBoxReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxReport.SelectedValue != null)
            {
                selectedReport = (int)comboBoxReport.SelectedValue;
            }
        }

        private void AllTimeAllPlayReport(bool regular_season_only = false, bool playoffs_only = false)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var all_league_matchup_ids_query = context.Matchups
                                        .Where(mat => mat.LeagueSeason.LeagueId == selectedLeague && (regular_season_only ? mat.PlayoffMatchup == 0 : true) && (playoffs_only ? mat.PlayoffMatchup == 1 : true))
                                        .Include(mat => mat.LeagueSeason)
                                            .ThenInclude(ls => ls.Season)
                                        .AsQueryable();

                if (textBoxSeasonStart.Text.Length > 0 && int.TryParse(textBoxSeasonStart.Text.ToString(), out int season_start))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.LeagueSeason.Season.Name >= season_start);
                }
                if (textBoxSeasonEnd.Text.Length > 0 && int.TryParse(textBoxSeasonEnd.Text.ToString(), out int season_end))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.LeagueSeason.Season.Name <= season_end);
                }
                if (textBoxWeekStart.Text.Length > 0 && int.TryParse(textBoxWeekStart.Text.ToString(), out int week_start))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.Week >= week_start);
                }
                if (textBoxWeekEnd.Text.Length > 0 && int.TryParse(textBoxWeekEnd.Text.ToString(), out int week_end))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.Week <= week_end);
                }

                List<int> all_league_matchup_ids = all_league_matchup_ids_query.Select(mat => mat.Id).ToList();

                List<MatchupDetail> all_league_matchup_details = context.MatchupDetails.Where(md => all_league_matchup_ids.Contains(md.MatchupId) && (checkBoxIncludeByeWeeks.Checked ? true : md.Matchup.ByeWeek == 0))
                                                                            .Include(md => md.Matchup)
                                                                                .ThenInclude(m => m.MatchupDetails)
                                                                            .Include(md => md.Team)
                                                                                .ThenInclude(t => t.Manager)
                                                                            .ToList();
                List<Manager> all_league_managers = context.Managers.Where(m => m.Teams.Any(t => t.LeagueSeason.LeagueId == selectedLeague
                                                                                                    && (int.TryParse(textBoxSeasonStart.Text.ToString(), out season_start) ? t.LeagueSeason.Season.Name >= season_start : true)
                                                                                                    && (int.TryParse(textBoxSeasonEnd.Text.ToString(), out season_end) ? t.LeagueSeason.Season.Name <= season_end : true)
                                                                                                    && all_league_matchup_details.Select(md => md.Team.ManagerId).ToList().Contains(m.Id)))
                                                                            .Include(md => md.Teams)
                                                                                .ThenInclude(t => t.LeagueSeason)
                                                                            .ToList();

                DataTable results = new DataTable();
                List<(Manager manager,
                          int wins,
                          int losses,
                          int ties,
                          int ap_wins,
                          int ap_losses,
                          int ap_ties,
                          int ap_no_opp_wins,
                          int ap_no_opp_losses,
                          int ap_no_opp_ties,
                          int ap_h2h_wins,
                          int ap_h2h_losses,
                          int ap_h2h_ties)> all_play_data = new List<(Manager, int, int, int, int, int, int, int, int, int, int, int, int)>();

                bool has_no_opp_games = false;
                bool has_h2h_games = false;
                foreach (Manager manager in all_league_managers)
                {
                    List<MatchupDetail> all_manager_matchup_details = all_league_matchup_details.Where(md => md.Team.ManagerId == manager.Id).ToList();
                    int wins = 0;
                    int losses = 0;
                    int ties = 0;
                    int ap_wins = 0;
                    int ap_losses = 0;
                    int ap_ties = 0;
                    int ap_no_opp_wins = 0;
                    int ap_no_opp_losses = 0;
                    int ap_no_opp_ties = 0;
                    int ap_h2h_wins = 0;
                    int ap_h2h_losses = 0;
                    int ap_h2h_ties = 0;
                    foreach (MatchupDetail md in all_manager_matchup_details)
                    {
                        bool existing_h2h = false;
                        bool existing_opp = true;
                        MatchupDetail? opp_md = md.Matchup.MatchupDetails.Where(opp_md => opp_md.Id != md.Id).FirstOrDefault();
                        if (opp_md != null)
                        {
                            if (md.TeamPoints > opp_md.TeamPoints) wins++;
                            else if (md.TeamPoints < opp_md.TeamPoints) losses++;
                            else if (md.TeamPoints == opp_md.TeamPoints) ties++;

                            has_h2h_games = true;
                            existing_h2h = true;
                        }
                        else
                        {
                            existing_opp = false;
                            has_no_opp_games = true;
                        }

                        List<MatchupDetail> league_week_matchup_details = all_league_matchup_details
                                                                        .Where(mat_det => mat_det.Matchup.LeagueSeasonId == md.Matchup.LeagueSeasonId && mat_det.Matchup.Week == md.Matchup.Week && mat_det.Id != md.Id)
                                                                        .ToList();
                        foreach (MatchupDetail ap_opp_md in league_week_matchup_details)
                        {
                            if (md.TeamPoints > ap_opp_md.TeamPoints)
                            {
                                ap_wins++;
                                if (existing_h2h) ap_h2h_wins++;
                                if (!existing_opp) ap_no_opp_wins++;
                            }
                            else if (md.TeamPoints < ap_opp_md.TeamPoints)
                            {
                                ap_losses++;
                                if (existing_h2h) ap_h2h_losses++;
                                if (!existing_opp) ap_no_opp_losses++;
                            }
                            else if (md.TeamPoints == ap_opp_md.TeamPoints)
                            {
                                ap_ties++;
                                if (existing_h2h) ap_h2h_ties++;
                                if (!existing_opp) ap_no_opp_ties++;
                            }
                        }
                    }
                    all_play_data.Add((manager, wins, losses, ties, ap_wins, ap_losses, ap_ties, ap_no_opp_wins, ap_no_opp_losses, ap_no_opp_ties, ap_h2h_wins, ap_h2h_losses, ap_h2h_ties));
                }

                results.Columns.Add("Manager", typeof(string));
                results.Columns.Add("All-Play Record", typeof(string));
                results.Columns.Add("All-Play Win %", typeof(string));
                if (has_h2h_games)
                {
                    if (has_no_opp_games)
                    {
                        results.Columns.Add("All-Play Record (No Opp Games)", typeof(string));
                        results.Columns.Add("All-Play Win % (No Opp Games)", typeof(string));
                        results.Columns.Add("All-Play Record (H2H Games)", typeof(string));
                        results.Columns.Add("All-Play Win % (H2H Games)", typeof(string));
                    }
                    results.Columns.Add("Actual Record", typeof(string));
                    results.Columns.Add("Actual Win %", typeof(string));
                    results.Columns.Add("All-Play vs Actual Win %", typeof(string));
                }

                foreach (var man in all_play_data)
                {
                    string no_opp_all_play_record = man.ap_no_opp_wins + man.ap_no_opp_losses + man.ap_no_opp_ties > 0
                                            ? (man.ap_no_opp_ties == 0
                                                ? man.ap_no_opp_wins + "-" + man.ap_no_opp_losses
                                                : man.ap_no_opp_wins + "-" + man.ap_no_opp_losses + "-" + man.ap_no_opp_ties)
                                            : "-";
                    string no_opp_all_play_win_perc = man.ap_no_opp_wins + man.ap_no_opp_losses + man.ap_no_opp_ties > 0
                                            ? (((double)man.ap_no_opp_wins + (0.5 * (double)man.ap_no_opp_ties)) / (double)(man.ap_no_opp_wins + man.ap_no_opp_losses + man.ap_no_opp_ties)).ToString("F5")
                                            : "-";
                    string h2h_all_play_record = man.ap_h2h_wins + man.ap_h2h_losses + man.ap_h2h_ties > 0
                                            ? (man.ap_h2h_ties == 0
                                                ? man.ap_h2h_wins + "-" + man.ap_h2h_losses
                                                : man.ap_h2h_wins + "-" + man.ap_h2h_losses + "-" + man.ap_h2h_ties)
                                            : "-";
                    string h2h_all_play_win_perc = man.ap_h2h_wins + man.ap_h2h_losses + man.ap_h2h_ties > 0
                                            ? (((double)man.ap_h2h_wins + (0.5 * (double)man.ap_h2h_ties)) / (double)(man.ap_h2h_wins + man.ap_h2h_losses + man.ap_h2h_ties)).ToString("F5")
                                            : "-";
                    string actual_record = man.wins + man.losses + man.ties > 0
                                            ? (man.ties == 0
                                                ? man.wins + "-" + man.losses
                                                : man.wins + "-" + man.losses + "-" + man.ties)
                                            : "-";
                    string actual_win_perc = man.wins + man.losses + man.ties > 0
                                            ? (((double)man.wins + (0.5 * (double)man.ties)) / (double)(man.wins + man.losses + man.ties)).ToString("F5")
                                            : "-";
                    string actual_win_perc_vs_all_play_win_perc_diff = man.wins + man.losses + man.ties > 0
                                            ? ((((double)man.ap_h2h_wins + (0.5 * (double)man.ap_h2h_ties)) / (double)(man.ap_h2h_wins + man.ap_h2h_losses + man.ap_h2h_ties)) - (((double)man.wins + (0.5 * (double)man.ties)) / (double)(man.wins + man.losses + man.ties))).ToString("F5")
                                            : "-";
                    if (has_h2h_games && has_no_opp_games)
                    {
                        //  ** FULL **
                        results.Rows.Add(man.manager.FirstName + " " + man.manager.LastName,
                                            man.ap_ties == 0
                                                    ? man.ap_wins + "-" + man.ap_losses
                                                    : man.ap_wins + "-" + man.ap_losses + "-" + man.ap_ties,
                                            (((double)man.ap_wins + (0.5 * (double)man.ap_ties)) / (double)(man.ap_wins + man.ap_losses + man.ap_ties)).ToString("F5"),
                                            no_opp_all_play_record,
                                            no_opp_all_play_win_perc,
                                            h2h_all_play_record,
                                            h2h_all_play_win_perc,
                                            actual_record,
                                            actual_win_perc,
                                            double.TryParse(actual_win_perc_vs_all_play_win_perc_diff, out double output)
                                                ? double.Parse(actual_win_perc_vs_all_play_win_perc_diff) >= 0
                                                    ? "+" + actual_win_perc_vs_all_play_win_perc_diff
                                                    : actual_win_perc_vs_all_play_win_perc_diff
                                                : actual_win_perc_vs_all_play_win_perc_diff);
                    }
                    else if (has_h2h_games)
                    {
                        //  ** H2H **
                        results.Rows.Add(man.manager.FirstName + " " + man.manager.LastName,
                                            man.ap_ties == 0
                                                ? man.ap_wins + "-" + man.ap_losses
                                                : man.ap_wins + "-" + man.ap_losses + "-" + man.ap_ties,
                                            (((double)man.ap_wins + (0.5 * (double)man.ap_ties)) / (double)(man.ap_wins + man.ap_losses + man.ap_ties)).ToString("F5"),
                                            actual_record,
                                            actual_win_perc,
                                            double.TryParse(actual_win_perc_vs_all_play_win_perc_diff, out double output)
                                                ? double.Parse(actual_win_perc_vs_all_play_win_perc_diff) >= 0
                                                    ? "+" + actual_win_perc_vs_all_play_win_perc_diff
                                                    : actual_win_perc_vs_all_play_win_perc_diff
                                                : actual_win_perc_vs_all_play_win_perc_diff);
                    }
                    else
                    {
                        //  ** BASE 3 FIELDS ONLY **
                        results.Rows.Add(man.manager.FirstName + " " + man.manager.LastName,
                                            man.ap_ties == 0
                                                ? man.ap_wins + "-" + man.ap_losses
                                                : man.ap_wins + "-" + man.ap_losses + "-" + man.ap_ties,
                                            (((double)man.ap_wins + (0.5 * (double)man.ap_ties)) / (double)(man.ap_wins + man.ap_losses + man.ap_ties)).ToString("F5"));
                    }
                }

                dataGridViewAllPlay.DataSource = results;
                dataGridViewAllPlay.Sort(dataGridViewAllPlay.Columns[2], ListSortDirection.Descending);
            }
        }

        private void AllTimeSeasonsReport(bool regular_season_only = false, bool playoffs_only = false)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var all_league_matchup_ids_query = context.Matchups
                                        .Where(mat => mat.LeagueSeason.LeagueId == selectedLeague && (regular_season_only ? mat.PlayoffMatchup == 0 : true) && (playoffs_only ? mat.PlayoffMatchup == 1 : true))
                                        .Include(mat => mat.LeagueSeason)
                                            .ThenInclude(ls => ls.Season)
                                        .AsQueryable();

                if (textBoxSeasonStart.Text.Length > 0 && int.TryParse(textBoxSeasonStart.Text.ToString(), out int season_start))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.LeagueSeason.Season.Name >= season_start);
                }
                if (textBoxSeasonEnd.Text.Length > 0 && int.TryParse(textBoxSeasonEnd.Text.ToString(), out int season_end))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.LeagueSeason.Season.Name <= season_end);
                }
                if (textBoxWeekStart.Text.Length > 0 && int.TryParse(textBoxWeekStart.Text.ToString(), out int week_start))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.Week >= week_start);
                }
                if (textBoxWeekEnd.Text.Length > 0 && int.TryParse(textBoxWeekEnd.Text.ToString(), out int week_end))
                {
                    all_league_matchup_ids_query = all_league_matchup_ids_query.Where(mat => mat.Week <= week_end);
                }

                List<int> all_league_matchup_ids = all_league_matchup_ids_query.Select(mat => mat.Id).ToList();

                List<MatchupDetail> all_league_matchup_details = context.MatchupDetails.Where(md => all_league_matchup_ids.Contains(md.MatchupId) && (checkBoxIncludeByeWeeks.Checked ? true : md.Matchup.ByeWeek == 0))
                                                                            .Include(md => md.Matchup)
                                                                                .ThenInclude(m => m.MatchupDetails)
                                                                            .Include(md => md.Matchup)
                                                                                .ThenInclude(m => m.LeagueSeason)
                                                                                .ThenInclude(ls => ls.Season)
                                                                            .Include(md => md.Team)
                                                                                .ThenInclude(t => t.Manager)
                                                                            .Include(md => md.Team)
                                                                                .ThenInclude(t => t.TeamDetail)
                                                                            .ToList();
                List<Manager> all_league_managers = context.Managers.Where(m => m.Teams.Any(t => t.LeagueSeason.LeagueId == selectedLeague
                                                                                                    && (int.TryParse(textBoxSeasonStart.Text.ToString(), out season_start) ? t.LeagueSeason.Season.Name >= season_start : true)
                                                                                                    && (int.TryParse(textBoxSeasonEnd.Text.ToString(), out season_end) ? t.LeagueSeason.Season.Name <= season_end : true)
                                                                                                    && all_league_matchup_details.Select(md => md.Team.ManagerId).ToList().Contains(m.Id)))
                                                                            .Include(md => md.Teams)
                                                                                .ThenInclude(t => t.LeagueSeason)
                                                                            .ToList();

                DataTable results = new DataTable();
                List<(Manager manager,
                          int season,
                          int finish,
                          int playoffs,
                          int playoff_seed,
                          int wins,
                          int losses,
                          int ties,
                          int ap_wins,
                          int ap_losses,
                          int ap_ties,
                          int ap_no_opp_wins,
                          int ap_no_opp_losses,
                          int ap_no_opp_ties,
                          int ap_h2h_wins,
                          int ap_h2h_losses,
                          int ap_h2h_ties)> all_play_data = new List<(Manager, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();

                bool has_h2h_games = false;
                foreach (Manager manager in all_league_managers)
                {
                    List<MatchupDetail> all_manager_matchup_details = all_league_matchup_details.Where(md => md.Team.ManagerId == manager.Id).ToList();
                    List<int> seasons = all_manager_matchup_details.Select(md => md.Matchup.LeagueSeason.Season.Name).Distinct().ToList();

                    foreach (int season in seasons)
                    {
                        List<MatchupDetail> seasonal_manager_matchup_details = all_league_matchup_details.Where(md => md.Matchup.LeagueSeason.Season.Name == season && md.Team.ManagerId == manager.Id).ToList();
                        int finish = -1;
                        int playoffs = -1;
                        int playoff_seed = -1;
                        int wins = 0;
                        int losses = 0;
                        int ties = 0;
                        int ap_wins = 0;
                        int ap_losses = 0;
                        int ap_ties = 0;
                        int ap_no_opp_wins = 0;
                        int ap_no_opp_losses = 0;
                        int ap_no_opp_ties = 0;
                        int ap_h2h_wins = 0;
                        int ap_h2h_losses = 0;
                        int ap_h2h_ties = 0;
                        foreach (MatchupDetail md in seasonal_manager_matchup_details)
                        {
                            bool existing_h2h = false;
                            bool existing_opp = true;
                            MatchupDetail? opp_md = md.Matchup.MatchupDetails.Where(opp_md => opp_md.Id != md.Id).FirstOrDefault();
                            if (opp_md != null)
                            {
                                if (md.TeamPoints > opp_md.TeamPoints) wins++;
                                else if (md.TeamPoints < opp_md.TeamPoints) losses++;
                                else if (md.TeamPoints == opp_md.TeamPoints) ties++;

                                has_h2h_games = true;
                                existing_h2h = true;
                            }
                            else existing_opp = false;

                            if (finish == -1) finish = md.Team.TeamDetail.Finish;
                            if (playoffs == -1) playoffs = md.Team.TeamDetail.Playoffs ?? -2;
                            if (playoff_seed == -1) playoff_seed = md.Team.TeamDetail.PlayoffSeed ?? -2;

                            List<MatchupDetail> league_week_matchup_details = all_league_matchup_details
                                                                            .Where(mat_det => mat_det.Matchup.LeagueSeasonId == md.Matchup.LeagueSeasonId && mat_det.Matchup.Week == md.Matchup.Week && mat_det.Id != md.Id)
                                                                            .ToList();
                            foreach (MatchupDetail ap_opp_md in league_week_matchup_details)
                            {
                                if (md.TeamPoints > ap_opp_md.TeamPoints)
                                {
                                    ap_wins++;
                                    if (existing_h2h) ap_h2h_wins++;
                                    if (!existing_opp) ap_no_opp_wins++;
                                }
                                else if (md.TeamPoints < ap_opp_md.TeamPoints)
                                {
                                    ap_losses++;
                                    if (existing_h2h) ap_h2h_losses++;
                                    if (!existing_opp) ap_no_opp_losses++;
                                }
                                else if (md.TeamPoints == ap_opp_md.TeamPoints)
                                {
                                    ap_ties++;
                                    if (existing_h2h) ap_h2h_ties++;
                                    if (!existing_opp) ap_no_opp_ties++;
                                }
                            }
                        }
                        all_play_data.Add((manager, season, finish, playoffs, playoff_seed, wins, losses, ties, ap_wins, ap_losses, ap_ties, ap_no_opp_wins, ap_no_opp_losses, ap_no_opp_ties, ap_h2h_wins, ap_h2h_losses, ap_h2h_ties));
                    }
                }

                results.Columns.Add("Season", typeof(int));
                results.Columns.Add("Manager", typeof(string));
                results.Columns.Add("Playoff Status", typeof(string));
                results.Columns.Add("Playoff Seed", typeof(string));
                results.Columns.Add("Finish", typeof(int));
                results.Columns.Add("All-Play Record", typeof(string));
                results.Columns.Add("All-Play Win %", typeof(string));
                if (has_h2h_games)
                {
                    results.Columns.Add("Actual Record", typeof(string));
                    results.Columns.Add("Actual Win %", typeof(string));
                    results.Columns.Add("All-Play vs Actual Win %", typeof(string));
                }

                foreach (var man in all_play_data)
                {
                    string actual_record = man.wins + man.losses + man.ties > 0
                                            ? (man.ties == 0
                                                ? man.wins + "-" + man.losses
                                                : man.wins + "-" + man.losses + "-" + man.ties)
                                            : "-";
                    string actual_win_perc = man.wins + man.losses + man.ties > 0
                                            ? (((double)man.wins + (0.5 * (double)man.ties)) / (double)(man.wins + man.losses + man.ties)).ToString("F5")
                                            : "-";
                    string actual_win_perc_vs_all_play_win_perc_diff = man.wins + man.losses + man.ties > 0
                                            ? ((((double)man.ap_h2h_wins + (0.5 * (double)man.ap_h2h_ties)) / (double)(man.ap_h2h_wins + man.ap_h2h_losses + man.ap_h2h_ties)) - (((double)man.wins + (0.5 * (double)man.ties)) / (double)(man.wins + man.losses + man.ties))).ToString("F5")
                                            : "-";
                    if (has_h2h_games)
                    {
                        //  ** H2H **
                        results.Rows.Add(man.season,
                                            man.manager.FirstName + " " + man.manager.LastName,
                                            man.playoffs == -2 ? "-" : man.playoffs == 1 ? "Yes" : "No",
                                            man.playoff_seed == -2 ? "-" : man.playoff_seed,
                                            man.finish,
                                            man.ap_ties == 0
                                                ? man.ap_wins + "-" + man.ap_losses
                                                : man.ap_wins + "-" + man.ap_losses + "-" + man.ap_ties,
                                            (((double)man.ap_wins + (0.5 * (double)man.ap_ties)) / (double)(man.ap_wins + man.ap_losses + man.ap_ties)).ToString("F5"),
                                            actual_record,
                                            actual_win_perc,
                                            double.TryParse(actual_win_perc_vs_all_play_win_perc_diff, out double output)
                                                ? double.Parse(actual_win_perc_vs_all_play_win_perc_diff) >= 0
                                                    ? "+" + actual_win_perc_vs_all_play_win_perc_diff
                                                    : actual_win_perc_vs_all_play_win_perc_diff
                                                : actual_win_perc_vs_all_play_win_perc_diff);
                    }
                    else
                    {
                        //  ** BASE 3 FIELDS ONLY **
                        results.Rows.Add(man.season,
                                            man.manager.FirstName + " " + man.manager.LastName,
                                            man.playoffs == -2 ? "-" : man.playoffs == 1 ? "Yes" : "No",
                                            man.playoff_seed == -2 ? "-" : man.playoff_seed,
                                            man.finish,
                                            man.ap_ties == 0
                                                ? man.ap_wins + "-" + man.ap_losses
                                                : man.ap_wins + "-" + man.ap_losses + "-" + man.ap_ties,
                                            (((double)man.ap_wins + (0.5 * (double)man.ap_ties)) / (double)(man.ap_wins + man.ap_losses + man.ap_ties)).ToString("F5"));
                    }
                }

                dataGridViewAllPlay.DataSource = results;
                dataGridViewAllPlay.Sort(dataGridViewAllPlay.Columns[6], ListSortDirection.Descending);
            }
        }


        private void textBoxWeekStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, Backspace, and Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                this.buttonAllPlayReport_Click(sender, e);
            }
        }

        private void textBoxWeekEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, Backspace, and Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                this.buttonAllPlayReport_Click(sender, e);
            }
        }

        private void textBoxSeasonEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, Backspace, and Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                this.buttonAllPlayReport_Click(sender, e);
            }
        }

        private void textBoxSeasonStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow numbers, Backspace, and Delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                this.buttonAllPlayReport_Click(sender, e);
            }
        }

        private void Week_Validating(object sender, CancelEventArgs e)
        {
            if (double.TryParse(textBoxWeekStart.Text, out double week_start) &&
                double.TryParse(textBoxWeekEnd.Text, out double week_end))
            {
                if (week_end < week_start)
                {
                    MessageBox.Show("'Week End' must be greater than 'Week Start'.", "VALIDATION ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Prevents focus from leaving the textbox
                }
            }
        }

        private void Season_Validating(object sender, CancelEventArgs e)
        {
            if (double.TryParse(textBoxSeasonStart.Text, out double season_start) &&
                double.TryParse(textBoxSeasonEnd.Text, out double season_end))
            {
                if (season_end < season_start)
                {
                    MessageBox.Show("'Season End' must be greater than 'Season Start'.", "VALIDATION ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Prevents focus from leaving the textbox
                }
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("These parameters are all optional and can be combined in any way.\n"+
                            "Here is an explanation of what each field means.\n\n" +
                            "Week Start: Only weeks equal to or later will be included\n" +
                            "Week End: Only weeks before or equal to will be included\n"+
                            "Season Start: Only seasons equal to or later will be included\n"+
                            "Season End: Only seasons before or equal to will be included", "Additional Parameters (Optional) Explanation");
        }
    }
}
