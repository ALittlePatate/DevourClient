#include "Misc.hpp"

void Misc::SetRank(int rank) {
    Players::LocalPlayer->GetComponent("NolanRankController")->CallMethodSafe<void*>("SetRank", rank);
}

void Misc::WalkInlobby(bool walk) {
    /*
    if (!Players::LocalPlayer->GetComponent("UltimateCharacterLocomotionHandler")) {
        Unity::il2cppClass* Character = IL2CPP::Class::Find("Opsive.UltimateCharacterController.Character::UltimateCharacterLocomotionHandler");
        Unity::CGameObject* UltimateCharacterLocomotionHandler =
        if (!UltimateCharacterLocomotionHandler) {
            return;
        }

        Players::LocalPlayer->AddComponent(UltimateCharacterLocomotionHandler);
    }
    */
}