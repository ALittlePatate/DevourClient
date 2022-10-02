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
	if (settings::walk_in_lobby) {
		Misc::WalkInlobby(settings::walk_in_lobby);
	}
	if (settings::steam_name_spoof) {
		Misc::SetSteamName(settings::new_name);
	}
	if (settings::server_name_spoof) {
		Misc::SetServerName(settings::server_name);
	}

	Misc::UnlimitedUV(settings::unlimited_uv);
}