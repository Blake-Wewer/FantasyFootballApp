﻿namespace FantasyFootballApp
{
    internal class ErrorMessages
    {
        public static void LeagueRequiredMessageBox()
        {
            MessageBox.Show("A league must be selected to run this query.\nPlease select a league from the dropdown box.", "ERROR!");
        }
        public static void LeagueRequiredFromCheckOrListMessageBox()
        {
            MessageBox.Show("A league must be selected to run this query.\nPlease select a league from the list or check the \'Use All Leagues\' option.", "ERROR!");
        }

        public static void ManagerRequiredMessageBox()
        {
            MessageBox.Show("A Manager must be selected to run this query.\nPlease select a manager from the dropdown box.", "ERROR!");
        }

        public static void FavoredManagerRequiredMessageBox()
        {
            MessageBox.Show("A Favored Manager must be selected to run this query.\nPlease select a favored manager from the dropdown box.", "ERROR!");
        }

        public static void OpponentManagerRequiredMessageBox()
        {
            MessageBox.Show("An Opponent must be selected to run this query.\nPlease select an opponent from the dropdown box.", "ERROR!");
        }

        public static void UniqueManagersRequiredMessageBox()
        {
            MessageBox.Show("The selected managers must be unique to run this query.\nPlease select two unique managers from the dropdown box.", "ERROR!");
        }

        public static void ReportCouldNotBeRanMessageBox()
        {
            MessageBox.Show("Report could not be ran.", "ERROR!");
        }

        public static void PlayerKeeperEligibilityExhaustedMessageBox()
        {
            MessageBox.Show("Manager has exhausted this player's keeper eligibility.", "NOTICE!");
        }
    }
}
