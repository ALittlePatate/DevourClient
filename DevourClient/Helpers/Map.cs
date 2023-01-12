namespace DevourClient.Helpers
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
        
        public static void LoadMap(string mapName)
        {
            if (Il2CppPhoton.Bolt.BoltNetwork.IsServer)
            {
                Il2CppPhoton.Bolt.BoltNetwork.LoadScene(mapName);
                
                MelonLoader.MelonLogger.Warning("Please press the button only once, it may take some time for the map to load.");
            }
            else
            {
                MelonLoader.MelonLogger.Warning("You must be the host to use this command!");
            }
        }
    }
}
