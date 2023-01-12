using UnityEngine;
using MelonLoader;
using System.Threading;
using DevourClient.Helpers;
using Il2CppPhoton.Bolt;
using System.Runtime.CompilerServices;
using Il2CppHorror;

[assembly: MelonInfo(typeof(DevourClient.Load), "DevourClient", "1", "ALittlePatate & Jadis")]
[assembly: MelonGame("Straight Back Games", "DEVOUR")]

namespace DevourClient
{
    public class Load : MelonMod
    {
        bool flashlight_toggle = false;
        bool flashlight_colorpick = false;
        bool player_esp_colorpick = false;
        bool azazel_esp_colorpick = false;
        bool level_70 = false;
        bool level_666 = false;
        bool change_server_name = false;
        bool change_steam_name = false;
        bool fly = false;
        float fly_speed = 5;
        bool fastMove = false;
        float _PlayerSpeedMultiplier = 1;
        public bool _IsAutoRespawn = false;
        public static bool unlimitedUV = false;
        public static bool exp_modifier = false;
        public static float exp = 1000f;
        public static bool _walkInLobby = false;
        bool player_esp = false;
        bool player_snapline = false;
        bool azazel_esp = false;
        bool azazel_snapline = false;
        bool spam_message = false;
        bool item_esp = false;
        bool goat_rat_esp = false;
        bool demon_esp = false;
        bool fullbright = false;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("For the Queen !");
            MelonCoroutines.Start(Helpers.Entities.GetLocalPlayer());
            MelonCoroutines.Start(Helpers.Entities.GetGoatsAndRats());
            MelonCoroutines.Start(Helpers.Entities.GetSurvivalInteractables());
            MelonCoroutines.Start(Helpers.Entities.GetKeys());
            MelonCoroutines.Start(Helpers.Entities.GetDemons());
            MelonCoroutines.Start(Helpers.Entities.GetSpiders());
            MelonCoroutines.Start(Helpers.Entities.GetGhosts());
            MelonCoroutines.Start(Helpers.Entities.GetAzazels());
            MelonCoroutines.Start(Helpers.Entities.GetAllPlayers());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                try
                {
                    Il2Cpp.GameUI gameUI = UnityEngine.Object.FindObjectOfType<Il2Cpp.GameUI>();

                    if (Settings.Settings.menu_enable)
                    {
                        gameUI.HideMouseCursor();
                    }
                    else
                    {
                        gameUI.ShowMouseCursor();
                    }
                }
                catch { }

                Settings.Settings.menu_enable = !Settings.Settings.menu_enable;
            }

            if (this.flashlight_toggle && Player.IsInGame() && !this.fullbright)
            {
                Hacks.Misc.BigFlashlight(false);
            }
            else if (!this.flashlight_toggle && Player.IsInGame() && !this.fullbright)
            {
                Hacks.Misc.BigFlashlight(true);
            }

            if (this.fullbright && Player.IsInGame() && !this.flashlight_toggle)
            {
                Hacks.Misc.Fullbright(false);
            }
            else if (!this.fullbright && Player.IsInGame() && !this.flashlight_toggle)
            {
                Hacks.Misc.Fullbright(true);
            }

            if (this.spam_message)
            {
                MelonLogger.Msg("done");
                Hacks.Misc.MessageSpam(Settings.Settings.message_to_spam);
            }

            if (this.level_70 != this.level_666 && !Player.IsInGame())
            {
                if (this.level_70)
                {
                    Hacks.Misc.SetRank(70);
                }
                else
                {
                    Hacks.Misc.SetRank(666);
                }
            }

            if (this.change_server_name && !Player.IsInGame())
            {
                Hacks.Misc.SetServerName("patate on top !");
            }

            if (this.change_steam_name && !Player.IsInGame())
            {
                Hacks.Misc.SetSteamName("patate");
            }

            if (this.fly && Player.IsInGameOrLobby())
            {
                Hacks.Misc.Fly(this.fly_speed);
            }
            
