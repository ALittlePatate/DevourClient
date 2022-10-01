#include "OnUpdate.hpp"

#include <iostream>

#include "../Utils/Settings/Settings.hpp"
#include "../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"
#include "../Utils/Players/Players.hpp"

#include "../Features/Misc/Misc.hpp"


void OnUpdate() {
	if (settings::spoof_level) {
		Misc::SetRank(settings::new_level);
	}
}