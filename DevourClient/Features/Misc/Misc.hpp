#pragma once

#include "../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"
#include "../../Utils/Players/Players.hpp"
#include "../../Utils/Settings/Settings.hpp"
#include <iostream>

namespace Misc {
    void SetRank(int rank);
    void WalkInlobby(bool walk);
    void UnlimitedUV(bool active);
    void SetSteamName(std::string name);
    void SetServerName(std::string name);
    void PlayRandomSound();
    void CarryItem(const char* itemName);
    void PlayerSpeed(int speed);
    void SpawnAnimal(const char* animalName);
    void ForceStart();
    void BurnRitualObj(bool burnAll);
    void KnockoutPlayers(bool killYourself);
    void Revive(bool reviveEveryone);
    void SkipLongInteract();
    void TPKeys();
}
