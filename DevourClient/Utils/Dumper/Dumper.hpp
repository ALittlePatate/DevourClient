#pragma once
#include "../Dependencies/IL2CPP_Resolver/il2cpp_resolver.hpp"


namespace Dumper {
	std::vector<Unity::CComponent*> DumpComponents();
	std::vector<std::string> DumpComponentsString();

	std::vector<Unity::CComponent*> DumpClasses(std::string component);
	std::vector<std::string> DumpClassesString(std::string component);

	std::vector<Unity::il2cppMethodInfo*> DumpMethods(std::string component, std::string classname);
	std::vector<std::string> DumpMethodsString(std::string component, std::string classname);
}