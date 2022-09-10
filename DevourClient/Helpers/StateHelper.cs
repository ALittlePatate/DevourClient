using UnityEngine;
using Opsive.UltimateCharacterController.Character;
using System.Collections.Generic;
using System.Collections;

namespace DevourClient.Helpers
{
    public class Player
    {
        public static bool IsInGame()
        {
            OptionsHelpers optionsHelpers = UnityEngine.Object.FindObjectOfType<OptionsHelpers>();
            return optionsHelpers.inGame;
        }
        
        public static bool IsInGameOrLobby()
        {
            return GetPlayer() != null;
        }
        
        public static NolanBehaviour GetPlayer()
        {
            if (Entities.LocalPlayer_ == null)
            {
                return null;
            }

            return Entities.LocalPlayer_.GetComponent<NolanBehaviour>();
        }
        
        public static bool IsPlayerCrawling()
        {
            NolanBehaviour nb = Player.GetPlayer();

            if (nb.IsCrawling())
            {
                return true;
            }

            return false;
        }
        
    }
     
     public class Entities
     {
        public static GameObject LocalPlayer_;
        public static GoatBehaviour[] GoatsAndRats;
        public static SurvivalInteractable[] SurvivalInteractables;
        public static KeyBehaviour[] Keys;
        public static SurvivalDemonBehaviour[] Demons;
        public static SpiderBehaviour[] Spiders;
        public static GhostBehaviour[] Ghosts;
        public static SurvivalAzazelBehaviour[] Azazels;

        public static IEnumerator GetLocalPlayer()
        {
            for (;;)
            {
                GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 0; i < currentPlayers.Length; i++)
                {
                    if (currentPlayers[i].GetComponent<NolanBehaviour>().entity.IsOwner)
                    {
                        LocalPlayer_ = currentPlayers[i];
                        break;
                    }
                }

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }
         
        public static List<GameObject> GetAllPlayers()
        {
            GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
            List<GameObject> result = new List<GameObject>();

            for (int i = 0; i < currentPlayers.Length; i++)
            {
                result.Add(currentPlayers[i]);      
            }

            return result;
            
        }

        public static IEnumerator GetGoatsAndRats()
        {
            for (;;)
            {
                GoatsAndRats = GoatBehaviour.FindObjectsOfType<GoatBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetSurvivalInteractables()
        {
            for (;;)
            {
                SurvivalInteractables = SurvivalInteractable.FindObjectsOfType<SurvivalInteractable>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetKeys()
        {
            for (;;)
            {
                Keys = KeyBehaviour.FindObjectsOfType<KeyBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetDemons()
        {
            for (;;)
            {
                Demons = SurvivalDemonBehaviour.FindObjectsOfType<SurvivalDemonBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetSpiders()
        {
            for (;;)
            {
                Spiders = SpiderBehaviour.FindObjectsOfType<SpiderBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetGhosts()
        {
            for (;;)
            {
                Ghosts = GhostBehaviour.FindObjectsOfType<GhostBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GeAzazels()
        {
            /*
             * ikr AzazelS, because in case we spawn multiple we want the esp to render all of them
            */
            for (;;)
            {
                Azazels = SurvivalAzazelBehaviour.FindObjectsOfType<SurvivalAzazelBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
