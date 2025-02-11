using FantasyFootballApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static FantasyFootballApp.HomeForm;

namespace FantasyFootballApp
{
    public partial class ManagerComparisonForm : Form
    {
        public int selectedLeague = -1;
        public int selectedFavoredManager = -1;
        public int selectedOpponentManager = -1;

        public ManagerComparisonForm()
        {
            InitializeComponent();
        }

        private void ManagerComparisonForm_Load(object sender, EventArgs e)
        {
            LoadLeagueComboBox();
        }

        private void LoadLeagueComboBox()
        {
            using (var context = new AppDbContext())
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

        private void LoadManagerBoxes()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<Manager> managers = context.Managers
                                .Include(m => m.Teams)
                                    .ThenInclude(t => t.LeagueSeason)
                                .Where(m => m.Teams.Any(t => t.LeagueSeason.LeagueId == selectedLeague))
                                .ToList();
                List<ComboBoxItem> formattedManagers = new List<ComboBoxItem>();
                formattedManagers.Add(new ComboBoxItem { ID = -1, Display = " - Select a Manager - " });
                foreach (Manager manager in managers)
                {
                    formattedManagers.Add(new ComboBoxItem { ID = manager.Id, Display = manager.FirstName + " " + manager.LastName });
                }

                comboBoxFavoredManager.DataSource = formattedManagers.ToArray();
                comboBoxFavoredManager.DisplayMember = "Display";
                comboBoxFavoredManager.ValueMember = "ID";

                comboBoxOpponent.DataSource = formattedManagers.ToArray();
                comboBoxOpponent.DisplayMember = "Display";
                comboBoxOpponent.ValueMember = "ID";
            }
        }

