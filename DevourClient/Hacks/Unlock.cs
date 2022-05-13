namespace DevourClient.Hacks
{
    public class Unlock
    {
        public static void Achievements()
        {
            AchievementHelpers ah = UnityEngine.Object.FindObjectOfType<AchievementHelpers>();

			/*
		    string[] names = { "hasAchievedFusesUsed", "hasAchievedGasolineUsed", "hasAchievedNoKnockout", "hasCollectedAllPatches", "hasCollectedAllRoses",
                    "hasCompletedHardAsylumGame", "hasCompletedHardGame", "hasCompletedNightmareAsylumGame", "hasCompletedNightmareGame", "hasCompletedNormalGame",
                    "hasCompletedHardInnGamehasCompletedNightmareInnGame", "hasCollectedAllCherryBlossom", "hasAchievedEggsDestroyed", "hasCollectedAllPumpkins",
                    "isStatsValid", "isStatsFetched" };

            for (int i = 0; i < names.Length; i++)
            {
                ah.GetType().GetField(names[i], System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(ah, true); //Causing a crash.
            }
            */


			string[] achievments = { "ACH_WON_INN_NIGHTMARE", "ACH_WON_INN_NIGHTMARE_SP", "ACH_WON_INN_HARD", "ACH_WON_INN_HARD_SP", "ACH_WON_INN_COOP", "ACH_ALL_ROSES", "ACH_BURNT_GOAT",
                "ACH_ALL_CHERRY_BLOSSOM", "ACH_100_EGGS_DESTROYED", "ACH_WON_INN_SP", "ACH_SURVIVED_TO_3_GOATS", "ACH_SURVIVED_TO_5_GOATS", "ACH_SURVIVED_TO_7_GOATS", "ACH_WON_SP", "ACH_WON_COOP",
                "ACH_LOST", "ACH_LURED_20_GOATS", "ACH_REVIVED_20_PLAYERS", "ACH_ALL_NOTES_READ", "ACH_KNOCKED_OUT_BY_ANNA", "ACH_KNOCKOUT_OUT_BY_DEMON", "ACH_KNOCKED_OUT_20_TIMES",
                "ACH_NEVER_KNOCKED_OUT", "ACH_ONLY_ONE_KNOCKED_OUT", "ACH_UNLOCKED_CAGE", "ACH_UNLOCKED_ATTIC_CAGE", "ACH_BEAT_GAME_5_TIMES", "ACH_100_GASOLINE_USED",
                "ACH_FRIED_20_DEMONS", "ACH_STAGGERED_ANNA_20_TIMES", "ACH_CALMED_ANNA_10_TIMES", "ACH_CALMED_ANNA", "ACH_WIN_NIGHTMARE", "ACH_BEAT_GAME_5_TIMES_IN_NIGHTMARE_MODE",
                "ACH_WON_NO_KNOCKOUT_COOP", "ACH_WIN_NIGHTMARE_SP", "ACH_WON_HARD", "ACH_WON_HARD_SP", "ACH_100_FUSES_USED", "ACH_ALL_CLIPBOARDS_READ", "ACH_ALL_PATCHES",
                "ACH_FRIED_RAT", "ACH_FRIED_100_INMATES", "ACH_LURED_20_RATS", "ACH_STAGGERED_MOLLY_20_TIMES", "ACH_WON_MOLLY_SP", "ACH_WON_MOLLY_HARD_SP", "ACH_WON_MOLLY_NIGHTMARE_SP",
                "ACH_WON_MOLLY_COOP", "ACH_WON_MOLLY_HARD", "ACH_WON_MOLLY_NIGHTMARE", "ACH_20_TRASH_CANS_KICKED", "ACH_CALM_MOLLY_10_TIMES" };
            
            for (int i = 0; i < achievments.Length; i++)
            {
                ah.Unlock(achievments[i]);
            }
        }

        public static void Doors()
        {
            //Pour chaques portes, on les ouvre
            foreach (Horror.DoorBehaviour doorBehaviour in UnityEngine.Object.FindObjectsOfType<Horror.DoorBehaviour>())
            {
                doorBehaviour.state.Locked = false;
                if (doorBehaviour.IsOpen())
                {
                    doorBehaviour.m_DoorGraphUpdate.DoorOpening();
                }
                else
                {
                    doorBehaviour.m_DoorGraphUpdate.DoorClosed();
                }
                doorBehaviour.Unlock();
            }
        }
    }
}
