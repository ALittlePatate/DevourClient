#include "Dumper.hpp"
#include "../Output/Output.hpp"

void Dump(std::string component) {
    print("\n\n\n");
    print("Dumping %s\n\n", component.c_str());
    auto list = Unity::Object::FindObjectsOfType<Unity::CComponent>(component.c_str());
    for (int i = 0; i < list->m_uMaxLength + 1; i++)
    {
        if (!list->operator[](i))
            continue;

        Unity::CGameObject* object = list->operator[](i)->GetMemberValue<Unity::CGameObject*>("gameObject");

        print("%s\n", object->GetName()->ToString().c_str()); //SurvivalPlayer(Clone)

        auto components = object->GetComponents(UNITY_COMPONENT_CLASS);
        for (int n = 0; n < components->m_uMaxLength; n++)
        {
            if (!components->operator[](n))
                continue;

            print("| -> %s::%s\n", components->operator[](n)->m_Object.m_pClass->m_pNamespace, components->operator[](n)->m_Object.m_pClass->m_pName);

        }
        Unity::CComponent* BoltEntity = object->GetComponent("BoltEntity");
        if (BoltEntity)
        {
            print("|-------- owner:%d \n", BoltEntity->CallMethod<bool>("get_IsOwner")); //local player check?
            if (BoltEntity->CallMethod<bool>("get_IsOwner"))
            {
            }
        }
    }
}