            if(Player.IsInGame() && _IsAutoRespawn && Helpers.Player.IsPlayerCrawling())
            {
                Hacks.Misc.AutoRespawn();                
            }
            
            if (Helpers.Map.GetActiveScene() == "Menu")
            {
                Hacks.Misc.WalkInLobby(_walkInLobby);
            }
            
            if (this.fastMove)
            {
                try
                {
                    Helpers.Entities.LocalPlayer_.p_GameObject.GetComponent<Il2CppOpsive.UltimateCharacterController.Character.UltimateCharacterLocomotion>().TimeScale = this._PlayerSpeedMultiplier;
                }
                catch { return;  }
            }
        }

        public override void OnGUI()
        {
            //from https://www.unknowncheats.me/forum/unity/437277-mono-internal-optimisation-tips.html
            if (UnityEngine.Event.current.type == EventType.Repaint)
            {
                if (this.player_esp || this.player_snapline)
                {
                    foreach (Helpers.BasePlayer p in Helpers.Entities.Players)
                    {
                        if (p == null)
                        {
                            continue;
                        }

                        GameObject player = p.p_GameObject;
                        if (player != null)
                        {

                            if (player.GetComponent<Il2Cpp.NolanBehaviour>().entity.IsOwner)
                            {
                                continue;
                            }

                            Render.Render.DrawBoxESP(player.transform.position, player.transform.GetComponentsInChildren<Transform>()[0], Settings.Settings.player_esp_color, p.Name, this.player_snapline, this.player_esp);
                        }
                    }
                }

                if (this.goat_rat_esp)
                {
                    foreach (Il2Cpp.GoatBehaviour goat in Helpers.Entities.GoatsAndRats)
                    {
                        if (goat != null)
                        {
                            Render.Render.DrawNameESP(goat.transform.position, goat.name.Replace("Survival", "").Replace("(Clone)", ""), new Color(0.94f, 0.61f, 0.18f, 1.0f));
                        }
                    }
                }

                if (this.item_esp)
                {
                    foreach (Il2Cpp.SurvivalInteractable obj in Helpers.Entities.SurvivalInteractables)
                    {
                        if (obj != null)
                        {
                            Render.Render.DrawNameESP(obj.transform.position, obj.prefabName.Replace("Survival", ""), new Color(1.0f, 1.0f, 1.0f));
                        }
                    }

                    foreach (Il2Cpp.KeyBehaviour key in Helpers.Entities.Keys)
                    {
                        if (key != null)
                        {
                            Render.Render.DrawNameESP(key.transform.position, "Key", new Color(1.0f, 1.0f, 1.0f));
                        }
                    }
                }

                if (this.demon_esp)
                {
                    foreach (Il2Cpp.SurvivalDemonBehaviour demon in Helpers.Entities.Demons)
                    {
                        if (demon != null)
                        {
                            Render.Render.DrawNameESP(demon.transform.position, demon.name.Replace("Survival", "").Replace("(Clone)", ""), new Color(1.0f, 0.0f, 0.0f, 1.0f));
                        }
                    }

                    foreach (Il2Cpp.SpiderBehaviour spider in Helpers.Entities.Spiders)
                    {
                        if (spider != null)
                        {
                            Render.Render.DrawNameESP(spider.transform.position, "Spider", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                        }
                    }

                    foreach (Il2Cpp.GhostBehaviour ghost in Helpers.Entities.Ghosts)
                    {
                        if (ghost != null)
                        {
                            Render.Render.DrawNameESP(ghost.transform.position, "Ghost", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                        }
                    }
                }

                if (this.azazel_esp || this.azazel_snapline)
                {
                    foreach (Il2Cpp.SurvivalAzazelBehaviour survivalAzazel in Helpers.Entities.Azazels)
                    {
                        if (survivalAzazel != null)
                        {
                            Render.Render.DrawBoxESP(survivalAzazel.transform.position, survivalAzazel.transform.GetComponentsInChildren<Transform>()[0], Settings.Settings.azazel_esp_color, "Azazel", this.azazel_snapline, this.azazel_esp);
                        }
                    }
                }
            }
            

            if (Settings.Settings.menu_enable) //Si on appuie sur INSERT
            {
                GUI.Label(new Rect(Settings.Settings.x + 450, Settings.Settings.y, 100, 30), "Devour Client"); //Titre du menu
                this.flashlight_toggle = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 20), this.flashlight_toggle, "Big Flashlight"); //Checkbox Flashlight
                this.spam_message = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 250, 150, 20), this.spam_message, "Chat Spam"); //Checkbox Chat Spam
                this.level_70 = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 150, 20), this.level_70, "Level 70"); //Checkbox lvl 70
                this.level_666 = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 220, 150, 20), this.level_666, "Level 666"); //Checkbox lvl 70
                this.change_server_name = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 40, 150, 20), this.change_server_name, "Change server name"); //Checkbox servername
                this.change_steam_name = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 70, 150, 20), this.change_steam_name, "Change steam name"); //Checkbox servername
                this.fly = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 100, 150, 20), this.fly, "Fly"); //Checkbox fly
                this.fly_speed = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 130, 100, 10), this.fly_speed, 5f, 20f); //Slider for the fly speed
                GUI.Label(new Rect(Settings.Settings.x + 310, Settings.Settings.y + 125, 100, 30), this.fly_speed.ToString()); //Prints the value of the slider;
                this._IsAutoRespawn = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 190, 150, 20), this._IsAutoRespawn, "Auto-Respawn");
                
                Load.exp_modifier = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 40, 150, 20), Load.exp_modifier, "Exp Modifier");
                Load.exp = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 70, 100, 10), Load.exp, 1000f, 3000f); //Slider for the fly speed
                GUI.Label(new Rect(Settings.Settings.x + 500, Settings.Settings.y + 65, 100, 30), Load.exp.ToString()); //Prints the value of the slider;

                this.player_esp = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 100, 150, 20), this.player_esp, "Player ESP");
                this.player_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 130, 150, 20), this.player_snapline, "Player Snapline");

                this.azazel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 190, 150, 20), this.azazel_esp, "Azazel ESP");
                this.azazel_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 220, 150, 20), this.azazel_snapline, "Azazel Snapline");

                this.item_esp = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 280, 150, 20), this.item_esp, "Item ESP");
                this.goat_rat_esp = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 310, 150, 20), this.goat_rat_esp, "Goat/Rat ESP");
                this.demon_esp = GUI.Toggle(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 340, 150, 20), this.demon_esp, "Demon ESP");

                Load.unlimitedUV = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 220, 150, 20), Load.unlimitedUV, "Unlimited UV");
                Load._walkInLobby = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 250, 150, 20), Load._walkInLobby, "Walk In Lobby");
                
                this.fastMove = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 280, 150, 20), this.fastMove, "Player Speed");
                this._PlayerSpeedMultiplier = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 310, 100, 10), this._PlayerSpeedMultiplier, 1f, 10f);
                GUI.Label(new Rect(Settings.Settings.x + 310, Settings.Settings.y + 305, 100, 30), this._PlayerSpeedMultiplier.ToString());

                this.fullbright = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 340, 150, 20), this.fullbright, "Fullbright");

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 40, 150, 20), "Unlock Achievements"))
                {
                    Thread AchievementsThread = new Thread(
                        new ThreadStart(Hacks.Unlock.Achievements));
                    AchievementsThread.Start();

                    MelonLogger.Msg("Achievements Unlocked !");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 20), "Unlock Doors") && Player.IsInGame())
                {
                    Hacks.Unlock.Doors();

                    MelonLogger.Msg("Doors Unlocked !");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 250, 150, 20), "Azazel ESP Color"))
                {
                    azazel_esp_colorpick = !azazel_esp_colorpick;
                    MelonLogger.Msg("azazel_esp_colorpick color picker : " + azazel_esp_colorpick.ToString());

                }

                if (azazel_esp_colorpick)
                {
                    Color azazel_esp_colorpick_color_input = DevourClient.Helpers.GUIHelper.ColorPick("Azazel ESP Color", Settings.Settings.azazel_esp_color);
                    Settings.Settings.azazel_esp_color = azazel_esp_colorpick_color_input;
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 160, 150, 20), "Player ESP Color"))
                {
                    player_esp_colorpick = !player_esp_colorpick;
                    MelonLogger.Msg("player_esp_colorpick color picker : " + player_esp_colorpick.ToString());

                }

                if (player_esp_colorpick)
                {
                    Color player_esp_colorpick_color_input = DevourClient.Helpers.GUIHelper.ColorPick("Player ESP Color", Settings.Settings.player_esp_color);
                    Settings.Settings.player_esp_color = player_esp_colorpick_color_input;
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 130, 150, 20), "Flashlight Color")) 
                {
                    flashlight_colorpick = !flashlight_colorpick;
                    MelonLogger.Msg("Flashlight color picker : "+ flashlight_colorpick.ToString());

                }

                if (flashlight_colorpick)
                {
                    Color flashlight_color_input = DevourClient.Helpers.GUIHelper.ColorPick("Flashlight Color", Settings.Settings.flashlight_color);
                    Settings.Settings.flashlight_color = flashlight_color_input;

                    if (Player.IsInGame())
                    {
                        Hacks.Misc.FlashlightColor(flashlight_color_input);
                    }
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 160, 150, 20), "TP Keys") && Player.IsInGame())
                {
                    Hacks.Misc.TPKeys();
                    MelonLogger.Msg("Here are your keys !");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 280, 150, 20), "Instant Win") && Player.IsInGame())
                {
                    Hacks.Misc.InstantWin();
                    MelonLogger.Msg("EZ Win");
                }
                
                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 310, 150, 20), "TP Items") && Player.IsInGame())
                {
                    Hacks.Misc.TPItems();
                    MelonLogger.Msg("TP Items !");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 160, 150, 20), "Random sound"))
                {
                    Hacks.Misc.PlaySound();
                    MelonLogger.Msg("Playing a random sound !");
                }
                
                GUI.Label(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 40, 150, 30), "Azazel & Demons");

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 70, 120, 20), "Sam") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.SpawnAzazel((PrefabId)BoltPrefabs.AzazelSam);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 100, 120, 20), "Molly") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.SpawnAzazel((PrefabId)BoltPrefabs.SurvivalAzazelMolly);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 130, 120, 20), "Anna") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalAnnaNew, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 160, 120, 20), "Demon") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalDemon, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 190, 120, 20), "Ghost") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Ghost, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 220, 120, 20), "Inmate") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalInmate, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 250, 120, 20), "Zara") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.AzazelZara, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                GUI.Label(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 40, 120, 30), "Items");

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 70, 110, 20), "Hay") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalHay");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 100, 110, 20), "First Aid") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalFirstAid");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 130, 110, 20), "Battery") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalBattery");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 160, 110, 20), "Gasoline") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalGasoline");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 190, 110, 20), "Fuse") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalFuse");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 220, 110, 20), "Food") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalRottenFood");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 250, 110, 20), "Egg") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalEgg, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 280, 110, 20), "Bleach") && Player.IsInGameOrLobby())
                {
                    
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 310, 110, 20), "Ritual Book") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("RitualBook-Active-1");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 730, Settings.Settings.y + 340, 110, 20), "Matchbox") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("Matchbox-3");
                }

                GUI.Label(new Rect(Settings.Settings.x + 880, Settings.Settings.y + 40, 120, 30), "Animals");

                if (GUI.Button(new Rect(Settings.Settings.x + 880, Settings.Settings.y + 70, 110, 20), "Rat") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalRat");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 880, Settings.Settings.y + 100, 110, 20), "Goat") && Player.IsInGameOrLobby())
                {
                    Hacks.Misc.CarryObject("SurvivalGoat");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 880, Settings.Settings.y + 130, 110, 20), "Spider") && Player.IsInGameOrLobby())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Spider, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                
                if (Helpers.Map.GetActiveScene() != "")
                {
                    GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 400, 200, 30), $"Functions for the map: {Helpers.Map.GetMapName(Helpers.Map.GetActiveScene())}");
                    
                    if (Helpers.Map.GetActiveScene() != "Menu")
                    {
                        GUI.Label(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 400, 150, 30), "Player list:");
                        int i = 0;
                        foreach (BasePlayer bp in Entities.Players)
                        {
                            if (bp == null || bp.Name == "")
                            {
                                continue;
                            }

                            GUI.Label(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 430 + i, 150, 30), bp.Name);
                            
                            if (GUI.Button(new Rect(Settings.Settings.x + 650, Settings.Settings.y + 430 + i, 60, 20), "Kill"))
                            {
                                bp.Kill();
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 710, Settings.Settings.y + 430 + i, 80, 20), "Revive"))
                            {
                                bp.Revive();
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 790, Settings.Settings.y + 430 + i, 100, 20), "Jumpscare"))
                            {
                                bp.Jumpscare();
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 890, Settings.Settings.y + 430 + i, 80, 20), "TP to"))
                            {
                                bp.TP();
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 970, Settings.Settings.y + 430 + i, 110, 20), "Lock in cage"))
                            {
                                bp.LockInCage();
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 1080, Settings.Settings.y + 430 + i, 100, 20), "TP Azazel"))
                            {
                                bp.TPAzazel();
                            }

                            i += 30;
                        }
                    }
                    
                    switch (Helpers.Map.GetActiveScene())
                    {
                        case "Menu":
                            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 120, 20), "Force Start"))
                            {
                                Il2CppHorror.Menu menu = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
                                menu.OnLobbyStartButtonClick();
                            }
                            break;
                    
                        case "Devour":
                            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 120, 20), "Burn One Goat"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), false);
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 430, 120, 20), "Burn All Goats"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), true);
                            }
                            
                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 430, 120, 20), "TP to Azazel"))
                            {
                                try
                                {
                                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

                                    nb.TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity);
                                }
                                catch
                                {
                                    MelonLogger.Msg("Azazel not found !");
                                }
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 460, 120, 20), "Despawn demons"))
                            {
                                Hacks.Misc.DespawnDemons();
                            }

                            break;

                        case "Molly":
                            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 120, 20), "Burn One Rat"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), false);
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 430, 120, 20), "Burn All Rats"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), true);
                            }
                            
                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 430, 120, 20), "TP to Azazel"))
                            {
                                try
                                {
                                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

                                    nb.TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity);
                                }
                                catch
                                {
                                    MelonLogger.Msg("Azazel not found !");
                                }
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 460, 120, 20), "Despawn demons"))
                            {
                                Hacks.Misc.DespawnDemons();
                            }

                            break;

                        case "Inn":
                            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 120, 20), "Burn One Egg"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), false);
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 430, 120, 20), "Burn All Eggs"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), true);
                            }
                            
                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 430, 150, 20), "Clean The Fountains"))
                            {
                                Hacks.Misc.CleanFountain();
                            }
                            
                            if (GUI.Button(new Rect(Settings.Settings.x + 460, Settings.Settings.y + 430, 120, 20), "TP to Azazel"))
                            {
                                try
                                {
                                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

                                    nb.TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity);
                                }
                                catch
                                {
                                    MelonLogger.Msg("Azazel not found !");
                                }                            
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 460, 120, 20), "Despawn spiders"))
                            {
                                Hacks.Misc.DespawnSpiders();
                            }
                            break;

                        case "Town":
                            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 120, 20), "Burn One Book"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), false);
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 430, 120, 20), "Burn All Books"))
                            {
                                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), true);
                            }
                            
                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 430, 120, 20), "TP to Azazel"))
                            {
                                try
                                {
                                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();

                                    nb.TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity);
                                }
                                catch
                                {
                                    MelonLogger.Msg("Azazel not found !");
                                }
                            }

                            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 460, 120, 20), "Despawn ghosts"))
                            {
                                Hacks.Misc.DespawnGhosts();
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
