﻿using UnityEngine;
using MelonLoader;
using UnityEngine.UI;
using System.Reflection;
using System.Runtime.InteropServices;
using DevourClient.Helpers;
using System.Linq;
using System.Collections.Generic;
using Il2CppOpsive.UltimateCharacterController.Character;
using Il2CppPhoton.Bolt;

namespace DevourClient.Hacks
{
    public class Misc
    {
		public static void Fly(float speed) //normal speed 5f
		{
			Il2Cpp.NolanBehaviour nb = Player.GetPlayer();
			Vector3 pos = nb.transform.position;
			Il2Cpp.RewiredHelpers helpers = UnityEngine.Object.FindObjectOfType<Il2Cpp.RewiredHelpers>();
			if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), helpers.GetCurrentBinding("Move Up").ToString().Replace(" ", ""))))
			{
				pos += nb.transform.forward * speed * Time.deltaTime;
			}
			if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), helpers.GetCurrentBinding("Move Down").ToString().Replace(" ", ""))))
			{
				pos += -nb.transform.forward * speed * Time.deltaTime;
			}
			if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), helpers.GetCurrentBinding("Move Right").ToString().Replace(" ", ""))))
			{
				pos += nb.transform.right * speed * Time.deltaTime;
			}
			if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), helpers.GetCurrentBinding("Move Left").ToString().Replace(" ", ""))))
			{
				pos += -nb.transform.right * speed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Space))
			{
				pos += nb.transform.up * speed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.LeftControl))
			{
				pos += -nb.transform.up * speed * Time.deltaTime;
			}
			nb.locomotion.SetPosition(pos, false);
		}
	    
	    public static void WalkInLobby(bool walk)
        {
			GameObject LocalPlayer = Helpers.Entities.LocalPlayer_.p_GameObject;
			if (LocalPlayer == null)
            {
				return;
            }

			//GetComponent called only once as AddComponent returns a component
			UltimateCharacterLocomotionHandler cmp = Helpers.Entities.LocalPlayer_.p_GameObject.GetComponent<UltimateCharacterLocomotionHandler>();

			if (cmp == null)
			{
				cmp = LocalPlayer.AddComponent<UltimateCharacterLocomotionHandler>();
                cmp.enabled = false;
            }

			cmp.enabled = walk;
		}
	    
	    public static void BurnRitualObj(string map, bool burnAll)
		{
			switch (map)
            {
				case "Inn":
					Il2Cpp.InnMapController _innMapController = UnityEngine.Object.FindObjectOfType<Il2Cpp.InnMapController>();
                    if (!_innMapController) {
                        return;
                    }

                    if (burnAll){
                        _innMapController.SetProgressTo(10);
                    }
                    else{
                        _innMapController.IncreaseProgress();
                    }
					break;

				case "Slaughterhouse":
                    Il2Cpp.SlaughterhouseAltarController _slaughterhouseAltarController = UnityEngine.Object.FindObjectOfType<Il2Cpp.SlaughterhouseAltarController>();
                    if (!_slaughterhouseAltarController) {
                        return;
                    }

                    if (burnAll)
                    {
                        _slaughterhouseAltarController.BurnGoat();
                    }
                    else
                    {
                        _slaughterhouseAltarController.SkipToGoat(10);
                    }
					break;

				default:
					Il2Cpp.SurvivalObjectBurnController _altar = UnityEngine.Object.FindObjectOfType<Il2Cpp.SurvivalObjectBurnController>();
                    if (!_altar) {
                        return;
                    }

                    if (burnAll)
                    {
                        _altar.SkipToGoat(10);
                    }
                    else
                    {
                        _altar.BurnGoat();
                    }
					break;
            }
        }
	    
	    public static void SpawnAzazel(PrefabId _azazelPrefabId)
		{
			if (!Il2CppPhoton.Bolt.BoltNetwork.IsServer)
			{
				Hacks.Misc.ShowMessageBox("You need to be host to spawn stuff !");
				return;
			}

			GameObject _localPlayer = Helpers.Entities.LocalPlayer_.p_GameObject;

			if (_localPlayer != null)
			{
                Vector3 pos = _localPlayer.transform.position;

                GameObject _azazel;

                _azazel = BoltNetwork.Instantiate(_azazelPrefabId, new Vector3(pos.x, pos.y, pos.z + 1f), Quaternion.identity);
                Il2Cpp.SurvivalAzazelBehaviour azazelBehaviour = _azazel?.GetComponent<Il2Cpp.SurvivalAzazelBehaviour>();

                if (_azazel != null)
				{
					if (azazelBehaviour != null)
					{
                        _azazel.gameObject.GetComponent<Il2Cpp.SurvivalAzazelBehaviour>().Spawn();
					}
					else
					{
						MelonLogger.Error("azazelBehaviour is null!");
					}
                }
				else
				{
                    MelonLogger.Error("azazel is null!");
                }
            }
		}

		public static void CarryObject(string name)
		{
			Il2Cpp.NolanBehaviour nb = Helpers.Entities.LocalPlayer_.p_GameObject.GetComponent<Il2Cpp.NolanBehaviour>();

			nb.StartCarry(name);
		}

        public static void CleanFountain()
        {
            GameObject[] fountains = GameObject.FindGameObjectsWithTag("InnFountain");

            foreach (GameObject fountain in fountains)
            {
                fountain.GetComponent<Il2Cpp.InnFountainController>().Clean();
            }
        }

        public static void AutoRespawn()
		{
			Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

			Il2Cpp.SurvivalReviveInteractable _reviveInteractable = UnityEngine.Object.FindObjectOfType<Il2Cpp.SurvivalReviveInteractable>(); //probably can't be null

			_reviveInteractable.Interact(nb.gameObject);
		}
		public static void TPItems()
		{
            Il2Cpp.NolanBehaviour Nolan = Player.GetPlayer();

            foreach (Il2Cpp.SurvivalInteractable item in Helpers.Entities.SurvivalInteractables)
            {
                item.transform.position = Nolan.transform.position + Nolan.transform.forward * UnityEngine.Random.RandomRange(1f, 3f);
            }
		}
	    
		public static void CreateCustomizedLobby(int lobbySize = 4, bool isPrivate = false, Il2CppUdpKit.Platform.Photon.PhotonRegion.Regions __region = Il2CppUdpKit.Platform.Photon.PhotonRegion.Regions.BEST_REGION)
        {			
			//TODO : make it so we can specify a password for a private lobby

			Il2CppHorror.Menu _menu = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();

			Il2CppUdpKit.Platform.PhotonPlatformConfig __photonPlatformConfig = new Il2CppUdpKit.Platform.PhotonPlatformConfig();
            __photonPlatformConfig.Region = Il2CppUdpKit.Platform.Photon.PhotonRegion.regions[__region];

			BoltLauncher.SetUdpPlatform(new Il2CppUdpKit.Platform.PhotonPlatform(__photonPlatformConfig));

            BoltConfig __config = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>().boltConfig;
			Toggle __toggle = _menu.hostPrivateServer;

			__toggle.isOn = isPrivate;
            __config.serverConnectionLimit = lobbySize;

			BoltLauncher.StartServer(__config, null);
			
			Il2CppHorror.Menu.ShowCanvasGroup(_menu.loadingCanvasGroup, true);
            Il2CppHorror.Menu.ShowCanvasGroup(_menu.hostCanvasGroup, false);
            Il2CppHorror.Menu.ShowCanvasGroup(_menu.mainMenuCanvasGroup, false);
        }
		
		public static void SetSteamName(string name)
		{
			Il2CppHorror.Menu Menu_ = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
			if (Menu_ == null)
            {
				return;
            }

			Menu_.steamName = name;
		}
		public static void SetServerName(string name)
        {
			Il2CppHorror.Menu Menu_ = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
			if (Menu_ == null)
			{
				return;
			}

			Menu_.serverNameText.text = name;
		}

		public static void BigFlashlight(bool reset)
        {
			Il2Cpp.NolanBehaviour Nolan = Player.GetPlayer();
			if (Nolan == null)
            {
				return;
            }

			Light flashlightSpot = Nolan.flashlightSpot;
			if (flashlightSpot == null)
            {
				return;
            }

			if (reset)
			{
				flashlightSpot.intensity = 1.4f;
				flashlightSpot.range = 9f;
				flashlightSpot.spotAngle = 70f;
			}
			else
			{
				flashlightSpot.intensity = 1.1f;
				flashlightSpot.range = 200f;
				flashlightSpot.spotAngle = 90f;
			}
			
		}

		public static List<Transform> GetAllBones(Animator a)
		{
			List<Transform> Bones = new List<Transform>
			{
				a.GetBoneTransform(HumanBodyBones.Head), // 0
                a.GetBoneTransform(HumanBodyBones.Neck), // 1
                a.GetBoneTransform(HumanBodyBones.Spine), // 2
                a.GetBoneTransform(HumanBodyBones.Hips), // 3

                a.GetBoneTransform(HumanBodyBones.LeftShoulder), // 4
                a.GetBoneTransform(HumanBodyBones.LeftUpperArm), // 5
                a.GetBoneTransform(HumanBodyBones.LeftLowerArm), // 6
                a.GetBoneTransform(HumanBodyBones.LeftHand), // 7

                a.GetBoneTransform(HumanBodyBones.RightShoulder), // 8
                a.GetBoneTransform(HumanBodyBones.RightUpperArm), // 9
                a.GetBoneTransform(HumanBodyBones.RightLowerArm), // 10
                a.GetBoneTransform(HumanBodyBones.RightHand), // 11

                a.GetBoneTransform(HumanBodyBones.LeftUpperLeg), // 12
                a.GetBoneTransform(HumanBodyBones.LeftLowerLeg), // 13
                a.GetBoneTransform(HumanBodyBones.LeftFoot), // 14

                a.GetBoneTransform(HumanBodyBones.RightUpperLeg), // 15
                a.GetBoneTransform(HumanBodyBones.RightLowerLeg), // 16
                a.GetBoneTransform(HumanBodyBones.RightFoot) // 17
            };

			return Bones;
		}

		public static void Fullbright(bool reset)
		{
			Il2Cpp.NolanBehaviour Nolan = Player.GetPlayer();
			if (Nolan == null)
			{
				return;
			}

			Light flashlightSpot = Nolan.flashlightSpot;
			if (flashlightSpot == null)
			{
				return;
			}

			if (reset)
			{
				flashlightSpot.intensity = 1.4f;
				flashlightSpot.range = 9f;
				flashlightSpot.spotAngle = 70f;
				flashlightSpot.type = LightType.Spot;
				flashlightSpot.shadows = LightShadows.Soft;
			}
			else
			{
				flashlightSpot.intensity = 1.1f;
				flashlightSpot.range = 200f;
				flashlightSpot.spotAngle = 179f;
				flashlightSpot.type = LightType.Point;
				flashlightSpot.shadows = LightShadows.None;
			}

		}
		public static void FlashlightColor(Color color)
        {
			Il2Cpp.NolanBehaviour Nolan = Player.GetPlayer();
			Light flashlightSpot = Nolan.flashlightSpot;

			flashlightSpot.color = color;
		}

		public static void TPKeys()
        {
			//TOFIX: spawn manually the missing key in slaughterhouse
			Il2Cpp.NolanBehaviour Nolan = Player.GetPlayer();

			foreach (Il2Cpp.KeyBehaviour keyBehaviour in Helpers.Entities.Keys)
			{
				if (keyBehaviour == null)
				{
					return;
				}
				keyBehaviour.transform.position = Nolan.transform.position + Nolan.transform.forward * 1.5f;
			}
		}

		public static void SetRank(int rank)
        {
			Il2Cpp.NolanRankController NolanRank = UnityEngine.Object.FindObjectOfType<Il2Cpp.NolanRankController>();
			if (NolanRank == null)
            {
				return;
            }
			NolanRank.SetRank(rank);
		}

		public static void MessageSpam(string message)
        {
			//TOFIX : not spamming anymore :/
			if (Helpers.Player.IsInGame())
			{
				Il2Cpp.GameUI game_ui_class = UnityEngine.Object.FindObjectOfType<Il2Cpp.GameUI>();

				game_ui_class.textChatInput.text = message;
				game_ui_class.OnChatMessageSubmit();
			}
			else
			{
				Il2CppHorror.Menu menu_class = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
				menu_class.textChatInput.text = message;
				menu_class.OnChatMessageSubmit();
			}
		}

		public static void DespawnDemons()
		{
			foreach (Il2Cpp.SurvivalDemonBehaviour demon in Helpers.Entities.Demons)
			{
				if (demon != null)
				{
					demon.Despawn();
				}
			}
		}

		public static void DespawnSpiders()
		{
			foreach (Il2Cpp.SpiderBehaviour spider in Helpers.Entities.Spiders)
			{
				if (spider != null)
				{
					spider.Despawn();
				}
			}
		}

		public static void DespawnGhosts()
		{
			foreach (Il2Cpp.GhostBehaviour ghost in Helpers.Entities.Ghosts)
			{
				if (ghost != null)
				{
					ghost.Despawn();
				}
			}
		}

		public static void DespawnBoars()
		{
			foreach (Il2Cpp.BoarBehaviour boar in Helpers.Entities.Boars)
			{
				if (boar != null)
				{
					boar.Despawn();
				}
			}
		}

		public static void DespawnCorpses()
		{
			foreach (Il2Cpp.CorpseBehaviour corpse in Helpers.Entities.Corpses)
			{
				if (corpse != null)
				{
					corpse.Despawn();
				}
			}
		}

		public static void ShowMessageBox(string message)
        {
			//not used, might be useful, some day
			Il2CppHorror.Menu menu = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
			menu.ShowMessageModal(message);
		}
		
		public static void PlaySound()
        {
			Il2Cpp.PlayRandomAudioClip playRandomAudioClip = UnityEngine.Object.FindObjectOfType<Il2Cpp.PlayRandomAudioClip>();
			Il2Cpp.NolanVoiceOvers nolanVoiceOvers = UnityEngine.Object.FindObjectOfType<Il2Cpp.NolanVoiceOvers>();
			playRandomAudioClip.delay = 0f;

			int num = UnityEngine.Random.RandomRangeInt(0, 10);
			switch (num)
            {
				case 0:
					nolanVoiceOvers.yesClips.Play();
					return;
				case 1:
					nolanVoiceOvers.noClips.Play();
					return;
				case 2:
					nolanVoiceOvers.beckonClips.Play();
					return;
				case 3:
					nolanVoiceOvers.showOffClips.Play();
					return;
				case 4:
					nolanVoiceOvers.screamClips.Play();
					return;
				case 5:
					nolanVoiceOvers.pickupClips.Play();
					return;
				case 6:
					nolanVoiceOvers.burnGoatClips.Play();
					return;
				case 7:
					nolanVoiceOvers.laughClips.Play();
					return;
				case 8:
					nolanVoiceOvers.PlayMoan();
					return;
				case 9:
					nolanVoiceOvers.Scream();
					return;
				default:
					return;
            }
		}
		
		public static void FreezeAzazel()
		{
			if (Helpers.Map.GetActiveScene() == "Menu")
			{
				return;
			}

            if (Helpers.Map.GetAzazel() == null) {
                return;
            }

            UltimateCharacterLocomotion _azazelLocomotion = Helpers.Map.GetAzazel().GetComponent<UltimateCharacterLocomotion>();

            if (_azazelLocomotion == null)
            {
                return;
            }

            if (_azazelLocomotion.TimeScale > 0.0f) 
            {
                _azazelLocomotion.TimeScale = 0f; //host only (?)
            }
            else
            {
                _azazelLocomotion.TimeScale = 1.0f;
            }
		}
		
		public static void InstantWin()
        {
			Il2Cpp.Survival survival_class = UnityEngine.Object.FindObjectOfType<Il2Cpp.Survival>();
			string map_name = Map.GetMapName(Map.GetActiveScene());

			if (map_name == "Menu")
            {
				return;
            }

			if (map_name == "Farmhouse")
            {
				survival_class.PlayEnding("Win");
				return;
			}

			survival_class.PlayEnding(map_name+"Win");
		}
    }
}