        private void comboBoxLeague_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxLeague.SelectedValue != null)
            {
                selectedLeague = (int)comboBoxLeague.SelectedValue;
                if ((int)comboBoxLeague.SelectedValue != -1)
                {
                    LoadManagerBoxes();
                }
                else
                {

                    comboBoxFavoredManager.DataSource = new ComboBoxItem[] { new ComboBoxItem { ID = -1, Display = " - Select a League First - " } };
                    comboBoxFavoredManager.DisplayMember = "Display";
                    comboBoxFavoredManager.ValueMember = "ID";

                    comboBoxOpponent.DataSource = new ComboBoxItem[] { new ComboBoxItem { ID = -1, Display = " - Select a League First - " } };
                    comboBoxOpponent.DisplayMember = "Display";
                    comboBoxOpponent.ValueMember = "ID";
                }
            }
        }

        private bool btnRunManagerVsManagerClickConditions()
        {
            if (selectedLeague == -1)
            {
                ErrorMessages.LeagueRequiredMessageBox();
                return false;
            }
            if (selectedFavoredManager == -1)
            {
                ErrorMessages.FavoredManagerRequiredMessageBox();
                return false;
            }
            if (selectedOpponentManager == -1)
            {
                ErrorMessages.OpponentManagerRequiredMessageBox();
                return false;
            }
            if (selectedFavoredManager == selectedOpponentManager)
            {
                ErrorMessages.UniqueManagersRequiredMessageBox();
                return false;
            }
            return true;
        }

        private void btnRunManagerVsManager_Click(object sender, EventArgs e)
        {
            // Checking conditions
            if (!btnRunManagerVsManagerClickConditions())
            {
                return;
            }

            using (AppDbContext context = new AppDbContext())
            {
                // Set Dynamic Labels
                labelYearSpecifiedRangeMatchupRecord.Text = "Last " + numericUpDownYearSpecifier.Value + " Season" + ((int)numericUpDownYearSpecifier.Value > 1 ? "s" : "");
                labelYearSpecifiedRangeFavoredMatchupRecord.Text = "Last " + numericUpDownYearSpecifier.Value + " Season" + ((int)numericUpDownYearSpecifier.Value > 1 ? "s" : "") + " (When Favored)";

                // Gather normalized league data
                var normalized_league_data = context.LeagueSeasons
                                                .Where(ls => ls.LeagueId == selectedLeague
                                                        && ls.Completed == 1)
                                                .GroupBy(ls => 1)
                                                .Select(g => new
                                                {
                                                    NormalizedLeagueSize = g.Average(ls => (int?)ls.NumTeams) ?? 8,
                                                    NormalizedGamesPlayed = g.Average(ls => (int?)ls.NumWeeks) ?? 14
                                                })
                                                .FirstOrDefault();

                // Gather all favored manager matchups
                List<int> favored_matchup_detail_ids = context.MatchupDetails
                                            .Where(md => md.Team.Manager.Id == selectedFavoredManager
                                                    && md.Matchup.LeagueSeason.LeagueId == selectedLeague)
                                            .Select(md => md.Id)
                                            .Distinct()
                                            .ToList();
                List<MatchupDetail> favored_manager_matchups = context.MatchupDetails
                                                .Where(md => favored_matchup_detail_ids.Contains(md.Id))
                                                .Include(md => md.Matchup)
                                                    .ThenInclude(m => m.MatchupDetails)
                                                    .ThenInclude(md => md.Team)
                                                    .ThenInclude(t => t.Manager)
                                                .Include(md => md.Team)
                                                    .ThenInclude(t => t.TeamDetail)
                                                .ToList();

                // Gather all opponent manager matchups
                List<int> opp_matchup_ids = context.MatchupDetails
                                        .Where(md => md.Team.Manager.Id == selectedOpponentManager
                                                && md.Matchup.LeagueSeason.LeagueId == selectedLeague)
                                        .Select(md => md.Matchup.Id)
                                        .Distinct()
                                        .ToList();
                List<int> opp_matchup_detail_ids = context.MatchupDetails
                                        .Where(md => md.Team.Manager.Id == selectedOpponentManager
                                                && md.Matchup.LeagueSeason.LeagueId == selectedLeague)
                                        .Select(md => md.Id)
                                        .Distinct()
                                        .ToList();
                List<MatchupDetail> opp_manager_matchups = context.MatchupDetails
                                                .Where(md => opp_matchup_detail_ids.Contains(md.Id))
                                                .Include(md => md.Matchup)
                                                    .ThenInclude(m => m.MatchupDetails)
                                                    .ThenInclude(md => md.Team)
                                                    .ThenInclude(t => t.Manager)
                                                .Include(md => md.Team)
                                                    .ThenInclude(t => t.Manager)
                                                .Include(md => md.Team)
                                                    .ThenInclude(t => t.TeamDetail)
                                                .ToList();
                LeagueSeason current_league_season = context.LeagueSeasons
                                .Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague)
                                .OrderByDescending(ls => ls.Season.Name)
                                .First();
                int current_league_season_id = current_league_season.Id;

                Dictionary<int, double> power_rankings = Calculations.CalculateLeaguePowerRankings(selectedLeague, current_league_season.Season.Name);

                String favored_manager_power_ranking = "-";
                String opponent_manager_power_ranking = "-";
                try
                {
                    favored_manager_power_ranking = Math.Round(power_rankings[selectedFavoredManager], 3) + " (#" + (power_rankings.Keys.Cast<object>().ToList().IndexOf(selectedFavoredManager) + 1) + ")";
                }
                catch (KeyNotFoundException exception)
                {
                }
                try
                {
                    opponent_manager_power_ranking = Math.Round(power_rankings[selectedOpponentManager], 3) + " (#" + (power_rankings.Keys.Cast<object>().ToList().IndexOf(selectedOpponentManager) + 1) + ")";
                }
                catch (KeyNotFoundException exception)
                {
                }


                // Manager 1 Comparison Values
                Manager favored_manager = context.Managers
                                                    .Where(m => m.Id == selectedFavoredManager)
                                                    .Include(m => m.Teams)
                                                        .ThenInclude(t => t.LeagueSeason)
                                                    .Include(m => m.Teams)
                                                        .ThenInclude(t => t.TeamDetail)
                                                    .First();

                int favored_manager_wins = 0;
                int favored_manager_losses = 0;
                int favored_manager_ties = 0;
                String favored_manager_all_time_regular_season_record = "-";
                List<MatchupDetail> favored_manager_regular_season_matchup_details = favored_manager_matchups.Where(md => md.Matchup.PlayoffMatchup == 0).ToList();
                foreach (MatchupDetail md in favored_manager_regular_season_matchup_details)
                {
                    MatchupDetail? opp_md = md.Matchup.MatchupDetails.Where(opp_md => opp_md.Id != md.Id).FirstOrDefault();
                    bool has_opp_matchups = false;
                    if (opp_md != null)
                    {
                        has_opp_matchups = true;
                        if (md.TeamPoints > opp_md.TeamPoints) favored_manager_wins++;
                        else if (md.TeamPoints < opp_md.TeamPoints) favored_manager_losses++;
                        else if (md.TeamPoints == opp_md.TeamPoints) favored_manager_ties++;
                    }
                    if (has_opp_matchups)
                    {
                        favored_manager_all_time_regular_season_record = favored_manager_ties == 0
                                                        ? favored_manager_wins + "-" + favored_manager_losses + " (" + Math.Round((double)favored_manager_wins / (double)(favored_manager_wins + favored_manager_losses), 5) + ")"
                                                        : favored_manager_wins + "-" + favored_manager_losses + "-" + favored_manager_ties + " (" + Math.Round(((double)favored_manager_wins + (0.5 * (double)favored_manager_ties)) / (double)(favored_manager_wins + favored_manager_losses + favored_manager_ties), 5) + ")";
                    }
                    else
                    {
                        favored_manager_all_time_regular_season_record = "0-0 (-)";
                    }
                }
                String favored_manager_avg_division_finish = favored_manager.Teams.Any(t => t.TeamDetail.DivisionFinish != null && t.LeagueSeason.LeagueId == selectedLeague)
                                                            ? favored_manager.Teams.Where(t => t.TeamDetail.DivisionFinish != null && t.LeagueSeason.LeagueId == selectedLeague).Average(t => t.TeamDetail.DivisionFinish.Value).ToString()
                                                            : "-";
                String favored_manager_avg_playoff_seed = favored_manager.Teams.Where(t => t.TeamDetail.PlayoffSeed != null && t.LeagueSeason.LeagueId == selectedLeague).Select(t => (double)t.TeamDetail.PlayoffSeed * (normalized_league_data?.NormalizedLeagueSize / (double)t.LeagueSeason.NumTeams)).DefaultIfEmpty(-1).Average().ToString() ?? "-";
                String favored_manager_avg_finish = favored_manager.Teams.Where(t => t.LeagueSeason.LeagueId == selectedLeague).Select(t => (double)t.TeamDetail.Finish * (normalized_league_data?.NormalizedLeagueSize / (double)t.LeagueSeason.NumTeams)).DefaultIfEmpty(-1).Average().ToString() ?? "-";
                int favored_manager_championships = favored_manager.Teams.Where(t => t.LeagueSeason.LeagueId == selectedLeague && t.TeamDetail.Finish == 1).Count();


                // Manager 2 Comparison Values
                Manager opponent_manager = context.Managers
                                                    .Where(m => m.Id == selectedOpponentManager)
                                                    .Include(m => m.Teams)
                                                        .ThenInclude(t => t.LeagueSeason)
                                                    .Include(m => m.Teams)
                                                        .ThenInclude(t => t.TeamDetail)
                                                    .First();

                int opponent_manager_wins = 0;
                int opponent_manager_losses = 0;
                int opponent_manager_ties = 0;
                String opponent_manager_all_time_regular_season_record = "-";
                List<MatchupDetail> opponent_manager_regular_season_matchup_details = opp_manager_matchups.Where(md => md.Matchup.PlayoffMatchup == 0).ToList();
                foreach (MatchupDetail md in opponent_manager_regular_season_matchup_details)
                {
                    MatchupDetail? opp_md = md.Matchup.MatchupDetails.Where(opp_md => opp_md.Id != md.Id).FirstOrDefault();
                    bool has_opp_matchups = false;
                    if (opp_md != null)
                    {
                        has_opp_matchups = true;
                        if (md.TeamPoints > opp_md.TeamPoints) opponent_manager_wins++;
                        else if (md.TeamPoints < opp_md.TeamPoints) opponent_manager_losses++;
                        else if (md.TeamPoints == opp_md.TeamPoints) opponent_manager_ties++;
                    }
                    if (has_opp_matchups)
                    {
                        opponent_manager_all_time_regular_season_record = opponent_manager_ties == 0
                                                        ? opponent_manager_wins + "-" + opponent_manager_losses + " (" + Math.Round((double)opponent_manager_wins / (double)(opponent_manager_wins + opponent_manager_losses), 5) + ")"
                                                        : opponent_manager_wins + "-" + opponent_manager_losses + "-" + opponent_manager_ties + " (" + Math.Round(((double)opponent_manager_wins + (0.5 * (double)opponent_manager_ties)) / (double)(opponent_manager_wins + opponent_manager_losses + opponent_manager_ties), 5) + ")";
                    }
                    else
                    {
                        opponent_manager_all_time_regular_season_record = "0-0 (-)";
                    }
                }
                String opponent_manager_avg_division_finish = opponent_manager.Teams.Any(t => t.TeamDetail.DivisionFinish != null && t.LeagueSeason.LeagueId == selectedLeague)
                                                            ? opponent_manager.Teams.Where(t => t.TeamDetail.DivisionFinish != null && t.LeagueSeason.LeagueId == selectedLeague).Average(t => t.TeamDetail.DivisionFinish.Value).ToString()
                                                            : "-";
                String opponent_manager_avg_playoff_seed = opponent_manager.Teams.Where(t => t.TeamDetail.PlayoffSeed != null && t.LeagueSeason.LeagueId == selectedLeague).Select(t => (double)t.TeamDetail.PlayoffSeed * (normalized_league_data?.NormalizedLeagueSize / (double)t.LeagueSeason.NumTeams)).DefaultIfEmpty(-1).Average().ToString() ?? "-";
                String opponent_manager_avg_finish = opponent_manager.Teams.Where(t => t.LeagueSeason.LeagueId == selectedLeague).Select(t => (double)t.TeamDetail.Finish * (normalized_league_data?.NormalizedLeagueSize / (double)t.LeagueSeason.NumTeams)).DefaultIfEmpty(-1).Average().ToString() ?? "-";
                int opponent_manager_championships = opponent_manager.Teams.Where(t => t.LeagueSeason.LeagueId == selectedLeague && t.TeamDetail.Finish == 1).Count();



                // Matchup History and Breakdown Calculations
                // Limit matchups to ones between the two selected managers
                List<MatchupDetail> selected_matchups = favored_manager_matchups.Where(md => opp_matchup_ids.Contains(md.MatchupId))
                                            .ToList();

                double favored_manager_total_points = 0;
                double opponent_manager_total_points = 0;
                int favored_wins = 0;
                int favored_favored_wins = 0;
                int favored_losses = 0;
                int favored_favored_losses = 0;
                int favored_ties = 0;
                int favored_favored_ties = 0;
                int year_specified_favored_wins = 0;
                int year_specified_favored_favored_wins = 0;
                int year_specified_favored_losses = 0;
                int year_specified_favored_favored_losses = 0;
                int year_specified_favored_ties = 0;
                int year_specified_favored_favored_ties = 0;
                int last_season_favored_wins = 0;
                int last_season_favored_favored_wins = 0;
                int last_season_favored_losses = 0;
                int last_season_favored_favored_losses = 0;
                int last_season_favored_ties = 0;
                int last_season_favored_favored_ties = 0;
                int favored_current_win_streak = 0;
                int favored_longest_win_streak = 0;
                int opponent_current_win_streak = 0;
                int opponent_longest_win_streak = 0;

                double? favored_manager_greatest_mov = null;
                Matchup? favored_manager_greatest_mov_matchup = null;
                double? opponent_manager_greatest_mov = null;
                Matchup? opponent_manager_greatest_mov_matchup = null;

                int current_year = current_league_season.Season.Name;
                foreach (MatchupDetail favored in selected_matchups)
                {
                    MatchupDetail opp = favored.Matchup.MatchupDetails.Where(md => md.Id != favored.Id).First();
                    int matchup_season = context.Seasons.Find(favored.Matchup.LeagueSeason.SeasonId).Name;
                    favored_manager_total_points += favored.TeamPoints;
                    opponent_manager_total_points += opp.TeamPoints;
                    if (favored.TeamPoints > opp.TeamPoints)
                    {
                        favored_wins++;
                        favored_current_win_streak++;
                        if (favored_current_win_streak > favored_longest_win_streak)
                        {
                            favored_longest_win_streak = favored_current_win_streak;
                        }
                        opponent_current_win_streak = 0;
                        if (favored.TeamProjPoints > opp.TeamProjPoints)
                        {
                            favored_favored_wins++;
                        }
                        if (matchup_season > current_league_season.Season.Name - numericUpDownYearSpecifier.Value)
                        {
                            year_specified_favored_wins++;
                            if (favored.TeamProjPoints > opp.TeamProjPoints)
                            {
                                year_specified_favored_favored_wins++;
                            }
                        }
                        if (matchup_season > current_league_season.Season.Name - 2)
                        {
                            last_season_favored_wins++;
                            if (favored.TeamProjPoints > opp.TeamProjPoints)
                            {
                                last_season_favored_favored_wins++;
                            }
                        }
                        if (favored_manager_greatest_mov == null || (favored.TeamPoints - opp.TeamPoints >= favored_manager_greatest_mov))
                        {
                            favored_manager_greatest_mov = Math.Round(favored.TeamPoints - opp.TeamPoints, 2);
                            favored_manager_greatest_mov_matchup = favored.Matchup;
                        }
                        if (opponent_manager_greatest_mov == null || (opp.TeamPoints - favored.TeamPoints >= opponent_manager_greatest_mov))
                        {
                            opponent_manager_greatest_mov = Math.Round(opp.TeamPoints - favored.TeamPoints, 2);
                            opponent_manager_greatest_mov_matchup = favored.Matchup;
                        }
                    }
                    if (favored.TeamPoints < opp.TeamPoints)
                    {
                        favored_losses++;
                        opponent_current_win_streak++;
                        if (opponent_current_win_streak > opponent_longest_win_streak)
                        {
                            opponent_longest_win_streak = opponent_current_win_streak;
                        }
                        favored_current_win_streak = 0;
                        if (favored.TeamProjPoints > opp.TeamProjPoints)
                        {
                            favored_favored_losses++;
                        }
                        if (matchup_season > current_league_season.Season.Name - numericUpDownYearSpecifier.Value)
                        {
                            year_specified_favored_losses++;
                            if (favored.TeamProjPoints > opp.TeamProjPoints)
                            {
                                year_specified_favored_favored_losses++;
                            }
                        }
                        if (matchup_season > current_league_season.Season.Name - 2)
                        {
                            last_season_favored_losses++;
                            if (favored.TeamProjPoints > opp.TeamProjPoints)
                            {
                                last_season_favored_favored_losses++;
                            }
                        }
                        if (favored_manager_greatest_mov == null || (favored.TeamPoints - opp.TeamPoints >= favored_manager_greatest_mov))
                        {
                            favored_manager_greatest_mov = Math.Round(favored.TeamPoints - opp.TeamPoints, 2);
                            favored_manager_greatest_mov_matchup = favored.Matchup;
                        }
                        if (opponent_manager_greatest_mov == null || (opp.TeamPoints - favored.TeamPoints >= opponent_manager_greatest_mov))
                        {
                            opponent_manager_greatest_mov = Math.Round(opp.TeamPoints - favored.TeamPoints, 2);
                            opponent_manager_greatest_mov_matchup = favored.Matchup;
                        }
                    }
                    if (favored.TeamPoints == opp.TeamPoints)
                    {
                        favored_ties++;
                        favored_current_win_streak = 0;
                        opponent_current_win_streak = 0;
                        if (favored.TeamProjPoints > opp.TeamProjPoints)
                        {
                            favored_favored_ties++;
                        }
                        if (matchup_season > current_league_season.Season.Name - numericUpDownYearSpecifier.Value)
                        {
                            year_specified_favored_ties++;
                            if (favored.TeamProjPoints > opp.TeamProjPoints)
                            {
                                year_specified_favored_favored_ties++;
                            }
                        }
                        if (matchup_season > current_league_season.Season.Name - 2)
                        {
                            last_season_favored_ties++;
                            if (favored.TeamProjPoints > opp.TeamProjPoints)
                            {
                                last_season_favored_favored_ties++;
                            }
                        }
                    }
                }




                // Fill in Matchup Breakdown Values
                // CHECK FOR selected_matchups.Count == 0
                if (selected_matchups.Count != 0)
                {
                    textBoxFavoredManagerAverageScore.Text = (favored_manager_total_points / (double)selected_matchups.Count).ToString("F3");
                    textBoxOpponentManagerAverageScore.Text = (opponent_manager_total_points / (double)selected_matchups.Count).ToString("F3");

                    textBoxFavoredManagerGreatestMOV.Text = favored_manager_greatest_mov != null
                                                                ? favored_manager_greatest_mov.ToString() + " (" + favored_manager_greatest_mov_matchup.LeagueSeason.Season.Name + ", W" + favored_manager_greatest_mov_matchup.Week + ")"
                                                                : "-";
                    textBoxOpponentManagerGreatestMOV.Text = opponent_manager_greatest_mov != null
                                                                ? opponent_manager_greatest_mov.ToString() + " (" + opponent_manager_greatest_mov_matchup.LeagueSeason.Season.Name + ", W" + opponent_manager_greatest_mov_matchup.Week + ")"
                                                                : "-";

                    textBoxFavoredManagerLongestWinningStreak.Text = favored_current_win_streak != 0 ? favored_longest_win_streak + "(" + favored_current_win_streak + ")" : favored_longest_win_streak.ToString();
                    textBoxOpponentManagerLongestWinningStreak.Text = opponent_current_win_streak != 0 ? opponent_longest_win_streak + "(" + opponent_current_win_streak + ")" : opponent_longest_win_streak.ToString();

                    textBoxFavoredManagerPowerRankingScore.Text = favored_manager_power_ranking;
                    textBoxOpponentManagerPowerRankingScore.Text = opponent_manager_power_ranking;
                }
                else
                {
                    textBoxFavoredManagerAverageScore.Text = "-";
                    textBoxOpponentManagerAverageScore.Text = "-";
                    textBoxFavoredManagerGreatestMOV.Text = "-";
                    textBoxOpponentManagerGreatestMOV.Text = "-";
                    textBoxFavoredManagerPowerRankingScore.Text = "-";
                    textBoxOpponentManagerPowerRankingScore.Text = "-";
                    textBoxFavoredManagerLongestWinningStreak.Text = "-";
                    textBoxOpponentManagerLongestWinningStreak.Text = "-";
                }
                // All-Play Record
                textBoxFavoredManagerAllPlayRecord.Text = Calculations.CalculateAllPlayRecord(selectedLeague, selectedFavoredManager, context.LeagueSeasons.Find(current_league_season_id).Season.Name);
                textBoxOpponentManagerAllPlayRecord.Text = Calculations.CalculateAllPlayRecord(selectedLeague, selectedOpponentManager, context.LeagueSeasons.Find(current_league_season_id).Season.Name);
                // Switched Schedules Record
                textBoxFavoredManagerSwitchedSchedulesRecord.Text = Calculations.CalculateSwitchedRecords(selectedLeague, selectedFavoredManager, selectedOpponentManager, context.LeagueSeasons.Find(current_league_season_id).Season.Name).m1SwitchedRecord;
                textBoxOpponentManagerSwitchedSchedulesRecord.Text = Calculations.CalculateSwitchedRecords(selectedLeague, selectedFavoredManager, selectedOpponentManager, context.LeagueSeasons.Find(current_league_season_id).Season.Name).m2SwitchedRecord;

                // Fill in Matchup History Values
                // CHECK FOR selected_matchups.Count == 0
                // Also check count == 0 for each textBox
                if (selected_matchups.Count != 0)
                {
                    // Can skip the zero check on this one or else we would have not passed the if statement above
                    textBoxAllTimeMatchupRecord.Text = favored_ties == 0
                                                        ? favored_wins + "-" + favored_losses + " (" + ((double)favored_wins / (double)selected_matchups.Count()).ToString("F5") + ")"
                                                        : favored_wins + "-" + favored_losses + "-" + favored_ties + " (" + (((double)favored_wins + ((double)favored_ties * 0.5)) / (double)selected_matchups.Count()).ToString("F5") + ")";
                    textBoxAllTimeFavoredMatchupRecord.Text = favored_favored_wins + favored_favored_losses + favored_favored_ties > 0
                                                        ? (favored_favored_ties == 0
                                                                ? favored_favored_wins + "-" + favored_favored_losses + " (" + ((double)favored_favored_wins / (double)(favored_favored_wins + favored_favored_losses)).ToString("F5") + ")"
                                                                : favored_favored_wins + "-" + favored_favored_losses + "-" + favored_favored_ties + " (" + (((double)favored_favored_wins + ((double)favored_favored_ties * 0.5)) / (double)(favored_favored_wins + favored_favored_losses + favored_favored_ties)).ToString("F5") + ")")
                                                        : "0-0 (-)";

                    textBoxYearSpecifiedRangeMatchupRecord.Text = year_specified_favored_wins + year_specified_favored_losses + year_specified_favored_ties > 0
                                                        ? (year_specified_favored_ties == 0
                                                                ? year_specified_favored_wins + "-" + year_specified_favored_losses + " (" + ((double)year_specified_favored_wins / (double)(year_specified_favored_wins + year_specified_favored_losses)).ToString("F5") + ")"
                                                                : year_specified_favored_wins + "-" + year_specified_favored_losses + "-" + year_specified_favored_ties + " (" + (((double)year_specified_favored_wins + ((double)year_specified_favored_ties * 0.5)) / (double)(year_specified_favored_wins + year_specified_favored_losses + year_specified_favored_ties)).ToString("F5") + ")")
                                                        : "0-0 (-)";
                    textBoxYearSpecifiedRangeFavoredMatchupRecord.Text = year_specified_favored_favored_wins + year_specified_favored_favored_losses + year_specified_favored_favored_ties > 0
                                                        ? (year_specified_favored_favored_ties == 0
                                                                ? year_specified_favored_favored_wins + "-" + year_specified_favored_favored_losses + " (" + ((double)year_specified_favored_favored_wins / (double)(year_specified_favored_favored_wins + year_specified_favored_favored_losses)).ToString("F5") + ")"
                                                                : year_specified_favored_favored_wins + "-" + year_specified_favored_favored_losses + "-" + year_specified_favored_favored_ties + " (" + (((double)year_specified_favored_favored_wins + ((double)year_specified_favored_favored_ties * 0.5)) / (double)(year_specified_favored_favored_wins + year_specified_favored_favored_losses + year_specified_favored_favored_ties)).ToString("F5") + ")")
                                                        : "0-0 (-)";

                    textBoxLastSeasonMatchupRecord.Text = last_season_favored_wins + last_season_favored_losses + last_season_favored_ties > 0
                                                        ? (last_season_favored_ties == 0
                                                                ? last_season_favored_wins + "-" + last_season_favored_losses + " (" + ((double)last_season_favored_wins / (double)(last_season_favored_wins + last_season_favored_losses)).ToString("F5") + ")"
                                                                : last_season_favored_wins + "-" + last_season_favored_losses + "-" + last_season_favored_ties + " (" + (((double)last_season_favored_wins + ((double)last_season_favored_ties * 0.5)) / (double)(last_season_favored_wins + last_season_favored_losses + last_season_favored_ties)).ToString("F5") + ")")
                                                        : "0-0 (-)";
                    textBoxLastSeasonFavoredMatchupRecord.Text = last_season_favored_favored_wins + last_season_favored_favored_losses + last_season_favored_favored_ties > 0
                                                        ? (last_season_favored_favored_ties == 0
                                                                ? last_season_favored_favored_wins + "-" + last_season_favored_favored_losses + " (" + ((double)last_season_favored_favored_wins / (double)(last_season_favored_favored_wins + last_season_favored_favored_losses)).ToString("F5") + ")"
                                                                : last_season_favored_favored_wins + "-" + last_season_favored_favored_losses + "-" + last_season_favored_favored_ties + " (" + (((double)last_season_favored_favored_wins + ((double)last_season_favored_favored_ties * 0.5)) / (double)(last_season_favored_favored_wins + last_season_favored_favored_losses + last_season_favored_favored_ties)).ToString("F5") + ")")
                                                        : "0-0 (-)";
                }
                else
                {
                    textBoxAllTimeMatchupRecord.Text = "0-0 (-)";
                    textBoxAllTimeFavoredMatchupRecord.Text = "0-0 (-)";
                    textBoxYearSpecifiedRangeMatchupRecord.Text = "0-0 (-)";
                    textBoxYearSpecifiedRangeFavoredMatchupRecord.Text = "0-0 (-)";
                    textBoxLastSeasonMatchupRecord.Text = "0-0 (-)";
                    textBoxLastSeasonFavoredMatchupRecord.Text = "0-0 (-)";
                }


                // Fill in Manager V Manager Comparison Values
                textBoxFavoredManagerAllTimeRegularSeasonRecord.Text = favored_manager_all_time_regular_season_record;
                textBoxFavoredManagerChampionships.Text = favored_manager_championships.ToString();
                textBoxFavoredManagerAvgDivisionFinish.Text = favored_manager_avg_division_finish == "-" ? favored_manager_avg_division_finish : Math.Round(double.Parse(favored_manager_avg_division_finish), 3).ToString("F3");
                textBoxFavoredManagerAvgPlayoffSeed.Text = favored_manager_avg_playoff_seed == "-" ? favored_manager_avg_playoff_seed : (favored_manager_avg_playoff_seed == "-1" ? "-" : Math.Round(double.Parse(favored_manager_avg_playoff_seed), 3).ToString("F3"));
                textBoxFavoredManagerAvgFinish.Text = favored_manager_avg_finish == "-" ? favored_manager_avg_finish : Math.Round(double.Parse(favored_manager_avg_finish), 3).ToString("F3");

                textBoxOpponentManagerAllTimeRegularSeasonRecord.Text = opponent_manager_all_time_regular_season_record;
                textBoxOpponentManagerChampionships.Text = opponent_manager_championships.ToString();
                textBoxOpponentManagerAvgDivisionFinish.Text = opponent_manager_avg_division_finish == "-" ? opponent_manager_avg_division_finish : Math.Round(double.Parse(opponent_manager_avg_division_finish), 3).ToString("F3");
                textBoxOpponentManagerAvgPlayoffSeed.Text = opponent_manager_avg_playoff_seed == "-" ? opponent_manager_avg_playoff_seed : (opponent_manager_avg_playoff_seed == "-1" ? "-" : Math.Round(double.Parse(opponent_manager_avg_playoff_seed), 3).ToString("F3"));
                textBoxOpponentManagerAvgFinish.Text = opponent_manager_avg_finish == "-" ? opponent_manager_avg_finish : Math.Round(double.Parse(opponent_manager_avg_finish), 3).ToString("F3");

                tableLayoutPanelManagerVsManagerComparisonResults.Visible = true;
            }
        }

        private void comboBoxFavoredManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxFavoredManager.SelectedValue != null)
            {
                selectedFavoredManager = (int)comboBoxFavoredManager.SelectedValue;
            }
        }

        private void comboBoxOpponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get selected item's value
            if (comboBoxOpponent.SelectedValue != null)
            {
                selectedOpponentManager = (int)comboBoxOpponent.SelectedValue;
            }
        }

    }
}
