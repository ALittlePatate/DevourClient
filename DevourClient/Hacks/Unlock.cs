namespace DevourClient.Hacks
{
    public class Unlock
    {
        public static void Achievements()
        {
            Il2Cpp.AchievementHelpers ah = UnityEngine.Object.FindObjectOfType<Il2Cpp.AchievementHelpers>();

            string[] achievements = { "STAT_NUM_BLEACH_USED", "ACH_WON_TOWN_COOP", "ACH_WON_TOWN_HARD", "ACH_WON_SLAUGHTERHOUSE_SP", "ACH_WON_SP", "ACH_WON_SLAUGHTERHOUSE_NIGHTMARE", "ACH_WON_SLAUGHTERHOUSE_NIGHTMARE_SP", "ACH_WON_SLAUGHTERHOUSE_HARD", "ACH_WON_SLAUGHTERHOUSE_HARD_SP", "ACH_WON_NO_MEDKITS", "ACH_WON_SLAUGHTERHOUSE_COOP", "ACH_WON_NO_BATTERIES", "ACH_WON_NO_KNOCKOUT_COOP", "ACH_WON_NIGHTMARE_NO_BATTERIES", "ACH_WON_NIGHTMARE_NO_MEDKITS", "ACH_WON_MOLLY_NIGHTMARE_SP", "ACH_WON_MOLLY_SP", "ACH_WON_TOWN_NIGHTMARE_SP", "ACH_WON_TOWN_SP", "ACH_WON_TOWN_HARD_SP", "ACH_WON_TOWN_NIGHTMARE", "ACH_UNLOCKED_CAGE", "ACH_WIN_NIGHTMARE", "ACH_SURVIVED_TO_7_GOATS", "ACH_UNLOCKED_ATTIC_CAGE", "ACH_SURVIVED_TO_3_GOATS", "ACH_SURVIVED_TO_5_GOATS", "ACH_STAGGERED_SAM_20_TIMES", "ACH_STAGGERED_ZARA_20_TIMES", "ACH_STAGGERED_MOLLY_20_TIMES", "ACH_STAGGERED_NATHAN_20_TIMES", "ACH_REVIVED_20_PLAYERS", "ACH_STAGGERED_ANNA_20_TIMES", "ACH_NEVER_KNOCKED_OUT", "ACH_ONLY_ONE_KNOCKED_OUT", "ACH_LOST", "ACH_LURED_20_GOATS", "ACH_WON_MOLLY_HARD_SP", "ACH_WON_MOLLY_NIGHTMARE", "ACH_WON_MOLLY_COOP", "ACH_WON_MOLLY_HARD", "ACH_WON_INN_NIGHTMARE_SP", "ACH_WON_INN_SP", "ACH_WON_INN_HARD_SP", "ACH_WON_INN_NIGHTMARE", "ACH_WON_INN_COOP", "ACH_WON_INN_HARD", "ACH_WON_HARD_NO_MEDKITS", "ACH_WON_HARD_SP", "ACH_WON_HARD", "ACH_WON_HARD_NO_BATTERIES", "ACH_WIN_NIGHTMARE_SP", "ACH_WON_COOP", "ACH_ALL_ROSES", "ACH_BEAT_GAME_5_TIMES", "ACH_ALL_NOTES_READ", "ACH_ALL_PATCHES", "ACH_ALL_CLIPBOARDS_READ", "ACH_ALL_HORSESHOES", "ACH_ALL_BARBED_WIRES", "ACH_ALL_CHERRY_BLOSSOM", "ACH_20_POOPS_SEARCHED", "ACH_20_TRASH_CANS_KICKED", "ACH_100_GASOLINE_USED", "ACH_20_BLEACH_USED", "ACH_100_EGGS_DESTROYED", "ACH_100_FUSES_USED", "ACH_1000_PIGS_DESTROYED", "ACH_100_BOOKS_CURSED", "ACH_KNOCKED_OUT_IN_HIDING", "ACH_KNOCKOUT_OUT_BY_DEMON", "ACH_KNOCKED_OUT_20_TIMES", "ACH_KNOCKED_OUT_BY_ANNA", "ACH_FRIED_20_DEMONS", "ACH_FRIED_RAT", "ACH_FRIED_100_CORPSES", "ACH_FRIED_100_INMATES", "ACH_FRIED_1000_GHOSTS", "ACH_FRIED_1000_SPIDERS", "ACH_CALM_MOLLY_10_TIMES", "ACH_FRIED_1000_BOARS", "ACH_CALMED_ANNA", "ACH_CALMED_ANNA_10_TIMES", "ACH_BEAT_GAME_5_TIMES_IN_NIGHTMARE_MODE", "ACH_BURNT_GOAT", "ACH_1000_BOOKS_DESTROYED" };

            for (int i = 0; i < achievements.Length; i++)
            {
                ah.Unlock(achievements[i]);
            }
        }

        public static void Doors()
        {
            //Pour chaques portes, on les ouvre
            foreach (Il2CppHorror.DoorBehaviour doorBehaviour in UnityEngine.Object.FindObjectsOfType<Il2CppHorror.DoorBehaviour>())
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
