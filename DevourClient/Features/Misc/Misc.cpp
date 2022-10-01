#include "Misc.hpp"

void Misc::SetRank(int rank) {
    Players::LocalPlayer->GetComponent("NolanRankController")->CallMethodSafe<void*>("SetRank", rank);
}