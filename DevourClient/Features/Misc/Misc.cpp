#include "Misc.hpp"
#include "../../Utils/Output/Output.hpp"

void Misc::SetRank(int rank) {
    Players::LocalPlayer->GetComponent("NolanRankController")->CallMethodSafe<void*>("SetRank", rank);
}

void Misc::WalkInlobby(bool walk) {
    /*
    if (!Players::LocalPlayer->GetComponent("UltimateCharacterLocomotionHandler")) {
        Unity::il2cppClass* Character = IL2CPP::Class::Find("Opsive.UltimateCharacterController.Character::UltimateCharacterLocomotionHandler");
        Unity::CGameObject* UltimateCharacterLocomotionHandler =
        if (!UltimateCharacterLocomotionHandler) {
            return;
        }

        Players::LocalPlayer->AddComponent(UltimateCharacterLocomotionHandler);
    }
    */
}

void Misc::UnlimitedUV(bool active) {
    try {
        Unity::CComponent* NolanBehaviour = Players::LocalPlayer->GetComponent("NolanBehaviour");
        if (!NolanBehaviour) {
            return;
        }
        NolanBehaviour->CallMethodSafe<void*>("SetPurgatory", active);
    }
    catch (...) {
        settings::unlimited_uv = false;
        print("Unlimited UV error");
    }
}

void Misc::SetSteamName(std::string name) {
    Unity::CGameObject* MenuController = Unity::GameObject::Find("MenuController");
    if (!MenuController) {
        return;
    }

    Unity::CComponent* Menu = MenuController->GetComponent("Horror.Menu");
    if (!Menu) {
        return;
    }

    Menu->SetMemberValue<Unity::System_String*>("steamName", IL2CPP::String::New(name));
}

void Misc::SetServerName(std::string name) {
    Unity::CGameObject* MenuController = Unity::GameObject::Find("MenuController");
    if (!MenuController) {
        return;
    }

    Unity::CComponent* Menu = MenuController->GetComponent("Horror.Menu");
    if (!Menu) {
        return;
    }

    Unity::CComponent* serverNameText = Menu->GetMemberValue<Unity::CComponent*>("serverNameText");
    if (!serverNameText) {
        return;
    }

    serverNameText->SetMemberValue<Unity::System_String*>("m_Text", IL2CPP::String::New(name));
}

void Misc::PlayRandomSound() {
    Unity::CComponent* NolanVoiceOvers = Players::LocalPlayer->GetComponent("NolanVoiceOvers");
    if (!NolanVoiceOvers) {
        return;
    }

    srand((unsigned int)time(0));
    int num = 1;// rand() % 10 + 1;
    switch (num)
    {
    case 1: {
        Unity::CComponent* yesClips = NolanVoiceOvers->GetMemberValue<Unity::CComponent*>("yesClips");
        if (!yesClips) {
            return;
        }

        yesClips->SetMemberValue<float>("delay", 0.f);
        yesClips->SetMemberValue<bool>("useGoatBurnCount", false);
        yesClips->SetMemberValue<bool>("noDuplicateSelection", false);

        std::vector<Unity::il2cppMethodInfo*> methods;
        std::vector<Unity::il2cppFieldInfo*> fields;
        yesClips->FetchMethods(&methods);
        yesClips->FetchFields(&fields);

        for (Unity::il2cppMethodInfo* method : methods) {
            print("--> %s\n", method->m_pName);
        }
        print("\n\n\n");

        for (Unity::il2cppFieldInfo* field : fields) {
            print("--> %s\n", field->m_pName);
        }

        yesClips->CallMethodSafe<void*>("Play");
        break;
    }
    /*
    case 2:
        nolanVoiceOvers.noClips.Play();
        return;
    case 3:
        nolanVoiceOvers.beckonClips.Play();
        return;
    case 4:
        nolanVoiceOvers.showOffClips.Play();
        return;
    case 5:
        nolanVoiceOvers.screamClips.Play();
        return;
    case 6:
        nolanVoiceOvers.pickupClips.Play();
        return;
    case 7:
        nolanVoiceOvers.burnGoatClips.Play();
        return;
    case 8:
        nolanVoiceOvers.laughClips.Play();
        return;
    case 9:
        nolanVoiceOvers.PlayMoan();
        return;
    case 10:
        nolanVoiceOvers.Scream();
        return;
        */
    default:
        break;
    }
}