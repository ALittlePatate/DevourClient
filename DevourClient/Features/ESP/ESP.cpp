#include "ESP.hpp"
#include "../../Utils/Settings/Settings.hpp"
#include "../../Utils/Players/Players.hpp"
#include "../../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"

#include "../../Utils/Output/Output.hpp"

void ESP::PlayerESP() {
	IL2CPP::Thread::Attach(IL2CPP::Domain::Get());
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

				std::vector<Unity::il2cppFieldInfo*> fields;
				CameraMain->FetchFields(&fields);

				for (Unity::il2cppFieldInfo* field : fields) {
					print("--> %s\n", field->m_pName);
				}
			}
		}
	}
}