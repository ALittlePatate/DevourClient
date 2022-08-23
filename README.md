# Unknowncheats thread [here](https://www.unknowncheats.me/forum/other-fps-games/475950-devour-multihack-update.html)

# The Town Update
Now includes an Unlock All !!!
Everything should be 100% fixed and working for the last update of Devour, have fun !

# DevourClient

I wasn't home for 2 days so i made that, i learnt a lot about C# programming and about Unity IL2CPP game hacking.
DevourClient is a rather uncommon cheat for Devour, i've seen multiple cheats for this game but they all had some boring features such as ESP and stuff.
This cheat hasn't many features and some of them may broke with the time, but hey, spaming the chat with "Deez Nutz" is funny.
Also don't mind french comments lol, google translate is your friend !

## Detection rate

Well at this point i don't really know, i think there is some sort of native Unity anti cheat template but it doesn't seem active. You're fine, no anti cheat !

## Features
Everything about spoofing ehre (steam name, server name, level...) will persist if you don't uncheck it (it will be reseted when you'll restart the game obv).
* An IMGUI menu thanks to UnityEngine
* Fully compatible with the new IL2CPP version of the game
* Detects if you're in game (with bad code lol), so no chances of crashing on main menu by activating features
* Detects the map you are playing on (useful for the instant win)
* Big Flashlight (allows your flashlight to light a lot more)
* Flashlight color customization (with a home made color picker)
* A chat spammer for Lobby and InGame chat (i couldn't do a text entry because of the limitations of [Il2CppAssemblyUnhollower](https://github.com/knah/Il2CppAssemblyUnhollower))
* Achievements unlocker (couldn't do all of them, my code is crashing for some reasons at some point, i may fix it, for now it's commented out)
* Doors unlocker (should work fine, though it doesn't seem to work sometimes)
* Keys teleporter
* LV 70 (puts you to the max level of the game !)
* LV 666 (secret level, thanks to the guy who told me it exists lol)
* Steam name spoofer (sets it to "patate", again no input text :/)
* Server name spoofer (sets it to "patate on top !", again no input text :/)
* Fly !! YES ! YOU CAN FLY ! You can also change the speed of it (left shift : down, space : up, up arrow : forward, back arrow : backward, left arrow : left, right arrow : right)
* Unlock all, including flashlights, perks, outfits. Active by default, can't be turned off, no persistance.
* Instant Win (allows you to win instantaniously on any map, works in singleplayer, but not as a client. May be working as host)
* Random Sound (make your character play a random acting sound)
* Always carrying a medkit
* Change your exp at the end of the game, changing it is permanant !

## Installation
In order to get all of this working you need to generate the DevourClient.dll file by building the source code.

0. [Build the cheat from source](https://github.com/ALittlePatate/DevourClient#building-from-source).
1. Put the DevourClient.dll file located in `DevourClient\bin\Release` inside `C:\Program Files (x86)\Steam\steamapps\common\Devour\Mods` folder.
2. Start the game, now you have successfully installed DevourClient. Use INSERT to open the menu

## Uninstallation

0. Delete the folders `MelonLoader`, `Mods`, `Plugins`, `UserData`, and the file `version.dll` from `C:\Program Files (x86)\Steam\steamapps\common\Devour`

## Building from source

0. Clone the repository (or Code -> Download Zip)
1. Install [MelonLoader](https://github.com/LavaGang/MelonLoader/releases) to Devour.
2. Start your game. A cmd should appear, don't close it, MelonLoader is installing and decompiling Devour's game assemblies.
3. Wait for the process to finish, once it's done close the game.
4. Open the solution file (DevourClient.sln) in Visual Studio
5. Go to : Project --> Add a reference --> Browse --> Click on the browse button in the down right corner of the window.
6. Add those files :
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\MelonLoader.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\0Harmony.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\UnityEngine.IMGUIModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\UnityEngine.InputLegacyModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\bolt.user.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\bolt.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\UnityEngine.HotReloadModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\UnityEngine.UI.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\UnityEngine.CoreModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\Il2Cppmscorlib.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\Assembly-CSharp.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\Opsive.UltimateCharacterController.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Managed\UnityENgine.InputModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Dependencies\Il2CppAssemblyGenerator\Il2CppUnhollower\UnhollowerBaseLib.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Dependencies\Il2CppAssemblyGenerator\Il2CppUnhollower\UnhollowerRuntimeLib.dll`
7. Build the solutions in Release | Any CPU mode

## Contact

You can add me on discord at patate#1252

## Code used

For teaching me the basics :
* [A Begginner's Guide To Hacking Unity Games](https://www.unknowncheats.me/wiki/A_Beginner%27s_Guide_To_Hacking_Unity_Games)

For teaching me about the MelonLoader mods API and Il2Cpp specifications :
* [MelonLoader's quickstart documentation](https://melonwiki.xyz/#/modders/quickstart)
* [MelonLoader's Il2Cpp differences chapter in the documentation](https://melonwiki.xyz/#/modders/il2cppdifferences)

For teaching me about the UnityEngine API :
* [Unity User Manual 2020.3 (LTS)](https://docs.unity3d.com/Manual/index.html)

For decompiling and looking in the source code of the game :
* [dnSpy : a .NET debugger and assembly editor](https://github.com/dnSpy/dnSpy)

For teaching me the basics about Devour game hacking, and i pasted the Key TP hack and the non working part of the Achievements Unlocker from it :
* [DevourCheatMonoInjector](https://github.com/Glatrix/DevourCheatMonoInjector)

## Contributing

Open an [issue](https://github.com/ALittlePatate/DevourClient/issues/new) or make a [pull request](https://github.com/ALittlePatate/DevourClient/pulls), i'll be glad to improve my project with you !

## License

[GPL 3.0](https://www.gnu.org/licenses/gpl-3.0.md)
