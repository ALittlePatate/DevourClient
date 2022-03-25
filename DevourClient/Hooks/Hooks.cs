using HarmonyLib;

namespace DevourClient.Hooks
{
    public class Hooks
    {
        /*
        [HarmonyPatch(typeof(UIPerkSelectionType))]
        [HarmonyPatch(nameof(UIPerkSelectionType.SetLocked))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class UIPerkSelectionType_SetLocked_Patch
        {
            static void Prefix(ref bool locked, ref int cost)
            {
                MelonLoader.MelonLogger.Msg("cost : "+cost);
                MelonLoader.MelonLogger.Msg("locked : " + locked);

                locked = false;
                cost = 0;
                return;
            }
        }
        */

        [HarmonyPatch(typeof(NolanBehaviour))]
        [HarmonyPatch(nameof(NolanBehaviour.SetIsBeingKnockedOut))] //annotation boiler plate to tell Harmony what to patch. Refer to docs.
        static class NolanBehaviour_SetIsBeingKnockedOut_Patch
        {
            static void Prefix(ref bool enable)
            {
                MelonLoader.MelonLogger.Msg("called ! : " + enable);
                enable = false;
                return;
            }
        }
    }
}
