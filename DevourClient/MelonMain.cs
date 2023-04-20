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
        public static float lobbySize = 4;
        public static bool _IsAutoRespawn = false;
        public static bool unlimitedUV = false;
        public static bool exp_modifier = false;
        public static float exp = 1000f;
        public static bool _walkInLobby = false;
        static bool player_esp = false;
        static bool player_skel_esp = false;
        static bool player_snapline = false;
        static bool azazel_esp = false;
        static bool azazel_skel_esp = false;
        static bool azazel_snapline = false;
        static bool spam_message = false;
        static bool item_esp = false;
        static bool goat_rat_esp = false;
        static bool demon_esp = false;
        static bool fullbright = false;
        static bool need_fly_reset = false;
        static bool crosshair = false;
        static bool in_game_cache = false;
        static Texture2D crosshairTexture = default!;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("For the Queen !");

            crosshairTexture = Helpers.GUIHelper.GetCircularTexture(5, 5);

            MelonCoroutines.Start(Helpers.Entities.GetLocalPlayer());
            MelonCoroutines.Start(Helpers.Entities.GetGoatsAndRats());
            MelonCoroutines.Start(Helpers.Entities.GetSurvivalInteractables());
            MelonCoroutines.Start(Helpers.Entities.GetKeys());
            MelonCoroutines.Start(Helpers.Entities.GetDemons());
            MelonCoroutines.Start(Helpers.Entities.GetSpiders());
            MelonCoroutines.Start(Helpers.Entities.GetGhosts());
            MelonCoroutines.Start(Helpers.Entities.GetBoars());
            MelonCoroutines.Start(Helpers.Entities.GetCorpses());
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

                if (crosshair && !in_game_cache)
                {
                    in_game_cache = true;
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

                if (crosshair && in_game_cache)
                {
                    in_game_cache = false;
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

            if (Player.IsInGameOrLobby())
            {
                if (fly && !need_fly_reset)
                {
                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();
                    if (nb)
                    {
                        Collider coll = nb.GetComponentInChildren<Collider>();
                        if (coll)
                        {
                            coll.enabled = false;
                            need_fly_reset = true;
                        } 
                    }
                }

                else if (!fly && need_fly_reset)
                {
                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();
                    if (nb)
                    {
                        Collider coll = nb.GetComponentInChildren<Collider>();
                        if (coll)
                        {
                            coll.enabled = true;
                            need_fly_reset = false;
                        }
                    }
                }

                if (fly)
                {
                    Hacks.Misc.Fly(fly_speed);
                }

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
                if (player_esp || player_snapline || player_skel_esp)
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
                            Il2Cpp.NolanBehaviour nb = player.GetComponent<Il2Cpp.NolanBehaviour>();
                            if (nb.entity.IsOwner)
                            {
                                continue;
                            }

                            if (player_skel_esp)
                            {
                                Render.Render.DrawAllBones(Hacks.Misc.GetAllBones(nb.animator), Settings.Settings.player_esp_color);
                            }
                            
                            Render.Render.DrawBoxESP(player, -0.25f, 1.75f, p.Name, Settings.Settings.player_esp_color, player_snapline, player_esp);
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

                    foreach (Il2Cpp.BoarBehaviour boar in Helpers.Entities.Boars)
                    {
                        if (boar != null)
                        {
                            Render.Render.DrawNameESP(boar.transform.position, "Boar", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                        }
                    }

                    foreach (Il2Cpp.CorpseBehaviour corpse in Helpers.Entities.Corpses)
                    {
                        if (corpse != null)
                        {
                            Render.Render.DrawNameESP(corpse.transform.position, "Corpse", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                        }
                    }
                }

                if (azazel_esp || azazel_snapline || azazel_skel_esp)
                {
                    foreach (Il2Cpp.SurvivalAzazelBehaviour survivalAzazel in Helpers.Entities.Azazels)
                    {
                        if (survivalAzazel != null)
                        {
                            if (azazel_skel_esp)
                            {
                                Render.Render.DrawAllBones(Hacks.Misc.GetAllBones(survivalAzazel.animator), Settings.Settings.azazel_esp_color);
                            }
                            
                            Render.Render.DrawBoxESP(survivalAzazel.gameObject, -0.25f, 2.0f, "Azazel", Settings.Settings.azazel_esp_color, azazel_snapline, azazel_esp);
                        }
                    }
                }

                if (crosshair && in_game_cache) //&& !Player.IsPlayerCrawling())
                {
                    const float crosshairSize = 4;

                    float xMin = (Settings.Settings.width) - (crosshairSize / 2);
                    float yMin = (Settings.Settings.height) - (crosshairSize / 2);

                    if (crosshairTexture == null)
                    {
                        crosshairTexture = Helpers.GUIHelper.GetCircularTexture(5, 5);
                    }

                    GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize, crosshairSize), crosshairTexture);
                }
            }

            if (Settings.Settings.menu_enable) //Si on appuie sur INSERT
            {
                windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)Tabs, "DevourClient"); 
            }   
        }
        
        public static void Tabs(int windowID)
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Visual") || Input.GetKeyDown(KeyCode.F1))
            {
                current_tab = CurrentTab.Visuals;
            }
            
            if (GUILayout.Button("Entites") || Input.GetKeyDown(KeyCode.F2))
            {
                current_tab = CurrentTab.Entities;
            }
            
            if (GUILayout.Button("Map Specific") || Input.GetKeyDown(KeyCode.F3))
            {
                current_tab = CurrentTab.Map;
            }
            
            if (GUILayout.Button("ESP") || Input.GetKeyDown(KeyCode.F4))
            {
                current_tab = CurrentTab.ESP;
            }
            
            if (GUILayout.Button("Items") || Input.GetKeyDown(KeyCode.F5))
            {
                current_tab = CurrentTab.Items;
            }
            
            if (GUILayout.Button("Misc") || Input.GetKeyDown(KeyCode.F6))
            {
                current_tab = CurrentTab.Misc;
            }

            if (GUILayout.Button("Player") || Input.GetKeyDown(KeyCode.F7))
            {
                current_tab = CurrentTab.Players;
            }

            GUILayout.EndHorizontal();
            
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
            crosshair = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 160, 130, 30), crosshair, "Crosshair");


            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 130, 30), "Flashlight Color"))
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
            
            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 180, 60, 25), "Nathan") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.AzazelNathan, Player.GetPlayer().transform.position, Quaternion.identity);
            }

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
            
            if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 220, 60, 25), "Boar") && Player.IsInGameOrLobby() && BoltNetwork.IsServer)
            {
                BoltNetwork.Instantiate(BoltPrefabs.Boar, Player.GetPlayer().transform.position, Quaternion.identity);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 220, 60, 25), "Corpse") && BoltNetwork.IsServer && Player.IsInGameOrLobby())
            {
                BoltNetwork.Instantiate(BoltPrefabs.Corpse, Player.GetPlayer().transform.position, Quaternion.identity);
            }

            // Animal

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 260, 60, 25), "Rat"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalRat, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (Player.IsInGame() && !Player.IsPlayerCrawling())
                {
                    Hacks.Misc.CarryObject("SurvivalRat");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 80, Settings.Settings.y + 260, 60, 25), "Goat"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalGoat, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (Player.IsInGame() && !Player.IsPlayerCrawling())
                {
                    Hacks.Misc.CarryObject("SurvivalGoat");
                }
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 260, 60, 25), "Spider") && BoltNetwork.IsServer && Player.IsInGameOrLobby())
            {
                BoltNetwork.Instantiate(BoltPrefabs.Spider, Player.GetPlayer().transform.position, Quaternion.identity);
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 260, 60, 25), "Pig"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalPig, Player.GetPlayer().transform.position, Quaternion.identity);
                }

                if (Player.IsInGame() && !Player.IsPlayerCrawling())
                {
                    Hacks.Misc.CarryObject("SurvivalPig");
                }
            }
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

                case "Slaughterhouse":
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

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 150, 30), "Despawn Boars"))
                    {
                        Hacks.Misc.DespawnBoars();
                    }

                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 150, 150, 30), "Despawn Corpses"))
                    {
                        Hacks.Misc.DespawnCorpses();
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

            if (GUI.Button(new Rect(Settings.Settings.x + 450, Settings.Settings.y + 240, 100, 30), "Slaughterhouse") && BoltNetwork.IsServer)
            {
                Helpers.Map.LoadMap("Slaughterhouse");
            }
        }

        private static void EspTab()
        {
            player_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 20), player_esp, "Player ESP");
            player_skel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 20), player_skel_esp, "Skeleton ESP");
            player_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 130, 150, 20), player_snapline, "Player Snapline");
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 160, 130, 30), "Player ESP Color"))
            {
                player_esp_colorpick = !player_esp_colorpick;
            }

            if (player_esp_colorpick)
            {
                Color player_esp_color_input = GUIHelper.ColorPick("Player ESP Color", Settings.Settings.player_esp_color);
                Settings.Settings.player_esp_color = player_esp_color_input;
            }

            azazel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 200, 150, 20), azazel_esp, "Azazel ESP");
            azazel_skel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 230, 150, 20), azazel_skel_esp, "Skeleton ESP");
            azazel_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 260, 150, 20), azazel_snapline, "Azazel Snapline");
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 290, 130, 30), "Azazel ESP Color"))
            {
                azazel_esp_colorpick = !azazel_esp_colorpick;
            }

            if (azazel_esp_colorpick)
            {
                Color azazel_esp_color_input = GUIHelper.ColorPick("Azazel ESP Color", Settings.Settings.azazel_esp_color);
                Settings.Settings.azazel_esp_color = azazel_esp_color_input;
            }

            item_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 330, 150, 20), item_esp, "Item ESP");
            goat_rat_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 360, 150, 20), goat_rat_esp, "Goat/Rat ESP");
            demon_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 390, 150, 20), demon_esp, "Demon ESP");
        }

        private static void ItemsTab()
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();

            GUILayout.Label("Items");

            Settings.Settings.itemsScrollPosition = GUILayout.BeginScrollView(Settings.Settings.itemsScrollPosition, GUILayout.Width(220), GUILayout.Height(190));

            if (GUILayout.Button("Hay"))
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

            if (GUILayout.Button("First aid"))
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

            if (GUILayout.Button("Battery"))
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

            if (GUILayout.Button("Gasoline"))
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

            if (GUILayout.Button("Fuse"))
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

            if (GUILayout.Button("Food"))
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

            if (GUILayout.Button("Bone"))
            {

                if (BoltNetwork.IsServer && !Player.IsInGame())
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalBone, Player.GetPlayer().transform.position, Quaternion.identity);
                }
                else
                {
                    Hacks.Misc.CarryObject("SurvivalBone");
                }
            }

            if (GUILayout.Button("Bleach"))
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

            if (GUILayout.Button("Matchbox"))
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


            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            GUILayout.Label("Rituel Objects");

            Settings.Settings.rituelObjectsScrollPosition = GUILayout.BeginScrollView(Settings.Settings.rituelObjectsScrollPosition, GUILayout.Width(220), GUILayout.Height(190));

            if (GUILayout.Button("Egg-1"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-1");
            }

            if (GUILayout.Button("Egg-2"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-2");
            }

            if (GUILayout.Button("Egg-3"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-3");
            }

            if (GUILayout.Button("Egg-4"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-4");
            }

            if (GUILayout.Button("Egg-5"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-5");
            }

            if (GUILayout.Button("Egg-6"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-6");
            }

            if (GUILayout.Button("Egg-7"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-7");
            }

            if (GUILayout.Button("Egg-8"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-8");
            }

            if (GUILayout.Button("Egg-9"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-9");
            }

            if (GUILayout.Button("Egg-10"))
            {
                Hacks.Misc.CarryObject("Egg-Clean-10");
            }

            if (GUILayout.Button("Ritual Book"))
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

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            
            GUILayout.BeginVertical();

            GUILayout.Label("Spawnable Prefabs");

            Settings.Settings.stuffsScrollPosition = GUILayout.BeginScrollView(Settings.Settings.stuffsScrollPosition, GUILayout.Width(220), GUILayout.Height(190));

            if (GUILayout.Button("Animal_Gate"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Animal_Gate, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("AsylumDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.AsylumDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("AsylumDoubleDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.AsylumDoubleDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("AsylumWhiteDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.AsylumWhiteDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }


            if (GUILayout.Button("DevourDoorBack"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.DevourDoorBack, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("DevourDoorMain"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.DevourDoorMain, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("DevourDoorRoom"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.DevourDoorRoom, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("Elevator_Door"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Elevator_Door, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("InnDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.InnDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("InnDoubleDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.InnDoubleDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("InnShojiDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.InnShojiDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("InnShrine"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.InnShrine, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("InnWardrobe"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.InnWardrobe, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("InnWoodenDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.InnWoodenDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("PigExcrement"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.PigExcrement, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SlaughterhouseFireEscapeDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SlaughterhouseFireEscapeDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalAltarMolly"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalAltarMolly, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalAltarSlaughterhouse"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalAltarSlaughterhouse, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalAltarTown"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalAltarTown, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }


            if (GUILayout.Button("SurvivalCultist"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalCultist, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalKai"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalKai, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalNathan"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalNathan, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalMolly"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalMolly, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalRose"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalRose, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("SurvivalSmashableWindow"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.SurvivalSmashableWindow, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("TownDoor"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.TownDoor, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("TownDoor2"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.TownDoor2, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("TownPentagram"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.TownPentagram, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("TrashCan"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.TrashCan, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("Truck_Shutter"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Truck_Shutter, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }

            if (GUILayout.Button("TV"))
            {
                if (BoltNetwork.IsServer)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.TV, Player.GetPlayer().transform.position, Quaternion.identity);
                }
            }
            
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
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

            GUI.Label(new Rect(Settings.Settings.x + 295, Settings.Settings.y + 70, 150, 30), "Max players");
            lobbySize = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 295, Settings.Settings.y + 90, 100, 10), lobbySize, (int)0f, (int)30f);
            GUI.Label(new Rect(Settings.Settings.x + 405, Settings.Settings.y + 85, 100, 30), ((int)lobbySize).ToString());

            if (GUI.Button(new Rect(Settings.Settings.x + 285, Settings.Settings.y + 110, 150, 30), "Create server"))
            {
                MelonLogger.Msg("Creating the server...");
                Hacks.Misc.CreateCustomizedLobby((int)lobbySize);
                MelonLogger.Msg("Done !");
            }
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
