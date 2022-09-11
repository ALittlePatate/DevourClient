﻿namespace DevourClient.Helpers
{
    class Map
    {
        public static string GetActiveScene()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        public static string GetMapName(string sceneName)
        {
            switch (sceneName)
            {
                case "Devour":
                    return "Farmhouse";
                case "Molly":
                    return "Asylum";
                case "Inn":
                    return "Inn";
                case "Town":
                    return "Town";
                default:
                    return "Menu";
            }
        }
        
        public static UnityEngine.GameObject GetAzazel()
        {
            return Helpers.Entities.Azazels[0].gameObject;
        }
    }
}
