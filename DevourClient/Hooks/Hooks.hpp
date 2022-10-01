#pragma once
#include <Windows.h>
#include <MinHook.h>

#pragma warning(push, 0)
#include "imgui/imgui.h"
#include "imgui/imgui_impl_win32.h"
#include "imgui/imgui_impl_dx11.h"
#pragma warning(pop)

bool HookDX11();

void CreateHooks();
bool InitializeHooks();
void DisableHooks();