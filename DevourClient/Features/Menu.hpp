#pragma once
#include <Windows.h>
#pragma warning(push, 0)
#include "imgui/imgui.h"
#include "imgui/imgui_impl_win32.h"
#include "imgui/imgui_impl_dx11.h"
#include "imgui/imgui_stdlib.h"
#pragma warning(pop)

enum class tabs {
	VISUALS,
	ENTITIES,
	MAP_SPECIFIC,
	MISC
};

void InitStyle();
void DrawMenu(bool open_menu);