#pragma once

#include "../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"
#include "../../Utils/Players/Players.hpp"
#include "../../Utils/Settings/Settings.hpp"
#include <iostream>


//define functions the same as in misc.hpp/cpp
namespace Helpers {
  bool isPlayerCrawling();
  bool IsInGame();
  std::string GetActiveScene();
  Unity::CGameObject* Game();
}
