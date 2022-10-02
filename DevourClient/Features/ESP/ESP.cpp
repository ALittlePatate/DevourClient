#include "ESP.hpp"
#include "../../Utils/Settings/Settings.hpp"
#include "../../Utils/Players/Players.hpp"
#include "../../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"

#include "../../Utils/Output/Output.hpp"

void ESP::PlayerESP() {
	IL2CPP::Thread::Attach(IL2CPP::Domain::Get());

	ImGui::SetNextWindowPos(ImVec2(0, 0));
	ImGui::SetNextWindowSize(ImVec2(1024, 768));
	ImGui::Begin("PlayerESPOverlay", nullptr, ImGuiWindowFlags_NoTitleBar | ImGuiWindowFlags_NoResize | ImGuiWindowFlags_NoMove | ImGuiWindowFlags_NoScrollbar | ImGuiWindowFlags_NoInputs | ImGuiWindowFlags_NoBackground);

	while (1) {
		if (settings::player_esp || settings::player_snaplines) {
			for (Unity::CGameObject* player : Players::PlayerList) {
				if (!player) {
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

				Unity::CGameObject* Camera = Unity::GameObject::Find("Camera");
				if (!Camera) {
					continue;
				}

				Unity::CComponent* CameraMain = Camera->GetComponent("Camera");
				if (!CameraMain) {
					continue;
				}

				Unity::Vector3 w2s_footpos = CameraMain->CallMethodSafe<Unity::Vector3>("WorldToScreenPoint");
				Unity::Vector3 w2s_headpos = CameraMain->CallMethodSafe<Unity::Vector3>("WorldToScreenPoint");
				
				if (w2s_footpos.z > 0.f)
				{
					auto pDrawList = ImGui::GetWindowDrawList();

					pDrawList->AddRect(ImVec2(10, 10), ImVec2(100, 100), ImColor(255, 0, 0));
					pDrawList->AddText(ImVec2(10, 10), ImColor(255, 0, 0), "test");
					//Render.Render.DrawBoxESP(w2s_footpos, w2s_headpos, settings::player_esp_color, "", settings::player_snaplines, settings::player_esp);
				}
			}
		}
	}
}