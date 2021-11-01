using UnityEngine;

namespace DevourClient.Settings
{
    public class Settings
    {
        public static bool menu_enable = false;
        public static float width = Screen.width / 2f;
        public static float height = Screen.height / 2f;
        public static float x = 0;
        public static float y = 0;
        public static Color flashlight_color = new Color(1.00f, 1.00f, 1.00f, 1);
        public static float speed = 1f;
        public const string message_to_spam = "Deez Nutz";
    }
}
