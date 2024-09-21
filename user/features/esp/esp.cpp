#include "pch-il2cpp.h"

#include <iostream>
#include "ClientHelper.h"
#include "UnityEngine/Engine.hpp"
#include "players/players.h"
#include "helpers.h"
#include "esp.hpp"
#include <magic_enum.hpp>
#include <vector>

static void DrawBox(float x, float y, float w, float h, ImColor color, float thickness)
{
	auto drawlist = ImGui::GetBackgroundDrawList();

	drawlist->AddLine(ImVec2{ x, y }, ImVec2{ x + w, y }, color, thickness);
	drawlist->AddLine(ImVec2{ x, y }, ImVec2{ x, y + h }, color, thickness);
	drawlist->AddLine(ImVec2{ x + w, y }, ImVec2{ x + w, y + h }, color, thickness);
	drawlist->AddLine(ImVec2{ x, y + h }, ImVec2{ x + w, y + h }, color, thickness);
}

static void DrawString(ImVec2 pos, ImColor color, std::string label)
{
	auto drawlist = ImGui::GetBackgroundDrawList();

	drawlist->AddText(pos, color, label.c_str());
}

static void DrawBoxESP(app::GameObject* it, float footOffset, float headOffset, std::string name, ImColor color, ImColor snapcolor, bool snapline = false, bool esp = false, float nameOffset = -0.5f, float widthOffset = 2.0f)
{
	ImGuiIO& io = ImGui::GetIO();
	app::Camera* cam = app::Camera_get_main(nullptr);
	if (!it || cam == nullptr)
		return;

	app::Transform* _transform = Transform::GetTransform(it);
	if (_transform == nullptr)
		return;

	app::Vector3 pos = Transform::GetPosition(_transform);

	app::Vector3 footpos = app::Camera_WorldToScreenPoint_1(cam, app::Vector3{ pos.x, pos.y + footOffset, pos.z }, NULL);
	app::Vector3 headpos = app::Camera_WorldToScreenPoint_1(cam, app::Vector3{ pos.x, pos.y + headOffset, pos.z }, NULL);
	app::Vector3 namepos = app::Camera_WorldToScreenPoint_1(cam, app::Vector3{ pos.x, pos.y + nameOffset, pos.z }, NULL);

	if (esp && footpos.z > 0.0f) {
		float height = (headpos.y - footpos.y);
		float width = height / widthOffset;

		DrawBox(footpos.x - (width / 2), (float)io.DisplaySize.y - footpos.y - height, width, height, color, 2.0f);
		DrawString(ImVec2(namepos.x, (float)io.DisplaySize.y - namepos.y), color, name);
	}

	if (snapline && footpos.z > 0.f) {
		auto drawlist = ImGui::GetBackgroundDrawList();
		drawlist->AddLine(ImVec2(io.DisplaySize.x / 2, io.DisplaySize.y / 2), ImVec2(footpos.x, io.DisplaySize.y - footpos.y), snapcolor, 2.f);
	}
}

void DrawNameESP(app::Vector3 pos, std::string name, ImColor color)
{
	app::Camera* cam = app::Camera_get_main(nullptr);
	ImGuiIO& io = ImGui::GetIO();
	if (cam == nullptr)
		return;

	app::Vector3 vector = app::Camera_WorldToScreenPoint_1(cam, pos, NULL);
	if (vector.z > 0)
	{
		vector.y = io.DisplaySize.y - (vector.y + 1.f);
		auto drawlist = ImGui::GetBackgroundDrawList();

		drawlist->AddRectFilled(ImVec2(vector.x, vector.y), ImVec2(vector.x + 6.f, vector.y + 6.f), color);
		DrawString(ImVec2(vector.x + 8.f, vector.y), color, name);
	}
}

void ComputePositionAndDrawESP(app::Object_1__Array* ents, ImColor color, bool use_prefab = false, std::string name = "") {
	for (int i = 0; i < ents->max_length; i++) {
		app::Object_1* ent = ents->vector[i];
		if (Object::IsNull(ent))
			continue;

		app::Transform* _transform = Transform::GetTransform(ent);
		if (_transform == nullptr)
			continue;

		app::Vector3 pos = Transform::GetPosition(_transform);

		auto field = il2cpp_class_get_field_from_name(ent->klass->_0.castClass, "m_AI");
		if (field != nullptr) {
			app::Survival_AI__Enum type = il2cpp_object_get_field_value(ent, app::Survival_AI__Enum, field);
			name = magic_enum::enum_name(type);
		}

		if (use_prefab) {
			if ((((app::SurvivalInteractable*)ent)->fields.prefabName) == nullptr)
				continue;
			name = il2cppi_to_string(((app::SurvivalInteractable*)ent)->fields.prefabName);
			string_replace(name, "Survival", "");
		}

		DrawNameESP(pos, name, color);
	}
}

