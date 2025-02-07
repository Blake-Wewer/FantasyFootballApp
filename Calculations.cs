using FantasyFootballApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballApp
{
    public static class Calculations
    {
        /// <summary>
        /// Method that calculates league power rankings for a given season and returns a dictionary of manager_ids and power ranking score
        /// </summary>
        /// <param name="selectedLeague"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static Dictionary<int, double> CalculateLeaguePowerRankings(int selectedLeague, int? year = null)
        {
            using (AppDbContext context = new AppDbContext())
            {
                // Weekly Median Score (Used to calculate the power rankings)
                var current_league_season_query = context.LeagueSeasons.AsQueryable();
                if (year != null)
                {
                    current_league_season_query = current_league_season_query.Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague && ls.Season.Name == year);
                }
                else
                {
                    current_league_season_query = current_league_season_query.Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague)
                                .OrderByDescending(ls => ls.Season.Name);
                }
                int current_league_season_id = current_league_season_query.Select(ls => ls.Id).FirstOrDefault();
                if (current_league_season_id == 0) return new Dictionary<int, double>();

                List<int> all_league_season_matchup_ids = context.Matchups
                                        .Where(mat => mat.LeagueSeasonId == current_league_season_id
                                                && mat.PlayoffMatchup == 0)
                                        .Select(mat => mat.Id)
                                        .ToList();
                if (context.Matchups.Where(mat => all_league_season_matchup_ids.Contains(mat.Id) && mat.MatchupDetails.Count > 1).Count() == 0)
                {
                    return new Dictionary<int, double>();
                }
                List<MatchupDetail> all_league_season_matchup_details = context.MatchupDetails
                                                    .Where(md => all_league_season_matchup_ids.Contains(md.MatchupId))
                                                    .Include(md => md.Matchup)
                                                    .Include(md => md.Team)
                                                    .OrderBy(md => md.Matchup.Week)
                                                    .ThenBy(md => md.TeamPoints)
                                                    .ToList();
                Dictionary<int, double> weekly_median_scores = new Dictionary<int, double>();
                for (int i = all_league_season_matchup_details.First().Matchup.Week; i <= all_league_season_matchup_details.Last().Matchup.Week; i++)
                {
                    List<MatchupDetail> week_matchups = all_league_season_matchup_details.Where(md => md.Matchup.Week == i).ToList();
                    weekly_median_scores[i] = GetMedian(week_matchups.Select(md => md.TeamPoints).ToList());
                }


                // Calculate the league's manager's power rankings: (Points Scored x2) + (Points Scored * Winning %) + (Points Scored * Winning % if played vs the median score of the week)
                // manager_id, power_ranking_score
                Dictionary<int, double> power_rankings = new Dictionary<int, double>();
                // Grab all manager ids associated with this league season
                // Query to get all ManagerId values associated with the LeagueSeason
                List<int> managerIds = context.Teams
                    .Where(t => t.LeagueSeasonId == current_league_season_id)
                    .Select(t => t.ManagerId)
                    .Distinct()
                    .ToList();
                foreach (int manager_id in managerIds)
                {
                    List<MatchupDetail> manager_details = all_league_season_matchup_details.Where(md => md.Team.ManagerId == manager_id).ToList();
                    double points_scored = manager_details.Sum(md => md.TeamPoints);
                    int wins = 0;
                    int losses = 0;
                    int ties = 0;
                    int median_wins = 0;
                    int median_losses = 0;
                    int median_ties = 0;
                    foreach (KeyValuePair<int, double> median in weekly_median_scores)
                    {
                        MatchupDetail manager_matchup_details = all_league_season_matchup_details.Where(md => md.Matchup.Week == median.Key && md.Team.ManagerId == manager_id).First();
                        if (manager_matchup_details.TeamPoints > manager_matchup_details.Matchup.MatchupDetails.Where(md => md.Id != manager_matchup_details.Id).First().TeamPoints) { wins++; }
                        else if (manager_matchup_details.TeamPoints < manager_matchup_details.Matchup.MatchupDetails.Where(md => md.Id != manager_matchup_details.Id).First().TeamPoints) { losses++; }
                        else { ties++; }
                        if (manager_matchup_details.TeamPoints > median.Value) { median_wins++; }
                        else if (manager_matchup_details.TeamPoints < median.Value) { median_losses++; }
                        else { median_ties++; }
                    }
                    power_rankings.Add(manager_id, (points_scored * 2) + (points_scored * ((wins + (0.5 * ties)) / (wins + losses + ties))) + (points_scored * ((median_wins + (0.5 * median_ties)) / (median_wins + median_losses + median_ties))));
                }

                return power_rankings.OrderByDescending(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
            }
        }

        /// <summary>
        /// Calculates the all-play record for a manager in either the most recent season or a specified season.
        /// </summary>
        /// <param name="selectedLeague"></param>
        /// <param name="manager_id"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static string CalculateAllPlayRecord(int selectedLeague, int manager_id, int? year = null)
        {
            string result = "0-0 (-)";
            using (AppDbContext context = new AppDbContext())
            {
                var current_league_season_query = context.LeagueSeasons.AsQueryable();
                if (year != null)
                {
                    current_league_season_query = current_league_season_query.Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague && ls.Season.Name == year);
                }
                else
                {
                    current_league_season_query = current_league_season_query.Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague)
                                .OrderByDescending(ls => ls.Season.Name);
                }
                int current_league_season_id = current_league_season_query.Select(ls => ls.Id).First();
                List<int> all_league_season_matchup_ids = context.Matchups
                                        .Where(mat => mat.LeagueSeasonId == current_league_season_id)
                                        .Select(mat => mat.Id)
                                        .ToList();
                int target_team_id = context.Teams.Where(t => t.ManagerId == manager_id && t.LeagueSeasonId == current_league_season_id)
                                .Select(t => t.Id)
                                .FirstOrDefault();

                if (target_team_id == 0 || all_league_season_matchup_ids.Count == 0) return result;

                List<MatchupDetail> matchup_details = context.MatchupDetails
                                                    .Where(md => all_league_season_matchup_ids.Contains(md.MatchupId) && md.TeamId == target_team_id && md.Matchup.ByeWeek == 0)
                                                    .Include(md => md.Matchup)
                                                    .OrderBy(md => md.Matchup.Week)
                                                    .ToList();
                List<MatchupDetail> opp_matchup_details = context.MatchupDetails
                                                    .Where(md => all_league_season_matchup_ids.Contains(md.MatchupId) && md.TeamId != target_team_id)
                                                    .Include(md => md.Matchup)
                                                    .OrderBy(md => md.Matchup.Week)
                                                    .ToList();

                if (matchup_details.Count == 0 || opp_matchup_details.Count == 0) return result;

                int all_play_wins = 0;
                int all_play_losses = 0;
                int all_play_ties = 0;
                foreach (MatchupDetail md in matchup_details)
                {
                    foreach (MatchupDetail opp_md in opp_matchup_details.Where(omd => omd.Matchup.Week == md.Matchup.Week).ToList())
                    {
                        if (md.TeamPoints > opp_md.TeamPoints) all_play_wins++;
                        else if (md.TeamPoints < opp_md.TeamPoints) all_play_losses++;
                        else if (md.TeamPoints == opp_md.TeamPoints) all_play_ties++;
                    }
                }
                return all_play_ties == 0
                            ? all_play_wins + "-" + all_play_losses + " (" + Math.Round(((double)all_play_wins / (double)(all_play_wins + all_play_losses)), 5).ToString("F5") + ")"
                            : all_play_wins + "-" + all_play_losses + "-" + all_play_ties + " (" + Math.Round((((double)all_play_wins + (0.5 * (double)all_play_ties)) / (double)(all_play_wins + all_play_losses)), 5).ToString("F5") + ")";
            }
        }

        /// <summary>
        /// Calculates the record for two managers if their schedules were switched either in the most recent season or the provided year.
        /// </summary>
        /// <param name="selectedLeague"></param>
        /// <param name="m1_id"></param>
        /// <param name="m2_id"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static (string m1SwitchedRecord, string m2SwitchedRecord) CalculateSwitchedRecords(int selectedLeague, int m1_id, int m2_id, int? year = null)
        {
            (string, string) result = ("0-0 (-)", "0-0 (-)");
            using (AppDbContext context = new AppDbContext())
            {
                var current_league_season_query = context.LeagueSeasons.AsQueryable();
                if (year != null)
                {
                    current_league_season_query = current_league_season_query.Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague && ls.Season.Name == year);
                }
                else
                {
                    current_league_season_query = current_league_season_query.Include(ls => ls.Season)
                                .Where(ls => ls.LeagueId == selectedLeague)
                                .OrderByDescending(ls => ls.Season.Name);
                }
                int current_league_season_id = current_league_season_query.Select(ls => ls.Id).First();
                List<int> all_league_season_matchup_ids = context.Matchups
                                        .Where(mat => mat.LeagueSeasonId == current_league_season_id)
                                        .Select(mat => mat.Id)
                                        .ToList();
                int m1_team_id = context.Teams.Where(t => t.ManagerId == m1_id && t.LeagueSeasonId == current_league_season_id)
                                .Select(t => t.Id)
                                .FirstOrDefault();
                int m2_team_id = context.Teams.Where(t => t.ManagerId == m2_id && t.LeagueSeasonId == current_league_season_id)
                                .Select(t => t.Id)
                                .FirstOrDefault();
                if (m1_team_id == 0 || m2_team_id == 0 || all_league_season_matchup_ids.Count == 0) return result;

                List<MatchupDetail> m1_matchup_details = context.MatchupDetails
                                                    .Where(md => all_league_season_matchup_ids.Contains(md.MatchupId) && md.TeamId == m1_team_id && md.Matchup.PlayoffMatchup == 0)
                                                    .Include(md => md.Matchup)
                                                        .ThenInclude(m => m.MatchupDetails)
                                                    .OrderBy(md => md.Matchup.Week)
                                                    .ToList();
                List<MatchupDetail> m2_matchup_details = context.MatchupDetails
                                                    .Where(md => all_league_season_matchup_ids.Contains(md.MatchupId) && md.TeamId == m2_team_id && md.Matchup.PlayoffMatchup == 0)
                                                    .Include(md => md.Matchup)
                                                        .ThenInclude(m => m.MatchupDetails)
                                                    .OrderBy(md => md.Matchup.Week)
                                                    .ToList();
                if (m1_matchup_details.Count == 0 || m2_matchup_details.Count == 0) return result;

                int m1_wins = 0;
                int m1_losses = 0;
                int m1_ties = 0;
                int m2_wins = 0;
                int m2_losses = 0;
                int m2_ties = 0;
                foreach (MatchupDetail m1_md in m1_matchup_details)
                {
                    var m1_opp_md = m1_md.Matchup.MatchupDetails.Where(md => md.Id != m1_md.Id).First();
                    if (m1_opp_md.TeamId == m2_team_id)
                    {
                        if (m1_md.TeamPoints > m1_opp_md.TeamPoints)
                        {
                            m1_wins++;
                            m2_losses++;
                        }
                        else if (m1_md.TeamPoints < m1_opp_md.TeamPoints)
                        {
                            m1_losses++;
                            m2_wins++;
                        }
                        else if (m1_md.TeamPoints == m1_opp_md.TeamPoints)
                        {
                            m1_ties++;
                            m2_ties++;
                        }
                    }
                    else
                    {
                        MatchupDetail m2_md = m2_matchup_details.Where(opp_md => opp_md.Matchup.Week == m1_md.Matchup.Week).First();
                        MatchupDetail m2_opp_md = m2_md.Matchup.MatchupDetails.Where(opp_md => opp_md.Id != m2_md.Id).First();
                        if (m1_md.TeamPoints > m2_opp_md.TeamPoints) m1_wins++;
                        else if (m1_md.TeamPoints < m2_opp_md.TeamPoints) m1_losses++;
                        else if (m1_md.TeamPoints == m2_opp_md.TeamPoints) m1_ties++;
                        if (m2_md.TeamPoints > m1_opp_md.TeamPoints) m2_wins++;
                        else if (m2_md.TeamPoints < m1_opp_md.TeamPoints) m2_losses++;
                        else if (m2_md.TeamPoints == m1_opp_md.TeamPoints) m2_ties++;
                    }
                }
                string m1_switched_schedule = m1_ties == 0
                            ? m1_wins + "-" + m1_losses + " (" + Math.Round(((double)m1_wins / (double)(m1_wins + m1_losses)), 5).ToString("F5") + ")"
                            : m1_wins + "-" + m1_losses + "-" + m1_ties + " (" + Math.Round((((double)m1_wins + (0.5 * (double)m1_ties)) / (double)(m1_wins + m1_losses + m1_ties)), 5).ToString("F5") + ")";
                string m2_switched_schedule = m2_ties == 0
                            ? m2_wins + "-" + m2_losses + " (" + Math.Round(((double)m2_wins / (double)(m2_wins + m2_losses)), 5).ToString("F5") + ")"
                            : m2_wins + "-" + m2_losses + "-" + m2_ties + " (" + Math.Round((((double)m2_wins + (0.5 * (double)m2_ties)) / (double)(m2_wins + m2_losses + m2_ties)), 5).ToString("F5") + ")";
                return (m1_switched_schedule, m2_switched_schedule);
            }
        }

        public static double GetMedian(List<double> values)
        {
            if (values == null || values.Count == 0)
                throw new InvalidOperationException("Cannot compute median of an empty list.");

            // Sort the list
            values.Sort();

            int count = values.Count;
            if (count % 2 == 0)
            {
                // Even count: Average of the two middle elements
                double middle1 = values[count / 2 - 1];
                double middle2 = values[count / 2];
                return (middle1 + middle2) / 2.0;
            }
            else
            {
                // Odd count: Return the middle element
                return values[count / 2];
            }
        }
    }
}
