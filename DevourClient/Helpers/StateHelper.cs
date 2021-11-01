namespace DevourClient.Helpers
{
    public class Player
    {
        public static bool IsInGame()
        {
            //Code pas très beau ici
            //La variable gameStarted dans la classe Horror.Menu n'existe plus quand on lance une game
            //Donc si elle existe on retourne sa valeur (qui est false)
            //Et si elle existe pas (donc si on est ingame) on retourne true
            try
            {
                Horror.Menu menu_class = UnityEngine.ScriptableObject.FindObjectOfType<Horror.Menu>();
                return menu_class.gameStarted;
            }
            catch
            {
                return true;
            }
        }
    }
}
