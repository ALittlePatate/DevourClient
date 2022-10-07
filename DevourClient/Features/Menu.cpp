#include "Menu.hpp"
#include "../Utils/Output/Output.hpp"
#include "../dllmain.hpp"
#include "../Utils/Settings/Settings.hpp"
#include "../Utils/Dumper/Dumper.hpp"
#include "Misc/Misc.hpp"

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

	ImGui::Checkbox("Demon ESP", &settings::demon_esp);
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
	if (ImGui::Button("Spawn##a")) {
		//call spawn function
	}

	ImGui::Spacing();
	ImGui::Text("Items");
	const char* items_items[] = { "Hay", "First aid", "Battery", "Gasoline", "Fuse", "Food", "Bleach", "Ritual Book (inactive)", "Ritual Book (active)", "Matchbox", "Egg-1", "Egg-2", "Egg-3", "Egg-4", "Egg-5", "Egg-6", "Egg-7", "Egg-8", "Egg-9", "Egg-10"};
	static int item_current = 0;
	ImGui::Combo("##i", &item_current, items_items, IM_ARRAYSIZE(items_items));
	if (ImGui::Button("Spawn##i")) {
		//print("--> %s\n",items_items[item_current]);
		Misc::CarryItem(items_items[item_current]);
	}

	ImGui::Spacing();
	ImGui::Text("Animals");
	const char* animals_items[] = { "Rat", "Goat", "Spider"};
	static int animal_current = 0;
	ImGui::Combo("##an", &animal_current, animals_items, IM_ARRAYSIZE(animals_items));
	if (ImGui::Button("Spawn##an")) {
		Misc::SpawnAnimal(animals_items[animal_current]);
	}
}

void DrawMapSpecificTab() {
	if (ImGui::Button("Instant win")) {
		//call instant win
	}
	
	if (ImGui::Button("Burn a ritual object")) {
		Misc::BurnRitualObj(false);
	}
	
	if (ImGui::Button("Burn all ritual objects")) {
		Misc::BurnRitualObj(true);
	}

	if (ImGui::Button("Force start game")) {
		Misc::ForceStart();
	}
	
	if (ImGui::Button("Kill all players")) {
		Misc::KnockoutPlayers(false);
	}
}

bool inspector = false;
void DrawInspector() {
	ImGui::SetNextWindowSize(ImVec2(600.000f, 600.000f), ImGuiCond_Once);
	if (!ImGui::Begin("Inspector", &inspector, 2)) {
		ImGui::End();
		return;
	}

	static std::vector<std::string> components;
	static std::vector<std::string> classes;
	static std::vector<std::string> methods;
	static std::string current_comp = "";

	ImGui::Text("Components");
	if (ImGui::Button("Update##comp")) {
		components = Dumper::DumpComponentsString();
	}

	ImGui::SetNextItemWidth(150.000f);
	static int component_current_idx = 0; // Here we store our selection data as an index.
	static ImGuiTextFilter c_filter;
	c_filter.Draw("Search##compfilter");
	if (ImGui::BeginListBox("##Components", ImVec2(-FLT_MIN, 5 * ImGui::GetTextLineHeightWithSpacing())))
	{
		for (size_t n = 0; n < components.size(); n++)
		{
			if (!c_filter.PassFilter(components[n].c_str())) {
				continue;
			}
			const bool comp_is_selected = (component_current_idx == (int)n);
			if (ImGui::Selectable(components[n].c_str(), comp_is_selected))
				component_current_idx = (int)n;

			// Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
			if (comp_is_selected)
				ImGui::SetItemDefaultFocus();
		}
		ImGui::EndListBox();
	}

	ImGui::Spacing();
	ImGui::Text("Classes");
	if (ImGui::Button("Update##class")) {
		classes = Dumper::DumpClassesString(components[component_current_idx]);
		current_comp = components[component_current_idx];
	}

	ImGui::SetNextItemWidth(150.000f);
	static int class_current_idx = 0; // Here we store our selection data as an index.
	static ImGuiTextFilter cl_filter;
	cl_filter.Draw("Search##classfilter");
	if (ImGui::BeginListBox("##Class", ImVec2(-FLT_MIN, 5 * ImGui::GetTextLineHeightWithSpacing())))
	{
		for (size_t n = 0; n < classes.size(); n++)
		{
			if (!cl_filter.PassFilter(classes[n].c_str())) {
				continue;
			}
			const bool class_is_selected = (class_current_idx == (int)n);
			if (ImGui::Selectable(classes[n].c_str(), class_is_selected)) {
				class_current_idx = (int)n;
			}

			// Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
			if (class_is_selected)
				ImGui::SetItemDefaultFocus();
		}
		ImGui::EndListBox();
	}

	ImGui::Spacing();
	ImGui::Text("Methods");
	if (ImGui::Button("Update##Methods")) {
		methods = Dumper::DumpMethodsString(current_comp, classes[class_current_idx]);
	}

	ImGui::SetNextItemWidth(150.000f);
	static int method_current_idx = 0; // Here we store our selection data as an index.
	static ImGuiTextFilter me_filter;
	me_filter.Draw("Search##methodfilter");
	if (ImGui::BeginListBox("##Methods", ImVec2(-FLT_MIN, -1)))
	{
		for (size_t n = 0; n < methods.size(); n++)
		{
			if (!me_filter.PassFilter(methods[n].c_str())) {
				continue;
			}
			const bool meth_is_selected = (method_current_idx == (int)n);
			if (ImGui::Selectable(methods[n].c_str(), meth_is_selected))
				method_current_idx = (int)n;

			// Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
			if (meth_is_selected)
				ImGui::SetItemDefaultFocus();
		}
		ImGui::EndListBox();
	}

	ImGui::End();
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
	ImGui::InputText("New name##steam", &settings::new_name);

	ImGui::Checkbox("Change server name", &settings::server_name_spoof);
	ImGui::InputText("New name##server", &settings::server_name);

	ImGui::Checkbox("Fly", &settings::fly);
	
	if (ImGui::Button("Make random noise")) {
		Misc::PlayRandomSound();
	}

	ImGui::Checkbox("EXP modifier", &settings::exp_modifier);
	ImGui::SliderInt("Amount", &settings::new_exp, 0, 5000);

	ImGui::Checkbox("Walk in lobby", &settings::walk_in_lobby);
	
	ImGui::Checkbox("Auto respawn", &settings::auto_respawn);
	
	ImGui::Checkbox("Change player speed", &settings::change_player_speed);
	ImGui::SliderInt("Multiplier", &settings::new_speed, 0, 10);

#if _DEBUG
	ImGui::Spacing();
	if (ImGui::Button("Inspector")) {
		inspector = !inspector;
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

#if _DEBUG
	if (inspector) {
		DrawInspector();
	}
#endif

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
