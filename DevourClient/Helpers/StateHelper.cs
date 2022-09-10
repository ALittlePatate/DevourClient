using UnityEngine;
using Opsive.UltimateCharacterController.Character;

﻿namespace DevourClient.Helpers
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
            foreach (NolanBehaviour nb in UnityEngine.Object.FindObjectsOfType<NolanBehaviour>())
            {
                if (nb.entity.IsOwner)
                {
                    return nb;
                }
            }
            return null;
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
        public static GameObject player;

        public static GameObject GetLocalPlayer()
        {
            GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < currentPlayers.Length; i++)
            {
                if (currentPlayers[i].GetComponent<NolanBehaviour>().entity.IsOwner)
                {
                    player = currentPlayers[i];
                    break;
                }
            }

            return player;
        }
    }
}
