using UnityEngine;
using Il2CppOpsive.UltimateCharacterController.Character;
using System.Collections.Generic;
using System.Collections;
using MelonLoader;
using Il2CppPhoton.Bolt;

namespace DevourClient.Helpers
{
    public class BasePlayer
    {
        public GameObject p_GameObject { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Id { get; set; } = default!;

        public void Kill()
        {
            if (p_GameObject == null)
            {
                return;
            }

            Il2Cpp.SurvivalAzazelBehaviour sab = Il2Cpp.SurvivalAzazelBehaviour.FindObjectOfType<Il2Cpp.SurvivalAzazelBehaviour>();

            if (sab == null)
            {
                return;
            }

            sab.OnKnockout(sab.gameObject, p_GameObject);
        }

        public void Revive()
        {
            if (p_GameObject == null)
            {
                return;
            }

            Il2Cpp.NolanBehaviour nb = p_GameObject.GetComponent<Il2Cpp.NolanBehaviour>();
            Il2Cpp.SurvivalReviveInteractable _reviveInteractable = UnityEngine.Object.FindObjectOfType<Il2Cpp.SurvivalReviveInteractable>();

            _reviveInteractable.Interact(nb.gameObject);
        }

        public void Jumpscare()
        {
            if (p_GameObject == null)
            {
                return;
            }

            Il2Cpp.SurvivalAzazelBehaviour sab = Il2Cpp.SurvivalAzazelBehaviour.FindObjectOfType<Il2Cpp.SurvivalAzazelBehaviour>();

            if (sab == null)
            {
                return;
            }

            sab.OnPickedUpPlayer(sab.gameObject, p_GameObject, false);

            /*
            MelonLogger.Msg(Name);
            Il2Cpp.JumpScare _jumpscare = UnityEngine.Object.FindObjectOfType<Il2Cpp.JumpScare>();
            _jumpscare.player = p_GameObject;
            _jumpscare.Activate(p_GameObject.GetComponent<BoltEntity>());
            */
        }

        public void LockInCage()
        {
            if (p_GameObject == null)
            {
                return;
            }

            BoltNetwork.Instantiate(BoltPrefabs.Cage, p_GameObject.transform.position, Quaternion.identity);

        }

        public void TP()
        {
            if (p_GameObject == null)
            {
                return;
            }

            Il2Cpp.NolanBehaviour nb = Player.GetPlayer();
            nb.TeleportTo(p_GameObject.transform.position, Quaternion.identity);
        }

        public void TPAzazel()
        {
            if (p_GameObject == null)
            {
                return;
            }

            UltimateCharacterLocomotion ucl = Helpers.Map.GetAzazel().GetComponent<UltimateCharacterLocomotion>();

            try
            {
                ucl.SetPosition(p_GameObject.transform.position);
            }
            catch { return; }
        }
    }
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
            if (Entities.LocalPlayer_.p_GameObject == null)
            {
                return null!;
            }

            return Entities.LocalPlayer_.p_GameObject.GetComponent<Il2Cpp.NolanBehaviour>();
        }

        public static bool IsPlayerCrawling()
        {
            Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

            if (nb == null)
            {
                return false;
            }

            return nb.IsCrawling();
        }
        
    }
    
    public class Entities
    {
        public static int MAX_PLAYERS = 4; //will change by calling CreateCustomizedLobby

        public static BasePlayer LocalPlayer_ = new BasePlayer();
        public static BasePlayer[] Players = default!;
        public static Il2Cpp.GoatBehaviour[] GoatsAndRats = default!;
        public static Il2Cpp.SurvivalInteractable[] SurvivalInteractables = default!;
        public static Il2Cpp.KeyBehaviour[] Keys = default!;
        public static Il2Cpp.SurvivalDemonBehaviour[] Demons = default!;
        public static Il2Cpp.SpiderBehaviour[] Spiders = default!;
        public static Il2Cpp.GhostBehaviour[] Ghosts = default!;
        public static Il2Cpp.SurvivalAzazelBehaviour[] Azazels = default!;
        public static Il2Cpp.BoarBehaviour[] Boars = default!;
        public static Il2Cpp.CorpseBehaviour[] Corpses = default!;

        public static IEnumerator GetLocalPlayer()
        {
            for (;;)
            {
                GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 0; i < currentPlayers.Length; i++)
                {
                    if (currentPlayers[i].GetComponent<Il2Cpp.NolanBehaviour>().entity.IsOwner)
                    {
                        LocalPlayer_.p_GameObject = currentPlayers[i];
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
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                Players = new BasePlayer[players.Length];

                int i = 0;
                foreach (GameObject p in players)
                {
                    string player_name = "";
                    string player_id = "-1";

                    Il2Cpp.DissonancePlayerTracking dpt = p.gameObject.GetComponent<Il2Cpp.DissonancePlayerTracking>();
                    if (dpt != null)
                    {
                        player_name = dpt.state.PlayerName;
                        player_id = dpt.state.PlayerId;
                    }

                    if (Players[i] == null)
                    {
                        Players[i] = new BasePlayer();
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

        public static IEnumerator GetBoars()
        {
            for (; ; )
            {
                Boars = Il2Cpp.BoarBehaviour.FindObjectsOfType<Il2Cpp.BoarBehaviour>();

                // Wait 5 seconds before caching objects again.
                yield return new WaitForSeconds(5f);
            }
        }

        public static IEnumerator GetCorpses()
        {
            for (; ; )
            {
                Corpses = Il2Cpp.CorpseBehaviour.FindObjectsOfType<Il2Cpp.CorpseBehaviour>();

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
