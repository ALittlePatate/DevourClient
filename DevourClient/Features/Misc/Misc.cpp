#include "Misc.hpp"

void Misc::SetRank(int rank) {
    Players::LocalPlayer->GetComponent("NolanRankController")->CallMethodSafe<void*>("SetRank", rank);
}

void Misc::WalkInlobby(bool walk) {
    Players::LocalPlayer->GetComponent("UltimateCharacterLocomotionHandler")->GetGameObject()->SetActive(walk);
}