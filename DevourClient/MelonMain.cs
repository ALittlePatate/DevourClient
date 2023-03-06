using UnityEngine;
using MelonLoader;
using System.Threading;
using DevourClient.Helpers;
using Il2CppPhoton.Bolt;
using System.Runtime.CompilerServices;
using Il2CppHorror;

[assembly: VerifyLoaderVersion(0, 6, 0, true)] //Minimum MelonLoader version is V6.0.0, sanity check for people who use 5.7 and wonder why it crashes :)
[assembly: MelonInfo(typeof(DevourClient.Load), "DevourClient", "1", "ALittlePatate & Jadis")]
[assembly: MelonGame("Straight Back Games", "DEVOUR")]

namespace DevourClient
{
    enum CurrentTab : int
    {
        Visuals = 0,
        Entities = 1,
        Map = 2,
        ESP = 3,
        Items = 4,
        Misc = 5,
        Players = 6
    }
    
    public class Load : MelonMod
    {
        static Rect windowRect = new Rect(Settings.Settings.x + 10, Settings.Settings.y + 10, 700, 700);
        static CurrentTab current_tab = CurrentTab.Visuals;
    
        static bool flashlight_toggle = false;
        static bool flashlight_colorpick = false;
        static bool player_esp_colorpick = false;
        static bool azazel_esp_colorpick = false;
        static bool spoofLevel = false;
        static float spoofLevelValue = 0;
        static bool change_server_name = false;
        static bool change_steam_name = false;
        static bool fly = false;
        static float fly_speed = 5;
        static bool fastMove = false;
        static float _PlayerSpeedMultiplier = 1;
        public static bool _IsAutoRespawn = false;
        public static bool unlimitedUV = false;
        public static bool exp_modifier = false;
        public static float exp = 1000f;
        public static bool _walkInLobby = false;
        static bool player_esp = false;
        static bool player_snapline = false;
        static bool azazel_esp = false;
        static bool azazel_snapline = false;
        static bool spam_message = false;
        static bool item_esp = false;
        static bool goat_rat_esp = false;
        static bool demon_esp = false;
        static bool fullbright = false;

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

            if (Player.IsInGame())
            {
                if (flashlight_toggle && !fullbright)
                {
                    Hacks.Misc.BigFlashlight(false);
                }
                else if (!flashlight_toggle && !fullbright)
                {
                    Hacks.Misc.BigFlashlight(true);
                }

                if (fullbright && !flashlight_toggle)
                {
                    Hacks.Misc.Fullbright(false);
                }
                else if (!fullbright && !flashlight_toggle)
                {
                    Hacks.Misc.Fullbright(true);
                }

                if (_IsAutoRespawn && Helpers.Player.IsPlayerCrawling())
                {
                    Hacks.Misc.AutoRespawn();
                }
            }
            else
            {
                if (change_server_name)
                {
                    Hacks.Misc.SetServerName("patate on top !");
                }

                if (change_steam_name)
                {
                    Hacks.Misc.SetSteamName("patate");
                }
            }

            if (spam_message)
            {
                MelonLogger.Msg("done");
                Hacks.Misc.MessageSpam(Settings.Settings.message_to_spam);
            }

            if (spoofLevel)
            {
                Hacks.Misc.SetRank((int)spoofLevelValue);
            }

            if (Input.GetKeyDown(Settings.Settings.flyKey))
            {
                fly = !fly;
            }

            if (fly && Player.IsInGameOrLobby())
            {
                Hacks.Misc.Fly(fly_speed);
            }
            
            if (Helpers.Map.GetActiveScene() == "Menu")
            {
                Hacks.Misc.WalkInLobby(_walkInLobby);
            }
            
            if (fastMove)
            {
                try
                {
                    Helpers.Entities.LocalPlayer_.p_GameObject.GetComponent<Il2CppOpsive.UltimateCharacterController.Character.UltimateCharacterLocomotion>().TimeScale = _PlayerSpeedMultiplier;
                }
                catch { return;  }
            }
        }

