using UnityEngine;
using Il2CppOpsive.UltimateCharacterController.Character;
using System.Collections.Generic;
using System.Collections;
using MelonLoader;

namespace DevourClient.Helpers
{
    public class Player
    {
        public static bool IsInGame()
        {
            Il2Cpp.OptionsHelpers optionsHelpers = UnityEngine.Object.FindObjectOfType<Il2Cpp.OptionsHelpers>();
            return optionsHelpers.inGame;
        }
        
        public static bool IsInGameOrLobby()
        {
            return GetPlayer() != null;
        }
        
        public static Il2Cpp.NolanBehaviour GetPlayer()
        {
            if (Entities.LocalPlayer_ == null)
            {
                return null!;
            }

            return Entities.LocalPlayer_.GetComponent<Il2Cpp.NolanBehaviour>();
        }

        public static bool IsPlayerCrawling()
        {
            Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

            if (nb == null)
            {
                return false;
            }

            if (nb.IsCrawling())
            {
                return true;
            }

            return false;
        }
        
    }
    public class BasePlayer
    {
        public GameObject p_GameObject = default!;
        public string Name = default!;
        public string Id = default!;
    }

    public class Entities
     {
        public static GameObject LocalPlayer_ = default!;
        public static BasePlayer[] Players = default!;
        public static Il2Cpp.GoatBehaviour[] GoatsAndRats = default!;
        public static Il2Cpp.SurvivalInteractable[] SurvivalInteractables = default!;
        public static Il2Cpp.KeyBehaviour[] Keys = default!;
        public static Il2Cpp.SurvivalDemonBehaviour[] Demons = default!;
        public static Il2Cpp.SpiderBehaviour[] Spiders = default!;
        public static Il2Cpp.GhostBehaviour[] Ghosts = default!;
        public static Il2Cpp.SurvivalAzazelBehaviour[] Azazels = default!;

        public static IEnumerator GetLocalPlayer()
        {
            for (;;)
            {
                GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 0; i < currentPlayers.Length; i++)
                {
                    if (currentPlayers[i].GetComponent<Il2Cpp.NolanBehaviour>().entity.IsOwner)
                    {
                        LocalPlayer_ = currentPlayers[i];
                        break;
                    }
                }

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }
         
        public static IEnumerator GetAllPlayers()
        {
            for (;;)
            {
                int i = 0;
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    string player_name = "";
                    string player_id = "-1";

                    Il2Cpp.DissonancePlayerTracking dpt = p.gameObject.GetComponent<Il2Cpp.DissonancePlayerTracking>();
                    if (dpt != null)
                    {
                        MelonLogger.Msg(dpt.state.PlayerName + " | " + dpt.state.PlayerId.ToString());
                        player_name = dpt.state.PlayerName;
                        player_id = dpt.state.PlayerId;
                    }

                    Players[i].Id = player_id;
                    Players[i].Name = player_name;
                    Players[i].p_GameObject = p;

                    i++;
                }

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }
        public static IEnumerator GetGoatsAndRats()
        {
            for (;;)
            {
                GoatsAndRats = Il2Cpp.GoatBehaviour.FindObjectsOfType<Il2Cpp.GoatBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetSurvivalInteractables()
        {
            for (;;)
            {
                SurvivalInteractables = Il2Cpp.SurvivalInteractable.FindObjectsOfType<Il2Cpp.SurvivalInteractable>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetKeys()
        {
            for (;;)
            {
                Keys = Il2Cpp.KeyBehaviour.FindObjectsOfType<Il2Cpp.KeyBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetDemons()
        {
            for (;;)
            {
                Demons = Il2Cpp.SurvivalDemonBehaviour.FindObjectsOfType<Il2Cpp.SurvivalDemonBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetSpiders()
        {
            for (;;)
            {
                Spiders = Il2Cpp.SpiderBehaviour.FindObjectsOfType<Il2Cpp.SpiderBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetGhosts()
        {
            for (;;)
            {
                Ghosts = Il2Cpp.GhostBehaviour.FindObjectsOfType<Il2Cpp.GhostBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetAzazels()
        {
            /*
             * ikr AzazelS, because in case we spawn multiple we want the esp to render all of them
            */
            for (;;)
            {
                Azazels = Il2Cpp.SurvivalAzazelBehaviour.FindObjectsOfType<Il2Cpp.SurvivalAzazelBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
