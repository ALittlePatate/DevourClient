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
            if (LocalPlayer.LocalPlayer_ == null)
            {
                return null;
            }

            return LocalPlayer.LocalPlayer_.GetComponent<NolanBehaviour>();
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
     
     public class LocalPlayer
    {
        public static GameObject LocalPlayer_;

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
    }
}
