using UnityEngine;
using MelonLoader;
using UnityEngine.UI;
using System.Reflection;
using System.Runtime.InteropServices;
using DevourClient.Helpers;
using System.Linq;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using Photon.Bolt;

namespace DevourClient.Hacks
{
    public class Misc
    {
		public static void Fly(float speed) //normal speed 5f
		{
			NolanBehaviour nb = Player.GetPlayer();
			Vector3 pos = nb.transform.position;
			RewiredHelpers helpers = UnityEngine.Object.FindObjectOfType<RewiredHelpers>();
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
		  try {	
                	if (Helpers.Entities.LocalPlayer_.GetComponent<UltimateCharacterLocomotionHandler>() == null)
			{
                    		Helpers.Entities.LocalPlayer_.AddComponent<UltimateCharacterLocomotionHandler>();
                    		Helpers.Entities.LocalPlayer_.GetComponent<UltimateCharacterLocomotionHandler>().enabled = false;
                	}

               		Helpers.Entities.LocalPlayer_.GetComponent<UltimateCharacterLocomotionHandler>().enabled = walk;
            	 }
		catch { return; }	
        	}
	    
	    	public static void BurnRitualObj(string map, bool burnAll)
		{
		    SurvivalObjectBurnController _altar = UnityEngine.Object.FindObjectOfType<SurvivalObjectBurnController>();
		    InnMapController _innMapController = UnityEngine.Object.FindObjectOfType<InnMapController>();

		    if (map != "Inn")
		    {
			if (burnAll)
			{
			    _altar.SkipToGoat(10);
			}
			else
			{
			    _altar.BurnGoat();
			}
		    }
			
		    else
		    {
			if (burnAll){
			 	_innMapController.SetProgressTo(10);
			}
			else{
				_innMapController.IncreaseProgress();
			}
		    }
        	}
	    
	    	public static void SpawnAzazel(PrefabId _azazelPrefabId)
		{
			if (!Photon.Bolt.BoltNetwork.IsServer)
			{
				MelonLogger.Msg("You need to be host to spawn stuff !");
				return;
			}

			GameObject _azazel;
			Vector3 pos = Player.GetPlayer().transform.position;

			_azazel = BoltNetwork.Instantiate(_azazelPrefabId, new Vector3(pos.x, pos.y, pos.z + 1f), Quaternion.identity);

			_azazel.gameObject.GetComponent<SurvivalAzazelBehaviour>().Spawn();
		}

		public static void SpawnGoatOrRat(PrefabId _goatPrefabID)
		{
			if (!Photon.Bolt.BoltNetwork.IsServer)
			{
				MelonLogger.Msg("You need to be host to spawn stuff !");
				return;
			}

			GameObject _goat;
			Vector3 pos = Player.GetPlayer().transform.position;

			_goat = BoltNetwork.Instantiate(_goatPrefabID, new Vector3(pos.x, pos.y, pos.z + 1f), Quaternion.identity);
			_goat.gameObject.GetComponent<GoatBehaviour>().Spawn();
			BehaviorDesigner.Runtime.Behavior goat_behavior = _goat.gameObject.GetComponent<GoatBehaviour>().m_mainBehaviour;
			goat_behavior.EnableBehavior();
		}
		
	    	public static void CleanFountain()
		{
			try
			{
                		GameObject[] fountains = GameObject.FindGameObjectsWithTag("InnFountain");

                		for (int i = 0; i < fountains.Length; i++)
                		{
                    			fountains[i].GetComponent<InnFountainController>().Clean();
                		}
            		}
			catch { return;  }
		}
	    
		public static void AutoRespawn()
		{
			NolanBehaviour nb = Player.GetPlayer();

			Vector3 setNewPosition = nb.transform.position = new Vector3(0.0f, -100.0f, 0.0f);
			nb.locomotion.SetPosition(setNewPosition);
		}
	    
	    public static void TPItems()
		{
		    try
		    {
			NolanBehaviour Nolan = Player.GetPlayer();

			foreach (SurvivalInteractable item in Helpers.Entities.SurvivalInteractables)
			{
			    item.transform.position = Nolan.transform.position + Nolan.transform.forward * Random.RandomRange(1f, 3f);
				}
		    }
		    catch { }
		}
	    
