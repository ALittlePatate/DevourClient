#include "Objects.hpp"
#include "../../Utils/Helpers/Helpers.hpp"

namespace Objects {
    std::vector<Unity::CGameObject*> ObjectList(NULL);
}

void Objects::GetObjectsThread() {
    /*
    * Will always lop and get the objects
    * This runs in a separate thread to avoid lags because we're fetching the components
    * Used as a "cache" - sorta
    * I use this instead of my good old corroutine
    */

    IL2CPP::Thread::Attach(IL2CPP::Domain::Get());
    while (1) {
        ObjectList.clear();

        auto list = Unity::Object::FindObjectsOfType<Unity::CComponent>("SurvivalInteractable");
        for (int i = 0; i < list->m_uMaxLength + 1; i++)
        {
            if (!list->operator[](i))
                continue;

            Unity::CGameObject* object = list->operator[](i)->GetMemberValue<Unity::CGameObject*>("gameObject");
            if (!object) {
                continue;
            }

            ObjectList.push_back(object);
        }

        auto list2 = Unity::Object::FindObjectsOfType<Unity::CComponent>("KeyBehaviour");
        for (int j = 0; j < list2->m_uMaxLength + 1; j++)
        {
            if (!list2->operator[](j))
                continue;

            Unity::CGameObject* object2 = list2->operator[](j)->GetMemberValue<Unity::CGameObject*>("gameObject");
            if (!object2) {
                continue;
            }

            ObjectList.push_back(object2);
        }

        Sleep(5000);
    }

}