#include "Misc.hpp"

void Misc::SetRank(int rank) {
    Players::LocalPlayer->GetComponent("NolanRankController")->CallMethodSafe<void*>("SetRank", rank);
}

void Misc::WalkInlobby(bool walk) {
    Unity::CComponent* UltimateCharacterLocomotionHandler = Players::LocalPlayer->GetComponent("UltimateCharacterLocomotionHandler");

    if (UltimateCharacterLocomotionHandler == NULL)
    {
        Players::LocalPlayer->AddComponent(IL2CPP::Class::GetSystemType("UltimateCharacterLocomotionHandler"));
        Players::LocalPlayer->GetComponent("UltimateCharacterLocomotionHandler")->SetMemberValue<bool>("enabled", false);
    }

    Players::LocalPlayer->GetComponent("UltimateCharacterLocomotionHandler")->SetMemberValue<bool>("enabled", walk);
}