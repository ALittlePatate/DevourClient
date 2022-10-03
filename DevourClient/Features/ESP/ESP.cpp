#include "ESP.hpp"
#include "../../Utils/Settings/Settings.hpp"
#include "../../Utils/Players/Players.hpp"
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

			Unity::Vector2 w2s_footpos = CameraMain->CallMethodSafe<Unity::Vector2>("WorldToScreenPoint");
			Unity::Vector2 w2s_headpos = CameraMain->CallMethodSafe<Unity::Vector2>("WorldToScreenPoint");

			auto draw = ImGui::GetBackgroundDrawList();
				

			ImColor box(settings::player_esp_color[0], settings::player_esp_color[1],
				settings::player_esp_color[2], settings::player_esp_color[3]);

 			draw->AddRect(ImVec2(w2s_headpos.x, settings::width - w2s_headpos.y), ImVec2(w2s_footpos.x, settings::width - w2s_footpos.y), box);
		}
	}
}