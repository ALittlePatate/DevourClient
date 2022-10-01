#include "Menu.hpp"
#include "../Utils/Output/Output.hpp"
#include "../dllmain.hpp"
#include "../Utils/Settings/Settings.hpp"
#include "../Utils/Dumper/Dumper.hpp"

#include <string>

void InitStyle()
{
	ImGui::StyleColorsDark();

	auto& Style = ImGui::GetStyle();

	Style.WindowRounding = 8.000f;
	Style.FrameRounding = 4.000f;

	Style.Colors[ImGuiCol_WindowBg] = ImColor(0, 0, 0, 240);
	Style.Colors[ImGuiCol_TitleBg] = ImColor(1, 1, 1, 240);
	Style.Colors[ImGuiCol_TitleBgActive] = ImColor(1, 1, 1, 240);
	Style.Colors[ImGuiCol_TitleBgCollapsed] = ImColor(0, 0, 0, 240);
	Style.Colors[ImGuiCol_Button] = ImColor(46, 53, 62, 20);
	Style.Colors[ImGuiCol_ButtonHovered] = ImColor(46, 53, 62, 255);
	Style.Colors[ImGuiCol_ButtonActive] = ImColor(46, 53, 62, 255);
	Style.Colors[ImGuiCol_CheckMark] = ImColor(183, 185, 189, 255);
	Style.Colors[ImGuiCol_FrameBg] = ImColor(60, 66, 76, 138);
	Style.Colors[ImGuiCol_FrameBgActive] = ImColor(60, 66, 76, 171);
	Style.Colors[ImGuiCol_FrameBgHovered] = ImColor(60, 66, 76, 102);
	Style.Alpha = 0.8f;

	ImGuiIO& io = ImGui::GetIO();
	io.Fonts->AddFontFromFileTTF("C:\\Windows\\Fonts\\tahoma.ttf", 13.000f);
}