// TEMP FIX #60
app::Object_1__Array* ESP::RefreshEntList(app::Object_1__Array* ent, const char* className, const char* classNamespace) {
	if (time_counter < time_refresh) return ent;
	return Object::FindObjectsOfType(className, classNamespace);
}

void ESP::RunAzazelESP() {

	if (time_counter < (time_refresh - 1)) {
		ents_azazel = Object::FindGameObjectsWithTag("Azazel");
	}
	app::GameObject__Array* ents = ents_azazel;


	if (ents == NULL)
		return;

	for (int i = 0; i < ents->max_length; i++) {
		app::GameObject* ent = (app::GameObject*)ents->vector[i];

		if (ent == nullptr)
			continue;

		DrawBoxESP(ent, -0.25, 2.0f, "Azazel", ImColor{ settings::azazel_esp_color[0], settings::azazel_esp_color[1], settings::azazel_esp_color[2], settings::azazel_esp_color[3] },
			ImColor{ settings::azazel_snaplines_color[0], settings::azazel_snaplines_color[1], settings::azazel_snaplines_color[2], settings::azazel_snaplines_color[3] }, settings::azazel_snaplines, settings::azazel_esp);
	}
}

void ESP::RunDemonESP() {
	ImColor col = ImColor{ settings::demon_esp_color[0], settings::demon_esp_color[1], settings::demon_esp_color[2], settings::demon_esp_color[3] };

	std::vector<std::string> demons_c = { "SurvivalDemonBehaviour", "SpiderBehaviour", "GhostBehaviour", "BoarBehaviour", "CorpseBehaviour"};

	// There's might be a better way to do it, but i'm lazy : )
	if (name_demon == "N/A") {
		for (std::string& class_ : demons_c) {
			ents_demon = RefreshEntList(ents_demon, class_.c_str(), "");
			if (ents_demon && ents_demon->max_length > 0) {
				name_demon = class_;
			}
		}		
	}
	
	if (ents_demon == nullptr) return;

	app::Object_1__Array* ents = ents_demon;
	std::string name = name_demon;
	string_replace(name, "Survival", "");
	string_replace(name, "Behaviour", "");
	ComputePositionAndDrawESP(ents, col, false, name);
}

void ESP::RunItemsESP() {
	ImColor col = ImColor{ settings::item_esp_color[0], settings::item_esp_color[1], settings::item_esp_color[2], settings::item_esp_color[3] };

	ents_item = RefreshEntList(ents_item, "SurvivalInteractable");
	if (ents_item != nullptr) {

		app::Object_1__Array* ents = ents_item;


		if (ents != nullptr || !Object::IsNull(ents->vector[0])) {
			ComputePositionAndDrawESP(ents, col, true);
		}
	}

	
}

void ESP::RunKeyESP() {
	ImColor col = ImColor{ settings::key_esp_color[0], settings::key_esp_color[1], settings::key_esp_color[2], settings::key_esp_color[3] };

	app::Object_1__Array* ents = ents_key;

	if (ents_key == nullptr) return;

	if (ents != nullptr || !Object::IsNull(ents->vector[0])) {
		ComputePositionAndDrawESP(ents, col, false, "Key");
	}
}

void ESP::RunGoatsESP() {

	app::Object_1__Array* goats = ESP::ents_goat;

	if (goats == nullptr || Object::IsNull(goats->vector[0]))
		return;

	ComputePositionAndDrawESP(goats, ImColor{ settings::goat_esp_color[0], settings::goat_esp_color[1], settings::goat_esp_color[2], settings::goat_esp_color[3] });
}

void ESP::RunPlayersESP() {
	app::GameObject__Array* players = Players::GetAllPlayers();

	if (players == NULL)
		return;

	for (int i = 0; i < players->max_length; i++) {
		app::GameObject* ent = players->vector[i];

		if (ent == nullptr || ent == Player::GetLocalPlayer())
			continue;

		DrawBoxESP(ent, -0.25, 1.75, "Player", ImColor{ settings::player_esp_color[0], settings::player_esp_color[1], settings::player_esp_color[2], settings::player_esp_color[3] },
			ImColor{ settings::player_snaplines_color[0], settings::player_snaplines_color[1], settings::player_snaplines_color[2], settings::player_snaplines_color[3] }, settings::player_snaplines, settings::player_esp);
	}
}