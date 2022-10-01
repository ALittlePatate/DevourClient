#include "Helpers.hpp"

//define functions the same as in misc.hpp/cpp
bool Helpers::isPlayerCrawling() {
    return Players::LocalPlayer->GetComponent("NolanBehaviour")->CallMethod<bool*>("IsCrawling");
}

bool Helpers::IsInGame() {
    Unity::CGameObject* OptionsHelpers = Unity::GameObject::Find("OptionsHelpers");
    Unity::CComponent* OptionsHelpersData = OptionsHelpers->GetComponent("OptionsHelpers");

    if (OptionsHelpers == NULL) {
        return false;
    }

    if (OptionsHelpersData == NULL) {
        return false;
    }

    return OptionsHelpersData->GetMemberValue<bool*>("inGame");
}