		public static void SetSteamName(string name)
		{
			Horror.Menu Menu_ = UnityEngine.Object.FindObjectOfType<Horror.Menu>();
			Menu_.steamName = name;
		}
		public static void SetServerName(string name)
        {
			Horror.Menu Menu_ = UnityEngine.Object.FindObjectOfType<Horror.Menu>();
			Menu_.serverNameText.text = name;
		}

		public static void BigFlashlight(bool reset)
        {
			NolanBehaviour Nolan = Player.GetPlayer();//UnityEngine.Object.FindObjectOfType<NolanBehaviour>();
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
				flashlightSpot.intensity = 1.5f;
				flashlightSpot.range = 9f;
				flashlightSpot.spotAngle = 70f;
			}
			else
			{
				flashlightSpot.intensity = 1.5f;
				flashlightSpot.range = 200f;
				flashlightSpot.spotAngle = 90f;
			}
			
		}

		public static void Fullbright(bool reset)
		{
			NolanBehaviour Nolan = Player.GetPlayer();//UnityEngine.Object.FindObjectOfType<NolanBehaviour>();
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
				flashlightSpot.intensity = 1.5f;
				flashlightSpot.range = 9f;
				flashlightSpot.spotAngle = 70f;
				flashlightSpot.type = LightType.Spot;
			}
			else
			{
				flashlightSpot.intensity = 1.5f;
				flashlightSpot.range = 200f;
				flashlightSpot.spotAngle = 180f;
				flashlightSpot.type = LightType.Point;
			}

		}
		public static void FlashlightColor(Color color)
        {
			NolanBehaviour Nolan = Player.GetPlayer(); //UnityEngine.Object.FindObjectOfType<NolanBehaviour>();
			Light flashlightSpot = Nolan.flashlightSpot;

			flashlightSpot.color = color;
		}

		public static void TPKeys()
        {
			NolanBehaviour Nolan = Player.GetPlayer(); //UnityEngine.Object.FindObjectOfType<NolanBehaviour>();

			foreach (KeyBehaviour keyBehaviour in Helpers.Entities.Keys)
			{
				bool flag = keyBehaviour == null;
				if (flag)
				{
					return;
				}
				keyBehaviour.transform.position = Nolan.transform.position + Nolan.transform.forward * 1.5f;
			}
		}

		public static void SetRank(int rank)
        {
			NolanRankController NolanRank = UnityEngine.Object.FindObjectOfType<NolanRankController>();

			NolanRank.SetRank(rank);
		}

		public static void MessageSpam(string message)
        {
			if (Helpers.Player.IsInGame())
			{
				GameUI game_ui_class = UnityEngine.Object.FindObjectOfType<GameUI>();

				game_ui_class.textChatInput.text = message;
				game_ui_class.OnChatMessageSubmit();
			}
			else
			{
				Horror.Menu menu_class = UnityEngine.Object.FindObjectOfType<Horror.Menu>();
				menu_class.textChatInput.text = message;
				menu_class.OnChatMessageSubmit();
			}
		}

		public static void ShowMessageBox(string message)
        {
			Horror.Menu menu = UnityEngine.Object.FindObjectOfType<Horror.Menu>();
			menu.ShowMessageModal(message);
		}
		public static void PlaySound()
        {
			/*
			public PlayRandomAudioClip yesClips;
			public PlayRandomAudioClip noClips;
			public PlayRandomAudioClip beckonClips;
			public PlayRandomAudioClip showOffClips;
			public PlayRandomAudioClip screamClips;
			public PlayRandomAudioClip pickupClips;
			public PlayRandomAudioClip burnGoatClips;
			public PlayRandomAudioClip laughClips;
			*/

			PlayRandomAudioClip playRandomAudioClip = UnityEngine.Object.FindObjectOfType<PlayRandomAudioClip>();
			NolanVoiceOvers nolanVoiceOvers = UnityEngine.Object.FindObjectOfType<NolanVoiceOvers>();
			playRandomAudioClip.delay = 0f;

			int num = Random.RandomRangeInt(0, 10);
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
		public static void InstantWin()
        {
			Survival survival_class = UnityEngine.Object.FindObjectOfType<Survival>();

			try
            {
				survival_class.PlayEnding("InnWin");
			}
			catch
            {
				try
                {
					survival_class.PlayEnding("AsylumWin");
				}
				catch
                {
					try
                    {
						survival_class.PlayEnding("TownWin");
					}
					catch
                    {
						survival_class.PlayEnding("Win");
					}
				}
            }
		}
    }
}
