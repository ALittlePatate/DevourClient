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
        public static Color player_esp_color = new Color(0.00f, 1.00f, 0.00f, 1);
        public static Color azazel_esp_color = new Color(1.00f, 0.00f, 0.00f, 1);
        public static float speed = 1f;
        public const string message_to_spam = "Deez Nutz";
        public static KeyCode flyKey = KeyCode.None;
        public static Vector2 itemsScrollPosition = Vector2.zero;
        public static Vector2 rituelObjectsScrollPosition = Vector2.zero;
        
        public static KeyCode GetKey()
        {
            Thread.Sleep(50); //TOFIX tried using anyKeydown, no success
            foreach (KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vkey))
                {
                    if (vkey != KeyCode.Delete)
                    {
                        return vkey;
                    }
                }
            }

            return KeyCode.None;
        }
    }
}
