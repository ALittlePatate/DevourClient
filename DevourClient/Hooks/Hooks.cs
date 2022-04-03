using HarmonyLib;
using DevourClient;

namespace DevourClient.Hooks
{
    public class Hooks
    {
        [HarmonyPatch(typeof(NolanBehaviour))]
        [HarmonyPatch(nameof(NolanBehaviour.IsCarryingFirstAidOnLocalCharacter))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class NolanBehaviour_IsCarryingFirstAidOnLocalCharacter_Patch
        {
            static void Postfix(ref bool __result)
            {
                Load settings = new Load();
                if (settings._IsCarryingFirstAidOnLocalCharacter)
                {
                    __result = true;
                }
                
                return;
            }
        }

        [HarmonyPatch(typeof(Horror.Menu))]
        [HarmonyPatch(nameof(Horror.Menu.SetupPerk))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class Horror_Menu_SetupPerk_Patch
        {
            static void Prefix(ref CharacterPerk perk)
            {
                /*
                    public int cost { get; set; }
                    public bool isOwned { get; set; }
                    public bool isHidden { get; set; }
                */

                //MelonLoader.MelonLogger.Msg("cost : " + perk.cost);
                //MelonLoader.MelonLogger.Msg("isOwned : " + perk.isOwned);
                //MelonLoader.MelonLogger.Msg("isHidden : " + perk.isHidden);
                perk.cost = 0;
                perk.isOwned = true;
                perk.isHidden = false;
                return;
            }
        }

        [HarmonyPatch(typeof(Horror.Menu))]
        [HarmonyPatch(nameof(Horror.Menu.SetupOutfit))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class Horror_Menu_SetupOutfit_Patch
        {
            static void Prefix(ref CharacterOutfit outfit)
            {
                /*
                    public ulong currentPrice;
	                public ulong basePrice;
	                public bool isOwned;
	                public bool isHidden;
                */

                //MelonLoader.MelonLogger.Msg("basePrice : " + outfit.basePrice);
                //MelonLoader.MelonLogger.Msg("currentPrice : " + outfit.currentPrice);
                //MelonLoader.MelonLogger.Msg("isOwned : " + outfit.isOwned);
                //MelonLoader.MelonLogger.Msg("isHidden : " + outfit.isHidden);
                outfit.basePrice = 0;
                outfit.currentPrice = 0;
                outfit.isOwned = true;
                outfit.isHidden = false;
                return;
            }
        }

        [HarmonyPatch(typeof(OptionsHelpers))]
        [HarmonyPatch(nameof(OptionsHelpers.IsRobeUnlocked))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class OptionsHelpers_IsRobeUnlocked_Patch
        {
            static bool Prefix(ref string robe)
            {
                //MelonLoader.MelonLogger.Msg("robe : " + robe);

                robe = "Default";
                return true;
            }
        }

        [HarmonyPatch(typeof(Horror.Menu))]
        [HarmonyPatch(nameof(Horror.Menu.SetupFlashlight))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class Horror_Menu_SetLocked_Patch
        {
            static void Prefix(CharacterFlashlight flashlight)
            {
                /*
                public bool isHidden { get; set; }
                public int cost { get; set; }
                public bool requiresPurchase { get; set; }
                public bool isOwned { get; set; }
                */

                //MelonLoader.MelonLogger.Msg("isHidden : " + flashlight.isHidden);
                //MelonLoader.MelonLogger.Msg("cost : " + flashlight.cost);
                //MelonLoader.MelonLogger.Msg("requiresPurchase : " + flashlight.requiresPurchase);
                //MelonLoader.MelonLogger.Msg("isOwned : " + flashlight.isOwned);

                flashlight.isHidden = false;
                flashlight.cost = 0;
                flashlight.requiresPurchase = false;
                flashlight.isOwned = true;
                return;
            }
        }
    }
}
