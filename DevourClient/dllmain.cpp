#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
#include <iostream>
#include <vector>

#include "dllmain.hpp"
#include "Utils/Settings/Settings.hpp"
#include "Hooks/Hooks.hpp"
#include "Utils/Output/Output.hpp"
#include "Callbacks/OnUpdate.hpp"
#include "Utils/Players/Players.hpp"
#include "Features/ESP/ESP.hpp"

#include <IL2CPP_Resolver/il2cpp_resolver.hpp>

//Creating a global copy of hModule, used for EjectThread
HMODULE myhModule;
DWORD __stdcall EjectThread(LPVOID lpParameter) {
    Sleep(100);
    CloseConsole();
    DisableHooks();

    IL2CPP::Callback::Uninitialize();
    FreeLibraryAndExitThread(myhModule, 0); //Freeing the module, that's why we needed the myhModule variable
}

DWORD WINAPI Main() {
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

    if (IL2CPP::Initialize(true) && IL2CPP::Thread::Attach(IL2CPP::Domain::Get())) {
        print("[+] Il2Cpp initialized\n");
    }
    else {
        print("[-] Il2Cpp initialize failed, quitting...");
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

    IL2CPP::Callback::Initialize();
    IL2CPP::Callback::OnUpdate::Add(OnUpdate);

    CreateThread(0, 0, (LPTHREAD_START_ROUTINE)Players::GetPlayersThread, 0, 0, 0);
    CreateThread(0, 0, (LPTHREAD_START_ROUTINE)ESP::PlayerESP, 0, 0, 0); //running in a different thread to help performance

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