using UnityEngine;
using MelonLoader;
using System.Threading;
using DevourClient.Helpers;

namespace DevourClient
{
    public class Load : MelonMod
    {
        bool flashlight_toggle = false;
        bool flashlight_reset = false;
        bool flashlight_colorpick = false;

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

            if (this.flashlight_toggle && !flashlight_reset && Player.IsInGame())
            {
                flashlight_reset = true;
                Hacks.Misc.BigFlashlight(false);
                MelonLogger.Msg("Big Flashlight enabled !");
            }
            else if (!this.flashlight_toggle && flashlight_reset && Player.IsInGame())
            {
                flashlight_reset = false;
                Hacks.Misc.BigFlashlight(true);
                MelonLogger.Msg("Big Flashlight disabled !");
            }

            if (this.spam_message)
            {
                Hacks.Misc.MessageSpam(Settings.Settings.message_to_spam);
            }
        }

        public override void OnGUI()
        {
            if (Settings.Settings.menu_enable) //Si on appuie sur INSERT
            {
                GUI.Label(new Rect(300, Settings.Settings.y, 100, 30), "Devour Client"); //Titre du menu
                this.flashlight_toggle = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 20), this.flashlight_toggle, "Big Flashlight"); //Checkbox Flashlight
                this.spam_message = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 220, 150, 20), this.spam_message, "Chat Spam"); //Checkbox Chat Spam

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

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 150, 20), "Rank 70"))
                {
                    Hacks.Misc.MaxRank();
                    MelonLogger.Msg("EZ LV 70");
                }

                if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 250, 150, 20), "Instant WIn") && Player.IsInGame())
                {
                    Hacks.Misc.InstantWin();
                    MelonLogger.Msg("EZ Win");
                }

            }
        }
    }
}
