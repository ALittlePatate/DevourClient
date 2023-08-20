/*
Note about license. As stated in the GPL FAQ :
You are allowed to sell copies of the modified program commercially, but only under the terms of the GNU GPL.
Thus, for instance, you must make the source code available to the users of the program as described in the GPL,
and they must be allowed to redistribute and modify it as described in the GPL.

If you decide to modify and then sell this software you have to agree with the GPL 3 license thus making the source code available.
*/

using UnityEngine;
using Il2CppInterop.Runtime.Injection;

[assembly: MelonLoader.VerifyLoaderVersion(0, 6, 0, true)] //Minimum MelonLoader version is V6.0.0, sanity check for people who use 5.7 and wonder why it crashes :)
[assembly: MelonLoader.MelonInfo(typeof(DevourClient.Load), "DevourClient", "2", "ALittlePatate & Jadis0x")]
[assembly: MelonLoader.MelonGame("Straight Back Games", "DEVOUR")]

namespace DevourClient
{
    public class Load : MelonLoader.MelonMod
    {
        public static ClientMain ClientMainInstance { get; private set; }
        public static GameObject DevourClientGO { get; private set; }
        public static void Init()
        {
            ClassInjector.RegisterTypeInIl2Cpp<ClientMain>();

            DevourClientGO = new GameObject("DevourClient");
            UnityEngine.Object.DontDestroyOnLoad(DevourClientGO);
            DevourClientGO.hideFlags |= HideFlags.HideAndDontSave;

            ClientMainInstance = DevourClientGO.AddComponent<ClientMain>();
        }

        public override void OnInitializeMelon()
        {
            Init();
        }
    }
}
