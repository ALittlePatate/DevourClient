#include "Dumper.hpp"
#include "../Output/Output.hpp"

std::vector<Unity::il2cppMethodInfo*> Dumper::DumpMethods(std::string component, std::string classname) {
    std::vector<Unity::il2cppMethodInfo*> methods_to_return;
    Unity::CGameObject* component_obj = Unity::GameObject::Find(component.c_str());
    if (!component_obj) {
        return methods_to_return;
    }

    size_t pos = classname.find("::");
    classname.erase(0, pos+2);

    Unity::CComponent* ccc = component_obj->GetComponent(classname.c_str());
    if (!ccc) {
        return methods_to_return;
    }

    ccc->FetchMethods(&methods_to_return);

    return methods_to_return;
}

std::vector<std::string> Dumper::DumpMethodsString(std::string component, std::string classname) {
    std::vector<std::string> methods_to_return;
    print("\nDumping methods of %s...\n", classname.c_str());

    std::vector<Unity::il2cppMethodInfo*> methods = Dumper::DumpMethods(component, classname);

    for (Unity::il2cppMethodInfo* method : methods) {
        if (!method) {
            return methods_to_return;
        }

        std::string name = method->m_pName;
        methods_to_return.push_back(name);
        print("--> %s\n", name.c_str());
    }

    return methods_to_return;
}


std::vector<Unity::CComponent*> Dumper::DumpClasses(std::string component) {
    std::vector<Unity::CComponent*> classes_to_return;

    Unity::CGameObject* component_obj = Unity::GameObject::Find(component.c_str());
    if (!component_obj) {
        return classes_to_return;
    }

    auto components = component_obj->GetComponents(UNITY_COMPONENT_CLASS);
    for (int n = 0; n < components->m_uMaxLength; n++)
    {
        if (!components->operator[](n))
            continue;

        classes_to_return.push_back(components->operator[](n));
    }

    return classes_to_return;
}

std::vector<std::string> Dumper::DumpClassesString(std::string component) {
    std::vector<std::string> classes_to_return;
    print("\nDumping classes of %s...\n",component.c_str());

    std::vector<Unity::CComponent*> classes = Dumper::DumpClasses(component);
    for (Unity::CComponent* class_obj : classes)
    {
        if (!class_obj)
            continue;

        std::string name = std::string(class_obj->m_Object.m_pClass->m_pNamespace) + "::" + std::string(class_obj->m_Object.m_pClass->m_pName);
        classes_to_return.push_back(name);
        print("--> %s\n", name.c_str());
    }
    return classes_to_return;
}

std::vector<Unity::CComponent*> Dumper::DumpComponents() {
    std::vector<Unity::CComponent*> compenents_to_return;

    auto list = Unity::Object::FindObjectsOfType<Unity::CComponent>(UNITY_COMPONENT_CLASS);
    for (int i = 0; i < list->m_uMaxLength + 1; i++)
    {
        if (!list->operator[](i))
            continue;

        compenents_to_return.push_back(list->operator[](i));
    }
    return compenents_to_return;
}

std::vector<std::string> Dumper::DumpComponentsString() {
    std::vector<std::string> compenents_to_return;
    print("\nDumping components...\n");

    std::vector<Unity::CComponent*> components = Dumper::DumpComponents();
    for (Unity::CComponent* component : components)
    {
        if (!component)
            continue;
        
        Unity::CGameObject* object = component->GetMemberValue<Unity::CGameObject*>("gameObject");
        std::string name = object->GetName()->ToString();
        compenents_to_return.push_back(name);
    }

    return compenents_to_return;
}