        public override void OnGUI()
        {
            GUI.backgroundColor = Color.grey;

            GUI.skin.button.normal.background = GUIHelper.MakeTex(2, 2, Color.black);
            GUI.skin.button.normal.textColor = Color.white;

            GUI.skin.button.hover.background = GUIHelper.MakeTex(2, 2, Color.green);
            GUI.skin.button.hover.textColor = Color.black;

            GUI.skin.toggle.onNormal.textColor = Color.yellow;
        
            //from https://www.unknowncheats.me/forum/unity/437277-mono-internal-optimisation-tips.html
            if (UnityEngine.Event.current.type == EventType.Repaint)
            {
                if (player_esp || player_snapline)
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

                            Render.Render.DrawBoxESP(player.transform.position, player.transform.GetComponentsInChildren<Transform>()[0], Settings.Settings.player_esp_color, p.Name, player_snapline, player_esp);
                        }
                    }
                }

                if (goat_rat_esp)
                {
                    foreach (Il2Cpp.GoatBehaviour goat in Helpers.Entities.GoatsAndRats)
                    {
                        if (goat != null)
                        {
                            Render.Render.DrawNameESP(goat.transform.position, goat.name.Replace("Survival", "").Replace("(Clone)", ""), new Color(0.94f, 0.61f, 0.18f, 1.0f));
                        }
                    }
                }

                if (item_esp)
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

                if (demon_esp)
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

                if (azazel_esp || azazel_snapline)
                {
                    foreach (Il2Cpp.SurvivalAzazelBehaviour survivalAzazel in Helpers.Entities.Azazels)
                    {
                        if (survivalAzazel != null)
                        {
                            Render.Render.DrawBoxESP(survivalAzazel.transform.position, survivalAzazel.transform.GetComponentsInChildren<Transform>()[0], Settings.Settings.azazel_esp_color, "Azazel", azazel_snapline, azazel_esp);
                        }
                    }
                }
            }
            

            if (Settings.Settings.menu_enable) //Si on appuie sur INSERT
            {
                windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)Tabs, "DevourClient"); 
            }   
        }
        
        public static void Tabs(int windowID)
        {
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 20, 95, 40), "Visuals"))
            {
                current_tab = CurrentTab.Visuals;
            }

            
            if (GUI.Button(new Rect(Settings.Settings.x + 105, Settings.Settings.y + 20, 95, 40), "Entites"))
            {
                current_tab = CurrentTab.Entities;
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 20, 95, 40), "Map Specific"))
            {
                current_tab = CurrentTab.Map;
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 295, Settings.Settings.y + 20, 95, 40), "ESP"))
            {
                current_tab = CurrentTab.ESP;
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 390, Settings.Settings.y + 20, 95, 40), "Items"))
            {
                current_tab = CurrentTab.Items;
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 485, Settings.Settings.y + 20, 95, 40), "Misc"))
            {
                current_tab = CurrentTab.Misc;
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 580, Settings.Settings.y + 20, 95, 40), "Player"))
            {
                current_tab = CurrentTab.Players;
            }
            

            switch (current_tab)
            {
                case CurrentTab.Visuals:
                    VisualsTab();
                    break;
                case CurrentTab.Entities:
                    EntitiesTab();
                    break;
                case CurrentTab.Map:
                    MapSpecificTab();
                    break;
                case CurrentTab.ESP:
                    EspTab();
                    break;
                case CurrentTab.Items:
                    ItemsTab();
                    break;
                case CurrentTab.Misc:
                    MiscTab();
                    break;
                case CurrentTab.Players:
                    PlayersTab();
                    break;

            }

            GUI.DragWindow();
        }

        // features
        private static void VisualsTab()
        {
            // draw visuals

            flashlight_toggle = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 120, 30), flashlight_toggle, "Big Flashlight");
            fullbright = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 120, 30), fullbright, "Fullbright");
            unlimitedUV = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 130, 130, 30), unlimitedUV, "Unlimited UV Light");


            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 160, 130, 30), "Flashlight Color"))
            {
                flashlight_colorpick = !flashlight_colorpick;
                MelonLogger.Msg("Flashlight color picker : " + flashlight_colorpick.ToString());

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
        }

        private static void EntitiesTab()
        {
            //draw entities

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 130, 30), "TP items to you"))
            {
                Hacks.Misc.TPItems();
                MelonLogger.Msg("TP Items!");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110, 130, 30), "Freeze azazel"))
            {
                Hacks.Misc.FreezeAzazel();
            }

            GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 150, 120, 30), "Azazel & Demons");

            // azazel

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 180, 60, 25), "Sam") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                Hacks.Misc.SpawnAzazel((PrefabId)BoltPrefabs.AzazelSam);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 80, Settings.Settings.y + 180, 60, 25), "Molly") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                Hacks.Misc.SpawnAzazel((PrefabId)BoltPrefabs.SurvivalAzazelMolly);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 180, 60, 25), "Anna") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.SurvivalAnnaNew, Player.GetPlayer().transform.position, Quaternion.identity);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 180, 60, 25), "Zara") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.AzazelZara, Player.GetPlayer().transform.position, Quaternion.identity);
            }
            
            /*
            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 180, 60, 25), "Nathan") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.AzazelNathan, Player.GetPlayer().transform.position, Quaternion.identity);
            }
            */

            // demon

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 220, 60, 25), "Ghost") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.Ghost, Player.GetPlayer().transform.position, Quaternion.identity);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 80, Settings.Settings.y + 220, 60, 25), "Inmate") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.SurvivalInmate, Player.GetPlayer().transform.position, Quaternion.identity);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 220, 60, 25), "Demon") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.SurvivalDemon, Player.GetPlayer().transform.position, Quaternion.identity);
            }
            
            /*
            if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 220, 60, 25), "Boar") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.Boar, Player.GetPlayer().transform.position, Quaternion.identity);
            }
            */

            // Animal

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 260, 60, 25), "Rat"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    try
                    {
                        BoltNetwork.Instantiate(BoltPrefabs.SurvivalRat, Player.GetPlayer().transform.position, Quaternion.identity);
                    }
                    catch { }
                }

                if (Player.IsInGame() && !Player.IsPlayerCrawling())
                {
                    try
                    {
                        Hacks.Misc.CarryObject("SurvivalRat");
                    }
                    catch { }
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 80, Settings.Settings.y + 260, 60, 25), "Goat"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    try
                    {
                        BoltNetwork.Instantiate(BoltPrefabs.SurvivalGoat, Player.GetPlayer().transform.position, Quaternion.identity);
                    }
                    catch { }
                }

                if (Player.IsInGame() && !Player.IsPlayerCrawling())
                {
                    try
                    {
                        Hacks.Misc.CarryObject("SurvivalGoat");
                    }
                    catch { }
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 260, 60, 25), "Spider") && BoltNetwork.IsServer && Player.IsInGameOrLobby())
            {
                BoltNetwork.Instantiate(BoltPrefabs.Spider, Player.GetPlayer().transform.position, Quaternion.identity);
            }
            
            /*if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 260, 60, 25), "Pig"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    try
                    {
                        BoltNetwork.Instantiate(BoltPrefabs.SurvivalPig, Player.GetPlayer().transform.position, Quaternion.identity);
                    }
                    catch { }
                }

                if (Player.IsInGame() && !Player.IsPlayerCrawling())
                {
                    try
                    {
                        Hacks.Misc.CarryObject("SurvivalPig");
                    }
                    catch { }
                }
            }*/
        }

        private static void MapSpecificTab()
        {
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 30), "Instant Win") && Player.IsInGame() && BoltNetwork.IsSinglePlayer)
            {
                Hacks.Misc.InstantWin();
                MelonLogger.Msg("EZ Win");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110, 150, 30), "Burn a ritual object"))
            {
                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), false);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 150, 150, 30), "Burn all ritual objects"))
            {
                Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), true);
            }

            switch (Helpers.Map.GetActiveScene())
            {
                case "Menu":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 150, 30), "Force Start Game") && BoltNetwork.IsServer && !Player.IsInGame())
                    {
                        Il2CppHorror.Menu menu = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
                        menu.OnLobbyStartButtonClick();
                    }
                    break;

                case "Devour":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 150, 30), "TP to Azazel"))
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

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 150, 30), "Despawn Demons"))
                    {
                        Hacks.Misc.DespawnDemons();
                    }
                    break;
                case "Molly":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 150, 30), "TP to Azazel"))
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

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 150, 30), "Despawn Inmates"))
                    {
                        Hacks.Misc.DespawnDemons();
                    }
                    break;
                case "Inn":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 150, 30), "TP to Azazel"))
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

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 150, 30), "Clean The Fountains"))
                    {
                        Hacks.Misc.CleanFountain();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 150, 150, 30), "Despawn Spiders"))
                    {
                        Hacks.Misc.DespawnSpiders();
                    }
                    break;

                case "Town":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 150, 30), "TP to Azazel"))
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

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 150, 30), "Despawn Ghosts"))
                    {
                        Hacks.Misc.DespawnGhosts();
                    }
                    break;
            }

            // load map
            GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 210, 100, 30), "Load Map: ");

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 240, 100, 30), "Farmhouse") && BoltNetwork.IsServer)
            {
                Helpers.Map.LoadMap("Devour");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 240, 100, 30), "Asylum") && BoltNetwork.IsServer)
            {
                Helpers.Map.LoadMap("Molly");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 230, Settings.Settings.y + 240, 100, 30), "Inn") && BoltNetwork.IsServer)
            {
                Helpers.Map.LoadMap("Inn");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 340, Settings.Settings.y + 240, 100, 30), "Town") && BoltNetwork.IsServer)
            {
                Helpers.Map.LoadMap("Town");
            }

            
        }

        private static void EspTab()
        {
            player_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 20), player_esp, "Player ESP");
            player_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 20), player_snapline, "Player Snapline");

            azazel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 140, 150, 20), azazel_esp, "Azazel ESP");
            azazel_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 170, 150, 20), azazel_snapline, "Azazel Snapline");

            item_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 210, 150, 20), item_esp, "Item ESP");
            goat_rat_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 240, 150, 20), goat_rat_esp, "Goat/Rat ESP");
            demon_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 270, 150, 20), demon_esp, "Demon ESP");
        }

        private static void ItemsTab()
        {
            GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 120, 30), "Items");

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 80, 25), "Hay"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalHay, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalHay");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 100, Settings.Settings.y + 100, 80, 25), "First aid"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalFirstAid, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalFirstAid");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 100, 80, 25), "Battery"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalBattery, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalBattery");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 280, Settings.Settings.y + 100, 80, 25), "Gasoline"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalGasoline, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalGasoline");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 370, Settings.Settings.y + 100, 80, 25), "Fuse"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalFuse, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalFuse");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 460, Settings.Settings.y + 100, 80, 25), "Food"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalRottenFood, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalRottenFood");
                }
            }
            
            /*if (GUI.Button(new Rect(Settings.Settings.x + 550, Settings.Settings.y + 100, 80, 25), "Bone"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalBone, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalBone");
                }
            }*/

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 140, 80, 25), "Bleach"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalBleach, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalBleach");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 100, Settings.Settings.y + 140, 80, 25), "Ritual Book"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalRitualBook, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("RitualBook-Active-1");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 140, 80, 25), "Matchbox"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalMatchbox, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("Matchbox-3");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 180, 80, 25), "Egg-1"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-1");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 100, Settings.Settings.y + 180, 80, 25), "Egg-2"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-2");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 180, 80, 25), "Egg-3"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-3");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 280, Settings.Settings.y + 180, 80, 25), "Egg-4"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-4");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 370, Settings.Settings.y + 180, 80, 25), "Egg-5"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-5");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 460, Settings.Settings.y + 180, 80, 25), "Egg-6"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-6");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 550, Settings.Settings.y + 180, 80, 25), "Egg-7"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-7");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 210, 80, 25), "Egg-8"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-8");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 100, Settings.Settings.y + 210, 80, 25), "Egg-9"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-9");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 210, 80, 25), "Egg-10"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-10");
            }
        }

        private static void MiscTab()
        {
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 30), "Unlock Achievements"))
            {
                Thread AchievementsThread = new Thread(new ThreadStart(Hacks.Unlock.Achievements));
                AchievementsThread.Start();

                MelonLogger.Msg("Achievements Unlocked!");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110, 150, 30), "Unlock Doors"))
            {
                Hacks.Unlock.Doors();

                MelonLogger.Msg("Doors Unlocked!");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 150, 150, 30), "TP Keys") && Player.IsInGame())
            {
                Hacks.Misc.TPKeys();
                MelonLogger.Msg("Here are your keys!");
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 150, 30), "Make Random Noise"))
            {
                Hacks.Misc.PlaySound();
                MelonLogger.Msg("Playing a random sound!");
            }

            spam_message = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 240, 140, 30), spam_message, "Chat spam");
            change_steam_name = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 270, 140, 30), change_steam_name, "Change Steam Name");
            change_server_name = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 300, 140, 30), change_server_name, "Change Server Name");
            _walkInLobby = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 330, 140, 30), _walkInLobby, "Walk In Lobby");
            _IsAutoRespawn = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 360, 140, 30), _IsAutoRespawn, "Auto Respawn");

            fly = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 400, 40, 20), fly, "Fly");
            if (GUI.Button(new Rect(Settings.Settings.x + 60, Settings.Settings.y + 400, 40, 20), Settings.Settings.flyKey.ToString()))
            {
                Settings.Settings.flyKey = Settings.Settings.GetKey();
            }

            fly_speed = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 100, 10), fly_speed, 5f, 20f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 425, 100, 30), ((int)fly_speed).ToString());


            spoofLevel = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 470, 150, 20), spoofLevel, "Spoof Level");
            spoofLevelValue = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 500, 100, 10), spoofLevelValue, 0f, 666f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 495, 100, 30), ((int)spoofLevelValue).ToString());


            exp_modifier = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 540, 150, 20), exp_modifier, "EXP Modifier");
            exp = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 570, 100, 10), exp, 1000f, 3000f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 565, 100, 30), ((int)exp).ToString());


            fastMove = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 610, 150, 20), fastMove, "Player Speed");
            _PlayerSpeedMultiplier = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 640, 100, 10), _PlayerSpeedMultiplier, (int)1f, (int)10f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 635, 100, 30), ((int)_PlayerSpeedMultiplier).ToString());
        }

        private static void PlayersTab()
        {
            if (Helpers.Map.GetActiveScene() != "Menu")
            {
                GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 30), "Player list:");
                int i = 0;
                foreach (BasePlayer bp in Entities.Players)
                {
                    if (bp == null || bp.Name == "")
                    {
                        continue;
                    }

                    GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110 + i, 150, 30), bp.Name);

                    if (GUI.Button(new Rect(Settings.Settings.x + 70, Settings.Settings.y + 105 + i, 60, 30), "Kill"))
                    {
                        bp.Kill();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 140, Settings.Settings.y + 105 + i, 60, 30), "Revive"))
                    {
                        bp.Revive();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 210, Settings.Settings.y + 105 + i, 90, 30), "Jumpscare"))
                    {
                        bp.Jumpscare();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 310, Settings.Settings.y + 105 + i, 60, 30), "TP to"))
                    {
                        bp.TP();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 380, Settings.Settings.y + 105 + i, 100, 30), "Lock in cage"))
                    {
                        bp.LockInCage();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 490, Settings.Settings.y + 105 + i, 90, 30), "TP Azazel"))
                    {
                        bp.TPAzazel();
                    }

                    i += 30;
                }
            }
            else
            {
                GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 30), "---------");
            }
        }
    }
}
