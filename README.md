# Unknowncheats thread [here](https://www.unknowncheats.me/forum/other-fps-games/475950-devour-multihack-update.html)

# DevourClient

I wasn't home for 2 days so i made that, i learnt a lot about C# programming and about Unity IL2CPP game hacking.
DevourClient is a rather uncommon cheat for Devour, i've seen multiple cheats for this game but they all had some boring features such as ESP and stuff.
This cheat hasn't many features and some of them may broke with the time, but hey, spaming the chat with "Deez Nutz" is funny.
Also don't mind french comments lol, google translate is your friend !

## Detection rate

Well at this point i don't really know, i think there is some sort of native Unity anti cheat template but it doesn't seem active. You're fine, no anti cheat !

## Menu
![menu screenshot](Screenshots/menu.png)

## Features
Everything about spoofing ehre (steam name, server name, level...) will persist if you don't uncheck it (it will be reseted when you'll restart the game obv).
* An IMGUI menu thanks to UnityEngine
* Fully compatible with the new IL2CPP version of the game
* Detects if you're in game (with bad code lol), so no chances of crashing on main menu by activating features
* Detects the map you are playing on (useful for the instant win)
* Big Flashlight (allows your flashlight to light a lot more)
* Flashlight color customization (with a home made color picker)
* Unlimited UV light (thanks to [@jadis0x](https://github.com/jadis0x))
* A chat spammer for Lobby and InGame chat (i couldn't do a text entry because of the limitations of [Il2CppAssemblyUnhollower](https://github.com/knah/Il2CppAssemblyUnhollower))
* Achievements unlocker (couldn't do all of them, my code is crashing for some reasons at some point, i may fix it, for now it's commented out)
* Doors unlocker (should work fine, though it doesn't seem to work sometimes)
* Keys teleporter
* LV spoofer
* Steam name spoofer (sets it to "patate", again no input text :/)
* Server name spoofer (sets it to "patate on top !", again no input text :/)
* Fly
* Unlock all, including flashlights, perks, outfits. Active by default, can't be turned off, no persistance.
* Instant Win (allows you to win instantaniously on any map, works in singleplayer, but not as a client. May be working as host)
* Random Sound (make your character play a random acting sound)
* Always carrying a medkit
* Change your exp at the end of the game, changing it is permanant !
* Player ESP (with a home made color picker)
* Player skeleton ESP
* Player snaplines (with a home made color picker)
* Azazel ESP (with a home made color picker)
* Azazel Skeleton ESP
* Azazel snapline (with a home made color picker)
* Item ESP
* Demon ESP
* Goat/Rat ESP
* TP all the items to your position ! (thanks to [@jadis0x](https://github.com/jadis0x))
* Spawn any item/entity to your position
* Walk in the lobby
* Change the player's speed
* Fullbright
* Create a lobby with no player limit

## Installation
For my french fellas out there, 1tap2times made a French video tutorial for the installation of the Mod : [link](https://vimeo.com/789315436)<br>
In order to get all of this working you need to generate the DevourClient.dll file by building the source code.

0. Install [.NET 6 SDK and runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
1. [Build the cheat from source](https://github.com/ALittlePatate/DevourClient#building-from-source).
2. Put the DevourClient.dll file located in `DevourClient\bin\Release\net6.0` inside `C:\Program Files (x86)\Steam\steamapps\common\Devour\Mods` folder.
3. Start the game, now you have successfully installed DevourClient. Use INSERT to open the menu

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
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\net6\MelonLoader.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\net6\0Harmony.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\net6\Il2CppInterop.Runtime.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Assembly-CSharp.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2CppOpsive.UltimateCharacterController.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2CppBehaviorDesigner.Runtime.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2Cppbolt.user.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2Cppbolt.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2Cppmscorlib.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.IMGUIModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.InputLegacyModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.HotReloadModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.UI.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.UIModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.CoreModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.InputModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2Cppudpkit.common.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2Cppudpkit.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\Il2Cppudpkit.platform.photon.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.AnimationModule.dll`
* `C:\Program Files (x86)\Steam\steamapps\common\Devour\MelonLoader\Il2CppAssemblies\UnityEngine.PhysicsModule.dll`
7. Build the solutions in Release | Any CPU

## Contact

You can add me on discord at patate#1252 or on the [discord server](https://discord.gg/2amMFvqjYd)

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

Game's last update before il2cpp :
* https://steamdb.info/depot/1274571/history/?changeid=M:1960656865974212833

## Contributing

Open an [issue](https://github.com/ALittlePatate/DevourClient/issues/new) or make a [pull request](https://github.com/ALittlePatate/DevourClient/pulls), i'll be glad to improve my project with you !

## License

[GPL 3.0](https://www.gnu.org/licenses/gpl-3.0.md)
