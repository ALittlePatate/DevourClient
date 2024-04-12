#include "GHelpers.hpp"

//define functions the same as in misc.hpp/cpp
bool Helpers::isPlayerCrawling() {
    return false;
    //return Players::LocalPlayer->GetComponent("NolanBehaviour")->CallMethod<bool*>("IsCrawling");
}

bool Helpers::IsInGame() {
    return false;
    /*
    Unity::CGameObject* OptionsHelpers = Unity::GameObject::Find("OptionsHelpers");
    Unity::CComponent* OptionsHelpersData = OptionsHelpers->GetComponent("OptionsHelpers");

    if (OptionsHelpers == NULL) {
        return false;
    }

    if (OptionsHelpersData == NULL) {
        return false;
    }

    return OptionsHelpersData->GetMemberValue<bool*>("inGame");
    */
}

std::string Helpers::GetActiveScene() {
    return "Menu";
    /*
    Unity::CGameObject* MapHelper = Unity::GameObject::Find("SaveHelpers");

    if (!MapHelper) {
        return "";
    }

    Unity::CComponent* MapHelperData = MapHelper->GetComponent("SaveHelpers");

    if (!MapHelperData) {
        return "";
    }

    return MapHelperData->GetMemberValue<Unity::System_String*>("sceneName")->ToString();
    */
}

    /*
        NOTE: this helper returns NULL if character is in lobby.

        some components for gameController
        - GameUI
        - Survival
    */
/*
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

}
*/
