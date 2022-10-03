#pragma once
#include "../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"


namespace Objects {
	extern std::vector<Unity::CGameObject*> ObjectList;

	void GetObjectsThread();
}