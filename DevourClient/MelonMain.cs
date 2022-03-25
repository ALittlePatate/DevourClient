using UnityEngine;
using MelonLoader;
using System.Threading;
using DevourClient.Helpers;

namespace DevourClient
{
    public class Load : MelonMod
    {
        bool flashlight_toggle = false;
        bool flashlight_colorpick = false;
        bool level_70 = false;
        bool level_666 = false;
        bool change_server_name = false;
        bool change_steam_name = false;
        bool fly = false;
        float fly_speed = 0.5f;

        bool spam_message = false;

        public override void OnApplicationStart()
        {
            
        }

        public override void OnUpdate()
        {

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                Settings.Settings.menu_enable = !Settings.Settings.menu_enable;
            }

            if (this.flashlight_toggle && Player.IsInGame())
            {
                Hacks.Misc.BigFlashlight(false);
            }
            else if (!this.flashlight_toggle && Player.IsInGame())
            {
                Hacks.Misc.BigFlashlight(true);
            }

            if (this.spam_message)
            {
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

            if (this.fly && Player.IsInGame())
            {
                Hacks.Misc.Fly(this.fly_speed);
            }
        }

        public override void OnGUI()
        {
            if (Settings.Settings.menu_enable) //Si on appuie sur INSERT
            {
                GUI.Label(new Rect(300, Settings.Settings.y, 100, 30), "Devour Client"); //Titre du menu
                this.flashlight_toggle = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 20), this.flashlight_toggle, "Big Flashlight"); //Checkbox Flashlight
                this.spam_message = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 250, 150, 20), this.spam_message, "Chat Spam"); //Checkbox Chat Spam
                this.level_70 = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 150, 20), this.level_70, "Level 70"); //Checkbox lvl 70
                this.level_666 = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 220, 150, 20), this.level_666, "Level 666"); //Checkbox lvl 70
                this.change_server_name = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 40, 150, 20), this.change_server_name, "Change server name"); //Checkbox servername
                this.change_steam_name = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 70, 150, 20), this.change_steam_name, "Change steam name"); //Checkbox servername
                this.fly = GUI.Toggle(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 100, 150, 20), this.fly, "Fly"); //Checkbox fly
                this.fly_speed = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 200, Settings.Settings.y + 130, 100, 10), this.fly_speed, 0f, 1f); //Slider for the fly speed
                GUI.Label(new Rect(Settings.Settings.x + 310, Settings.Settings.y + 125, 100, 30), this.fly_speed.ToString()); //Prints the value of the slider

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

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 280, 150, 20), "Instant WIn") && Player.IsInGame())
                {
                    Hacks.Misc.InstantWin();
                    MelonLogger.Msg("EZ Win");
                }

            }
        }
    }
}
