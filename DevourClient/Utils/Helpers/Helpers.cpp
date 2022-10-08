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

std::string Helpers::GetActiveScene() {
    Unity::CGameObject* MapHelper = Unity::GameObject::Find("SaveHelpers");

    if (!MapHelper) {
        return "";
    }

    Unity::CComponent* MapHelperData = MapHelper->GetComponent("SaveHelpers");

    if (!MapHelperData) {
        return "";
    }

    return MapHelperData->GetMemberValue<Unity::System_String*>("sceneName")->ToString();
}

Unity::CGameObject* Helpers::Game() {
    if (!Players::LocalPlayer) {
        return NULL;
    }

    Unity::CComponent* Nolan = Players::LocalPlayer->GetComponent("NolanBehaviour");

    if (!Nolan) {
        return NULL;
    }

    Unity::CGameObject* Game = Nolan->GetMemberValue<Unity::CGameObject*>("gameController");

    if (!Game) {
        return NULL;
    }

    return Game;

    /*
        NOTE: this helper returns NULL if character is in lobby.

        some components for gameController
        - GameUI
        - Survival
    */
}