void DrawVisualsTab() {
	ImGui::Checkbox("Big flashlight", &settings::big_flashlight);

	bool open_flcolor_popup = ImGui::ColorButton("flashlightcolor", ImVec4(settings::flashlight_color[0], settings::flashlight_color[1], settings::flashlight_color[2], settings::flashlight_color[3]));
	if (open_flcolor_popup)
	{
		ImGui::OpenPopup("flashlightcolorpicker");
	}
	if (ImGui::BeginPopup("flashlightcolorpicker")) {
		ImGui::ColorPicker4("Flashlight color", (float*)&settings::flashlight_color);
		ImGui::EndPopup();
	}
	ImGui::SameLine();
	ImGui::Text("Flashlight color");

	ImGui::Checkbox("Unlimited UV", &settings::unlimited_uv);
	ImGui::Checkbox("Fullbright", &settings::fullbright);

	ImGui::Checkbox("Player ESP", &settings::player_esp);
	ImGui::SameLine();
	bool open_pelcolor_popup = ImGui::ColorButton("playerespcolor", ImVec4(settings::player_esp_color[0], settings::player_esp_color[1], settings::player_esp_color[2], settings::player_esp_color[3]));
	if (open_pelcolor_popup)
	{
		ImGui::OpenPopup("playeresppop");
	}
	if (ImGui::BeginPopup("playeresppop")) {
		ImGui::ColorPicker4("Player ESP color", (float*)&settings::player_esp_color);
		ImGui::EndPopup();
	}

	ImGui::Checkbox("Player snaplines", &settings::player_snaplines);
	ImGui::SameLine();
	bool open_pslcolor_popup = ImGui::ColorButton("playersncolor", ImVec4(settings::player_snaplines_color[0], settings::player_snaplines_color[1], settings::player_snaplines_color[2], settings::player_snaplines_color[3]));
	if (open_pslcolor_popup)
	{
		ImGui::OpenPopup("playersnpop");
	}
	if (ImGui::BeginPopup("playersnpop")) {
		ImGui::ColorPicker4("Player snaplines color", (float*)&settings::flashlight_color);
		ImGui::EndPopup();
	}

	ImGui::Checkbox("Azazel ESP", &settings::azazel_esp);
	ImGui::SameLine();
	bool open_azacolor_popup = ImGui::ColorButton("azaespcolor", ImVec4(settings::azazel_esp_color[0], settings::azazel_esp_color[1], settings::azazel_esp_color[2], settings::azazel_esp_color[3]));
	if (open_azacolor_popup)
	{
		ImGui::OpenPopup("azaesppop");
	}
	if (ImGui::BeginPopup("azaesppop")) {
		ImGui::ColorPicker4("Azazel ESP color", (float*)&settings::azazel_esp_color);
		ImGui::EndPopup();
	}

	ImGui::Checkbox("Azazel snaplines", &settings::azazel_snaplines);
	ImGui::SameLine();
	bool open_azascolor_popup = ImGui::ColorButton("azasncolor", ImVec4(settings::azazel_snaplines_color[0], settings::azazel_snaplines_color[1], settings::azazel_snaplines_color[2], settings::azazel_snaplines_color[3]));
	if (open_azascolor_popup)
	{
		ImGui::OpenPopup("azasnpop");
	}
	if (ImGui::BeginPopup("azasnpop")) {
		ImGui::ColorPicker4("Azazel snaplines color", (float*)&settings::azazel_snaplines_color);
		ImGui::EndPopup();
	}

	ImGui::Checkbox("Item ESP", &settings::item_esp);
	ImGui::SameLine();
	bool open_icolor_popup = ImGui::ColorButton("iespcolor", ImVec4(settings::item_esp_color[0], settings::item_esp_color[1], settings::item_esp_color[2], settings::item_esp_color[3]));
	if (open_icolor_popup)
	{
		ImGui::OpenPopup("iesppop");
	}
	if (ImGui::BeginPopup("iesppop")) {
		ImGui::ColorPicker4("Item ESP color", (float*)&settings::item_esp_color);
		ImGui::EndPopup();
	}

	ImGui::Checkbox("Demon ESP", &settings::item_esp);
	ImGui::SameLine();
	bool open_dcolor_popup = ImGui::ColorButton("despcolor", ImVec4(settings::demon_esp_color[0], settings::demon_esp_color[1], settings::demon_esp_color[2], settings::demon_esp_color[3]));
	if (open_dcolor_popup)
	{
		ImGui::OpenPopup("desppop");
	}
	if (ImGui::BeginPopup("desppop")) {
		ImGui::ColorPicker4("Demon ESP color", (float*)&settings::demon_esp_color);
		ImGui::EndPopup();
	}

	ImGui::Checkbox("Goat/Rat ESP", &settings::goat_esp);
	ImGui::SameLine();
	bool open_gcolor_popup = ImGui::ColorButton("gespcolor", ImVec4(settings::goat_esp_color[0], settings::goat_esp_color[1], settings::goat_esp_color[2], settings::goat_esp_color[3]));
	if (open_gcolor_popup)
	{
		ImGui::OpenPopup("gesppop");
	}
	if (ImGui::BeginPopup("gesppop")) {
		ImGui::ColorPicker4("Goat ESP color", (float*)&settings::goat_esp_color);
		ImGui::EndPopup();
	}
}

void DrawEntitiesTab() {
	if (ImGui::Button("TP items to you")) {
		//call tp items
	}

	ImGui::Spacing();
	ImGui::Text("Azazel & Demons");
	const char* azazel_demons_items[] = { "Sam", "Molly", "Anna", "Zara", "Ghost", "Inmate", "Demon"};
	static int entity_current = 0;
	ImGui::Combo("##a", &entity_current, azazel_demons_items, IM_ARRAYSIZE(azazel_demons_items));
	if (ImGui::Button("Spawn")) {
		//call spawn function
	}

	ImGui::Spacing();
	ImGui::Text("Items");
	const char* items_items[] = { "Hay", "First aid", "Battery", "Gasoline", "Fuse", "Food", "Egg", "Bleach", "Ritual Book", "Matchbox"};
	static int item_current = 0;
	ImGui::Combo("##i", &item_current, items_items, IM_ARRAYSIZE(items_items));
	if (ImGui::Button("Spawn")) {
		//call spawn function
	}

	ImGui::Spacing();
	ImGui::Text("Animals");
	const char* animals_items[] = { "Rat", "Goat", "Spider"};
	static int animal_current = 0;
	ImGui::Combo("##an", &animal_current, animals_items, IM_ARRAYSIZE(animals_items));
	if (ImGui::Button("Spawn")) {
		//call spawn function
	}
}

