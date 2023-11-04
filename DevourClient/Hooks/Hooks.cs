using HarmonyLib;

namespace DevourClient.Hooks
{
    public class Hooks
    {
	[HarmonyPatch(typeof(Il2Cpp.NolanBehaviour), "OnAttributeUpdateValue")]
        static class NolanBehaviour_UV
        {
            [HarmonyPrefix]
            static void Prefix(ref Il2CppOpsive.UltimateCharacterController.Traits.Attribute attribute)
            {
                if (ClientMain.unlimitedUV && attribute.m_Name == "Battery")
                {
                    attribute.m_Value = 100.0f;
                    return;
                }
            }
        }    
	      
        [HarmonyPatch(typeof(Il2Cpp.RankHelpers))]
        [HarmonyPatch(nameof(Il2Cpp.RankHelpers.CalculateExpGain))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class RankHelpers_CalculateExpGain
        {
            static void Postfix(ref Il2Cpp.RankHelpers.ExpGainInfo __result)
            {
                if (ClientMain.exp_modifier)
                {
                    __result.totalExp = (int)ClientMain.exp;
                }
                return;
            }
        }
        

        [HarmonyPatch(typeof(Il2CppHorror.Menu))]
        [HarmonyPatch(nameof(Il2CppHorror.Menu.SetupPerk))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class Horror_Menu_SetupPerk_Patch
        {
            static void Prefix(ref Il2Cpp.CharacterPerk perk)
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

        [HarmonyPatch(typeof(Il2CppHorror.Menu))]
        [HarmonyPatch(nameof(Il2CppHorror.Menu.SetupOutfit))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class Horror_Menu_SetupOutfit_Patch
        {
            static void Prefix(ref Il2Cpp.CharacterOutfit outfit)
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
                outfit.isOwned = true;
                outfit.isHidden = false;
                return;
            }
        }

        [HarmonyPatch(typeof(Il2Cpp.OptionsHelpers))]
        [HarmonyPatch(nameof(Il2Cpp.OptionsHelpers.IsRobeUnlocked))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class OptionsHelpers_IsRobeUnlocked_Patch
        {
            static bool Prefix(ref string robe)
            {
                //MelonLoader.MelonLogger.Msg("robe : " + robe);

                robe = "Default";
                return true;
            }
        }

        [HarmonyPatch(typeof(Il2CppHorror.Menu))]
        [HarmonyPatch(nameof(Il2CppHorror.Menu.SetupFlashlight))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class Horror_Menu_SetLocked_Patch
        {
            static void Prefix(Il2Cpp.CharacterFlashlight flashlight)
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
