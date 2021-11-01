using UnityEngine;
using MelonLoader;
using UnityEngine.UI;

namespace DevourClient.Hacks
{
    public class Misc
    {
		public static void BigFlashlight(bool reset)
        {
			NolanBehaviour Nolan = UnityEngine.Object.FindObjectOfType<NolanBehaviour>();
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
		public static void FlashlightColor(Color color)
        {
			NolanBehaviour Nolan = UnityEngine.Object.FindObjectOfType<NolanBehaviour>();
			Light flashlightSpot = Nolan.flashlightSpot;

			flashlightSpot.color = color;
		}

		public static void TPKeys()
        {
			NolanBehaviour Nolan = UnityEngine.Object.FindObjectOfType<NolanBehaviour>();

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

		public static void MaxRank()
        {
			NolanRankController NolanRank = UnityEngine.Object.FindObjectOfType<NolanRankController>();

			NolanRank.SetRank(70);
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

		public static void InstantWin()
        {
			Survival survival_class = UnityEngine.Object.FindObjectOfType<Survival>();
			try
            {
				survival_class.PlayWinEnding("InnWin");
			}
			catch
            {
				try
                {
					survival_class.PlayWinEnding("AsylumWin");
				}
				catch
                {
					survival_class.PlayWinEnding("Win");
				}
            }
		}
    }
}