void DrawMapSpecificTab() {
	if (ImGui::Button("Instant win")) {
		//call instant win
	}
}

void DrawMiscTab() {
	ImGui::Checkbox("Chat spam", &settings::chat_spam);
	ImGui::InputText("Message", &settings::message);

	if (ImGui::Button("Unlock Achievements")) {
		//Unlock Achievements
	}

	if (ImGui::Button("Unlock Doors")) {
		//Unlock Achievements
	}

	ImGui::Checkbox("Unlock all", &settings::unlock_all);

	ImGui::Checkbox("Spoof level", &settings::spoof_level);
	ImGui::InputInt("New level", &settings::new_level);
	//normalize level
	if (settings::new_level > 666) {
		settings::new_level = 666;
	}
	else if (settings::new_level < 0) {
		settings::new_level = 0;
	}

	ImGui::Checkbox("Change Steam name", &settings::steam_name_spoof);
	ImGui::InputText("New name", &settings::new_name);

	ImGui::Checkbox("Change server name", &settings::server_name_spoof);
	ImGui::InputText("New name", &settings::server_name);

	ImGui::Checkbox("Fly", &settings::fly);
	
	if (ImGui::Button("Make random noise")) {
		//call make random noise
	}

	ImGui::Checkbox("EXP modifier", &settings::exp_modifier);
	ImGui::SliderInt("Amount", &settings::new_exp, 0, 5000);

	ImGui::Checkbox("Walk in lobby", &settings::walk_in_lobby);

	ImGui::Checkbox("Change player speed", &settings::change_player_speed);
	ImGui::SliderInt("Multiplier", &settings::new_speed, 0, 10);

#if _DEBUG
	ImGui::Spacing();
	static std::string component = "NolanBehaviour";
	ImGui::InputText("##component", &component);
	if (ImGui::Button("FindObjectsOfType")) {
		Dump(component);
	}
#endif

	ImGui::Spacing();
	if (ImGui::Button("Unhook")) {
		CreateThread(0, 0, EjectThread, 0, 0, 0); //Unhooking
	}
}

tabs current_tab = tabs::VISUALS;
void DrawMenu(bool open_menu) {
	ImGui::SetNextWindowSize(ImVec2(240.000f, 300.000f), ImGuiCond_Once);
	ImGui::Begin("Devour Client", NULL, 2);

	ImGui::SameLine();
	if (ImGui::Button("Visuals")) {//, ImVec2(0.000f, 0.000f))) {
		current_tab = tabs::VISUALS;
	}

	ImGui::SameLine();
	if (ImGui::Button("Entities", ImVec2(0.000f, 0.000f))) {
		current_tab = tabs::ENTITIES;
	}

	ImGui::SameLine();
	if (ImGui::Button("Map specific", ImVec2(0.000f, 0.000f))) {
		current_tab = tabs::MAP_SPECIFIC;
	}

	ImGui::SameLine();
	if (ImGui::Button("Misc", ImVec2(0.000f, 0.000f))) {
		current_tab = tabs::MISC;
	}
	
	ImGui::Separator();

	switch (current_tab) {
		case tabs::VISUALS:
			DrawVisualsTab();
			break;
		case tabs::ENTITIES:
			DrawEntitiesTab();
			break;
		case tabs::MAP_SPECIFIC:
			DrawMapSpecificTab();
			break;
		case tabs::MISC:
			DrawMiscTab();
			break;
		default:
			break;
	}

	ImGui::End();
}