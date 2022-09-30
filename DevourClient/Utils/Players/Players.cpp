#include "Players.hpp"

namespace Players {
    std::vector<Unity::CGameObject*> PlayerList(NULL);
    Unity::CGameObject* LocalPlayer = NULL;
}

void Players::GetPlayersThread() {
    /*
    * Will always lop and get the players + the localplayer
    * This runs in a separate thread to avoid lags because we're fetching the components
    * Used as a "cache" - sorta
    * I use this instead of my good old corroutine
    */

    while (1) {
        PlayerList.clear();

        auto list = Unity::Object::FindObjectsOfType<Unity::CComponent>("PlayerCharacterBehaviour");
        for (int i = 0; i < list->m_uMaxLength + 1; i++)
        {
            if (!list->operator[](i))
                continue;

            Unity::CGameObject* object = list->operator[](i)->GetMemberValue<Unity::CGameObject*>("gameObject"); //SurvivalPlayer(Clone)
            if (object != NULL) {
                PlayerList.push_back(object);
            }

            Unity::CComponent* BoltEntity = object->GetComponent("BoltEntity");
            if (BoltEntity)
            {
                if (BoltEntity->CallMethod<bool>("get_IsOwner"))//local player check
                {
                    LocalPlayer = object;
                }
            }
        }

        Sleep(500);
    }
    
}