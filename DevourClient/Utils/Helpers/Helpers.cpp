#include "Helpers.hpp"

//define functions the same as in misc.hpp/cpp
bool Helpers::isPlayerCrawling() {
    return Players::LocalPlayer->GetComponent("NolanBehaviour")->CallMethod<bool*>("IsCrawling");
}
