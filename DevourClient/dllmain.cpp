#include <pch-il2cpp.h>

#define WIN32_LEAN_AND_MEAN
#include <Windows.h>

#include <il2cpp-init.h>
#include <il2cpp-appdata.h>

#include <iostream>
#include <vector>

#include "dllmain.hpp"
#include "Utils/Settings/Settings.hpp"
#include "Hooks/Hooks.hpp"
#include "Utils/Output/Output.hpp"
#include "Callbacks/OnUpdate.hpp"
#include "Utils/Players/Players.hpp"
#include "Utils/Objects/Objects.hpp"

extern const LPCWSTR LOG_FILE = L"il2cpp-log.txt";

//Creating a global copy of hModule, used for EjectThread
HMODULE myhModule;
DWORD __stdcall EjectThread(LPVOID lpParameter) {
    Sleep(100);
    CloseConsole();
    DisableHooks();

    FreeLibraryAndExitThread(myhModule, 0); //Freeing the module, that's why we needed the myhModule variable
}

DWORD WINAPI Main() {
    il2cpp_thread_attach(il2cpp_domain_get());
    OpenConsole();
    print("[+] Injected !\n");

    if (InitializeHooks()) {
        print("[+] Hooks initialized\n");
    }
    else {
        print("[-] MH_Initialize failed, quitting...");
        Sleep(300);
        CreateThread(0, 0, EjectThread, 0, 0, 0); //Unhooking
        return false;
    }

    if (HookDX11()) {
        print("[+] DirectX11 hooked !\n");
    }
    else {
        print("[-] DirectX11 hook failed, quitting...");
        Sleep(300);
        CreateThread(0, 0, EjectThread, 0, 0, 0); //Unhooking
        return false;
    }

    CreateHooks();
    print("[+] Created hooks\n");

    RECT desktop;
    const HWND hDesktop = GetDesktopWindow();
    GetWindowRect(hDesktop, &desktop);
    settings::height = desktop.right;
    settings::width = desktop.bottom;

    CreateThread(0, 0, (LPTHREAD_START_ROUTINE)Players::GetPlayersThread, 0, 0, 0);
    CreateThread(0, 0, (LPTHREAD_START_ROUTINE)Objects::GetObjectsThread, 0, 0, 0);
    return TRUE;
}


BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        init_il2cpp();
        myhModule = hModule;
        CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)Main, NULL, 0, NULL);
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}