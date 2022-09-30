#pragma once
#include "../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"


namespace Players {
	extern std::vector<Unity::CGameObject*> PlayerList;
	extern Unity::CGameObject* LocalPlayer;

	void GetPlayersThread();
}