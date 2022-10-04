#include "ESP.hpp"
#include "../../Utils/Settings/Settings.hpp"
#include "../../Utils/Players/Players.hpp"
#include "../../Utils/Objects/Objects.hpp"
#include "../../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"

#include "../../Utils/Output/Output.hpp"

void ESP::PlayerESP() {
	if (settings::player_esp || settings::player_snaplines) {
		for (Unity::CGameObject* player : Players::PlayerList) {
			if (!player || player == Players::LocalPlayer) {
				continue;
			}

			Unity::Vector3 pivotPos = player->GetComponent("Transform")->GetMemberValue<Unity::Vector3>("position");

			Unity::Vector3 playerFootPos;
			playerFootPos.x = pivotPos.x;
			playerFootPos.z = pivotPos.z;
			playerFootPos.y = pivotPos.y - 2.f; //At the feet

			Unity::Vector3 playerHeadPos;
			playerHeadPos.x = pivotPos.x;
			playerHeadPos.z = pivotPos.z;
			playerHeadPos.y = pivotPos.y + 2.f; //At the head

			Unity::CGameObject* Camera = Unity::GameObject::Find("Main Camera");
			if (!Camera) {
				continue;
			}

			Unity::CComponent* CameraMain = Camera->GetComponent("Camera");
			if (!CameraMain) {
				continue;
			}

			Unity::Vector3 w2s_footpos = CameraMain->CallMethodSafe<Unity::Vector3>("WorldToScreenPoint", playerFootPos);
			Unity::Vector3 w2s_headpos = CameraMain->CallMethodSafe<Unity::Vector3>("WorldToScreenPoint", playerHeadPos);

			auto draw = ImGui::GetBackgroundDrawList();
				

			ImColor box(settings::player_esp_color[0], settings::player_esp_color[1],
				settings::player_esp_color[2], settings::player_esp_color[3]);

 			draw->AddRect(ImVec2(w2s_headpos.x, settings::width - w2s_headpos.y), ImVec2(w2s_footpos.x, settings::width - w2s_footpos.y), box);
		}
	}
}

void ESP::ItemESP() {
	if (settings::item_esp) {
		IL2CPP::Thread::Attach(IL2CPP::Domain::Get());
		for (Unity::CGameObject* object : Objects::ObjectList) {
			if (!object || !object->m_CachedPtr) {
				continue;
			}

			Unity::Vector3 pivotPos = object->GetComponent("Transform")->GetMemberValue<Unity::Vector3>("position");

			Unity::Vector3 playerFootPos;
			playerFootPos.x = pivotPos.x;
			playerFootPos.z = pivotPos.z;
			playerFootPos.y = pivotPos.y - 2.f; //At the feet

			Unity::Vector3 playerHeadPos;
			playerHeadPos.x = pivotPos.x;
			playerHeadPos.z = pivotPos.z;
			playerHeadPos.y = pivotPos.y + 2.f; //At the head

			Unity::CGameObject* Camera = Unity::GameObject::Find("Main Camera");
			if (!Camera) {
				continue;
			}

			Unity::CComponent* CameraMain = Camera->GetComponent("Camera");
			if (!CameraMain) {
				continue;
			}

			Unity::Vector3 w2s_footpos = CameraMain->CallMethodSafe<Unity::Vector3>("WorldToScreenPoint", playerFootPos);
			Unity::Vector3 w2s_headpos = CameraMain->CallMethodSafe<Unity::Vector3>("WorldToScreenPoint", playerHeadPos);

			//imgui (thanks to that 1 dude in pinned for math, not the math id use but idk im debugging lol)
			float h = w2s_headpos.y - w2s_footpos.y;
			float w = h / 4.f;
			float l = w2s_footpos.x - w;
			float r = w2s_footpos.x + w;

			auto draw = ImGui::GetBackgroundDrawList();

			ImColor box(settings::item_esp_color[0], settings::item_esp_color[1],
				settings::item_esp_color[2], settings::item_esp_color[3]);

 			draw->AddRect(ImVec2(l, w2s_headpos.y), ImVec2(r, w2s_footpos.y), box);
		}
	}
}