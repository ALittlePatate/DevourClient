using UnityEngine;

namespace DevourClient.Helpers
{
    class GUIHelper
    {
        private static float R;
        private static float G;
        private static float B;

        public static Color ColorPick(string title, Color color)
        {
            GUI.Label(new Rect(Settings.Settings.x + 195, Settings.Settings.y + 70, 250, 30), title);

            R = GUI.VerticalSlider(new Rect(Settings.Settings.x + 240, Settings.Settings.y + 100, 30, 90), color.r, 0f, 1f);
            G = GUI.VerticalSlider(new Rect(Settings.Settings.x + 270, Settings.Settings.y + 100, 30, 90), color.g, 0f, 1f);
            B = GUI.VerticalSlider(new Rect(Settings.Settings.x + 300, Settings.Settings.y + 100, 30, 90), color.b, 0f, 1f);

            GUI.Label(new Rect(Settings.Settings.x + 240, Settings.Settings.y + 190, 30, 30), "R");
            GUI.Label(new Rect(Settings.Settings.x + 270, Settings.Settings.y + 190, 30, 30), "G");
            GUI.Label(new Rect(Settings.Settings.x + 300, Settings.Settings.y + 190, 30, 30), "B");
            
            color = new Color(R, G, B, 1);

            void DrawPreview(Rect position, Color color_to_draw)
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, color_to_draw);
                texture.Apply();
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = texture;
                GUI.Box(position, GUIContent.none, boxStyle);
            }

            DrawPreview(new Rect(Settings.Settings.x + 195, Settings.Settings.y + 100, 20, 90), color);

            return color;
        }
        
        public static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
        
        public static Texture2D GetCircularTexture(int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    if (Vector2.Distance(new Vector2(x, y), new Vector2(texture.width / 2, texture.height / 2)) <= texture.width / 2)
                    {
                        texture.SetPixel(x, y, Color.white);
                    }
                    else
                    {
                        texture.SetPixel(x, y, Color.clear);
                    }
                }
            }

            texture.Apply();

            return texture;
        }
    }
}
