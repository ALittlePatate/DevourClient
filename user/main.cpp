// Generated C++ file by Il2CppInspector - http://www.djkaty.com - https://github.com/djkaty
// Custom injected code entry point

#include "pch-il2cpp.h"

#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <iostream>
#include "il2cpp-appdata.h"
#include "helpers.h"

#include <chrono>
#include <thread>

#include "Wrapper.h"
#include "ClientHelper.h"
#include "hooks/hooks.hpp"
#include "color.hpp"
#include "UnityCore.h"
#include "features/misc/misc.h"
#include "utils/utils.hpp"

// Set the name of your log file here
extern const LPCWSTR LOG_FILE = L"DevourClient.txt";

HMODULE myhModule = NULL;
bool should_unhook = 0;
DWORD __stdcall EjectThread(LPVOID lpParameter) {
	Sleep(100);
	il2cpp_close_console();
	DisableHooks();
	FreeLibraryAndExitThread(myhModule, 0); //Freeing the module, that's why we needed the myhModule variable
}

// Custom injected code entry point
void Run()
{
	// Initialize thread data - DO NOT REMOVE
	il2cpp_thread_attach(il2cpp_domain_get());


	// If you would like to output to a new console window, use il2cppi_new_console() to open one and redirect stdout
	il2cppi_new_console();

	std::cout << dye::green("\n\tDevourClient v2.0\n\t") << __DATE__ << " - " << __TIME__ << std::endl;
	std::cout << "\tDevour Version ";

	if (app::Application_get_version != nullptr)
		std::cout << dye::light_red("v") << dye::light_red(il2cppi_to_string(app::Application_get_version(NULL))) << "\n\n";
	else
		std::cout << "Unknown\n\n";

	std::cout << dye::light_aqua("\tMade with < 3 by patate and Jadis0x.\n\n");

	uint64_t steamUserID = app::SteamUser_GetSteamID(nullptr).m_SteamID;
	std::string steamName = il2cppi_to_string(app::SteamFriends_GetPersonaName(nullptr));

	std::cout << "[DevourClient]: " << dye::yellow("GitHub: ") << dye::aqua_on_black("https://github.com/ALittlePatate/DevourClient\n");
	std::cout << "[DevourClient]: " << "Note: " << dye::light_red("if you payed for this you most likely got scammed.\n\n");
	std::cout << "[DevourClient]: Logged in as " << dye::yellow(steamName) << " (" << steamUserID << ")\n\n";

	std::cout << "[DevourClient]: " << dye::aqua("Initializing..\n");
	il2cppi_log_write("Initializing..");

	if (InitializeHooks()) {
		il2cppi_log_write("Hooks initialized");
		std::cout << "[DevourClient]: " << dye::aqua("Hooks initialized.\n");
	}
	else {
		il2cppi_log_write("MH_Initialize failed, quitting...");
		std::cout << "[DevourClient]: " << dye::aqua("MH_Initialize failed, quitting...\n");
		CreateThread(0, 0, EjectThread, 0, 0, 0); //Unhooking
		return;
	}

	CreateHooks();

	if (HookDX11()) {
		il2cppi_log_write("DirectX11 hooked");
		std::cout << "[DevourClient]: " << dye::aqua("DirectX11 hooked.\n");
	}
	else {
		il2cppi_log_write("DirectX11 hook failed, quitting...");
		std::cout << "[DevourClient]: " << dye::aqua("DirectX11 hook failed!quitting...\n");
		CreateThread(0, 0, EjectThread, 0, 0, 0); //Unhooking
		return;
	}

	// create settings.ini
	std::string filename = "settings.ini";
	std::string key = "open_menu";
	std::string defaultValue = "INSERT";  // default value

	// null file??
	std::string keyValue = ReadValueFromIni(filename, key, defaultValue);

	std::cout << "[DevourClient]: " << dye::light_green("Done!:)\n\n");

	std::string scene = SceneName();

	if (scene == std::string("Menu")) {
		app::Menu* horrorMenu = UnityCore::Object<app::Menu>::FindObjectOfType("Menu", "Horror");

		if (!horrorMenu)
			return;

		app::String* str = reinterpret_cast<app::String*>(il2cpp_string_new("Welcome to DevourClient.\n\nAfter ensuring the game is in full screen, press the INS key to activate the menu.\n\n\nYou can disable the cheat by pressing the 'END' key."));

		if (str) {
			if (app::Menu_ShowMessageModal == nullptr)
				return;

			app::Menu_ShowMessageModal(horrorMenu, str, nullptr);
		}
	}

	while (true) {
		if (GetAsyncKeyState(VK_END) & 0x8000 || should_unhook)
			break;


		std::this_thread::sleep_for(std::chrono::milliseconds(1000));
	}
	CreateThread(0, 0, EjectThread, 0, 0, 0);
}