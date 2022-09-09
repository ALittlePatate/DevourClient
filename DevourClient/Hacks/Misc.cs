﻿using UnityEngine;
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
                	if (Helpers.LocalPlayer.GetLocalPlayer().GetComponent<UltimateCharacterLocomotionHandler>() == null)
			{
                    		Helpers.LocalPlayer.GetLocalPlayer().AddComponent<UltimateCharacterLocomotionHandler>();
                    		Helpers.LocalPlayer.GetLocalPlayer().GetComponent<UltimateCharacterLocomotionHandler>().enabled = false;
                	}

               		Helpers.LocalPlayer.GetLocalPlayer().GetComponent<UltimateCharacterLocomotionHandler>().enabled = walk;
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
			GameObject _azazel;
			Vector3 pos = Player.GetPlayer().transform.position;

			_azazel = BoltNetwork.Instantiate(_azazelPrefabId, new Vector3(pos.x, pos.y, pos.z + 1f), Quaternion.identity);

			_azazel.gameObject.GetComponent<SurvivalAzazelBehaviour>().Spawn();
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

			List<SurvivalInteractable> items = new List<SurvivalInteractable>();
			items = Object.FindObjectsOfType<SurvivalInteractable>().ToList<SurvivalInteractable>();

			foreach (SurvivalInteractable item in items)
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
			//Ohhhh yes, that's some great code, don't you like try and catches ?
			//it's for fixing a glitch that activates the big flashlight during the loading of a map
			//so the things are not loaded and it throws a shit ton of errors in the console
			try
            {
				NolanBehaviour Nolan = Player.GetPlayer();//UnityEngine.Object.FindObjectOfType<NolanBehaviour>();

				Light flashlightSpot = Nolan.flashlightSpot;

				if (reset)
				{
					flashlightSpot.intensity = 1.5f;
					flashlightSpot.range = 9f;
				}
				else
				{
					flashlightSpot.intensity = 1.5f;
					flashlightSpot.range = 200f;
				}
			}
			catch
            {
				return;
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

			foreach (KeyBehaviour keyBehaviour in UnityEngine.Object.FindObjectsOfType<KeyBehaviour>())
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
