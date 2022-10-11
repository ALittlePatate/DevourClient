#include "Misc.hpp"
#include "../../Utils/Output/Output.hpp"
#include "../../Utils/Helpers/Helpers.hpp"
#include "../../Utils/Objects/Objects.hpp"
#include <time.h>

void Misc::SetRank(int rank) {
    if (!Players::LocalPlayer) {
        return;
    }

    Unity::CComponent* NolanRankController = Players::LocalPlayer->GetComponent("NolanRankController");
    if (!NolanRankController) {
        return;
    }
    NolanRankController->CallMethodSafe<void*>("SetRank", rank);
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

void Misc::CarryItem(const char* item) {
    if (Helpers::GetActiveScene() == std::string("Menu")) {
        return;
    }

    std::string setItemName = "";

    if (item == "Hay") {
        setItemName = "SurvivalHay";
    }
    if (item == "First aid") {
        setItemName = "SurvivalFirstAid";
    }
    if (item == "Battery") {
        setItemName = "SurvivalBattery";
    }
    if (item == "Gasoline") {
        setItemName = "SurvivalGasoline";
    }
    if (item == "Fuse") {
        setItemName = "SurvivalFuse";
    }
    if (item == "Food") {
        setItemName = "SurvivalRottenFood";
    }
    if (item == "Bleach") {
        setItemName = "SurvivalBleach";
    }
    if (item == "Ritual Book (inactive)") {
        // inactive book example: RitualBook-InActive-<int>
        // active book example: RitualBook-Active-1
        setItemName = "RitualBook-InActive-1";
    }
    if (item == "Ritual Book (active)") {
        // inactive book example: RitualBook-InActive-<int>
        // active book example: RitualBook-Active-1
        setItemName = "RitualBook-Active-1";
    }
    if (item == "Matchbox") {
        setItemName = "Matchbox-3";
    }
    if (item == "Egg-1") {
        // clean egg example: "Egg-Clean-<int>"
        // dirty egg example: "Egg-Dirty-<int>"
        setItemName = "Egg-Clean-1";
    }
    if (item == "Egg-2") {
        setItemName = "Egg-Clean-2";
    }
    if (item == "Egg-3") {
        setItemName = "Egg-Clean-3";
    }
    if (item == "Egg-4") {
        setItemName = "Egg-Clean-4";
    }
    if (item == "Egg-5") {
        setItemName = "Egg-Clean-5";
    }
    if (item == "Egg-6") {
        setItemName = "Egg-Clean-6";
    }
    if (item == "Egg-7") {
        setItemName = "Egg-Clean-7";
    }
    if (item == "Egg-8") {
        setItemName = "Egg-Clean-8";
    }
    if (item == "Egg-9") {
        setItemName = "Egg-Clean-9";
    }
    if (item == "Egg-10") {
        setItemName = "Egg-Clean-10";
    }

    try {
        Unity::CComponent* NolanBehaviour = Players::LocalPlayer->GetComponent("NolanBehaviour");
        if (!NolanBehaviour) {
            return;
        }

        NolanBehaviour->CallMethod<void*>("StartCarry", IL2CPP::String::New(setItemName));
    }
    catch (...) {
        return;
        //print("Error!");
    }
}

void Misc::PlayerSpeed(int speed) {
    try {
        Unity::CComponent* UltimateCharacterLocomotion = Players::LocalPlayer->GetComponent("Opsive.UltimateCharacterController.Character.UltimateCharacterLocomotion");

        if (!UltimateCharacterLocomotion)
            return;

        UltimateCharacterLocomotion->SetMemberValue("TimeScale", (float)settings::new_speed);
    }
    catch (...) {
        settings::change_player_speed = false;
        //print("[ERROR] speed error\n");
    }
}

void Misc::SpawnAnimal(const char* animalName) {
    if (Helpers::GetActiveScene() == std::string("Menu")) {
        return;
    }

    std::string AnimalId = "";

    if (animalName == "Goat") {
        AnimalId = "SurvivalGoat";
    }
    if (animalName == "Rat") {
        AnimalId = "SurvivalRat";
    }
    if (animalName == "Spider") {
        // spawn spider
        return;
    }

    try {
        Unity::CComponent* NolanBehaviour = Players::LocalPlayer->GetComponent("NolanBehaviour");
        if (!NolanBehaviour) {
            return;
        }

        NolanBehaviour->CallMethod<void*>("StartCarry", IL2CPP::String::New(AnimalId));
    }
    catch (...) {
        return;
    }
}

void Misc::ForceStart() {
    Unity::CGameObject* MenuController = Unity::GameObject::Find("MenuController");

    if (!MenuController) {
        return;
    }

    Unity::CComponent* Menu = MenuController->GetComponent("Horror.Menu");

    if (!Menu) {
        return;
    }

    Unity::CComponent* player = Players::LocalPlayer->GetComponent("BoltEntity");
    
    if (!player) {
        return;
    }

    // check if player is host or not

    Menu->CallMethodSafe<void*>("OnLobbyStartButtonClick");
}

void Misc::BurnRitualObj(bool burnAll) {

    // check if player is host or not

    std::string currentMap = Helpers::GetActiveScene();

    if (currentMap == IL2CPP::String::New("Menu")->ToString()) {
        return;
    }

    if (currentMap == IL2CPP::String::New("Devour")->ToString()) {
        Unity::CGameObject* DevourAltar = Unity::GameObject::Find("SurvivalAltar");

        if (!DevourAltar) {
            return;
        }

        Unity::CComponent* DevourAltarData = DevourAltar->GetComponent("SurvivalObjectBurnController");

        if (!DevourAltarData) {
            return;
        }

        if (burnAll) {
            DevourAltarData->CallMethodSafe<void*>("SkipToGoat", 10);
        }
        else {
            DevourAltarData->CallMethodSafe<void*>("BurnGoat");
        }
    }

    if (currentMap == IL2CPP::String::New("Molly")->ToString()) {
        Unity::CGameObject* MollyAltar = Unity::GameObject::Find("SurvivalAltarMolly");

        if (!MollyAltar) {
            return;
        }

        Unity::CComponent* MollyAltarData = MollyAltar->GetComponent("SurvivalMollyAltarController");

        if (!MollyAltarData) {
            return;
        }

        if (burnAll) {
            MollyAltarData->CallMethodSafe<void*>("SkipToGoat", 10);
        }
        else {
            MollyAltarData->CallMethodSafe<void*>("BurnGoat");
        }
    }

    if (currentMap == IL2CPP::String::New("Inn")->ToString()) {
        Unity::CGameObject* InnAltar = Unity::GameObject::Find("InnMapController");

        if (!InnAltar) {
            return;
        }

        Unity::CComponent* InnAltarData = InnAltar->GetComponent("InnMapController");

        if (!InnAltarData) {
            return;
        }

        if (burnAll) {
            InnAltarData->CallMethodSafe<void*>("SetProgressTo", 10);
        }
        else {
            InnAltarData->CallMethodSafe<void*>("IncreaseProgress");
        }
    }

    if (currentMap == IL2CPP::String::New("Town")->ToString()) {
        Unity::CGameObject* TownAltar = Unity::GameObject::Find("SurvivalAltarTown");

        if (!TownAltar) {
            return;
        }

        Unity::CComponent* TownAltarData = TownAltar->GetComponent("SurvivalTownAltarController");

        if (!TownAltarData) {
            return;
        }

        if (burnAll) {
            TownAltarData->CallMethodSafe<void*>("SkipToGoat", 10);
        }
        else {
            TownAltarData->CallMethodSafe<void*>("BurnGoat");
        }
    }
}

void Misc::KnockoutPlayers(bool killYourself) {
    std::string currentMap = Helpers::GetActiveScene();

    if (currentMap == IL2CPP::String::New("Menu")->ToString()) {
        return;
    }

    if (currentMap == IL2CPP::String::New("Devour")->ToString()) {
        
        if (!Players::LocalPlayer) {
            return;
        }

        Unity::CGameObject* AzazelAnna = Unity::GameObject::Find("SurvivalAnnaNew(Clone)");

        if (!AzazelAnna) {
            return;
        }

        Unity::CComponent* AzazelAnnaComp = AzazelAnna->GetComponent("SurvivalAzazelBehaviour");

        if (!AzazelAnnaComp) {
            return;
        }

        if (killYourself) {
            AzazelAnnaComp->CallMethodSafe<void*>("OnKnockout", AzazelAnna, Players::LocalPlayer);
        }
        else {
            for (Unity::CGameObject* player : Players::PlayerList) {
                if (!player || player == Players::LocalPlayer) {
                    continue;
                }

                AzazelAnnaComp->CallMethodSafe<void*>("OnKnockout", AzazelAnna, player);
            }
        }
    }

    if (currentMap == IL2CPP::String::New("Molly")->ToString()) {

        if (!Players::LocalPlayer) {
            return;
        }

        Unity::CGameObject* AzazelMolly = Unity::GameObject::Find("SurvivalAzazelMolly(Clone)");

        if (!AzazelMolly) {
            return;
        }

        Unity::CComponent* AzazelMollyComp = AzazelMolly->GetComponent("SurvivalAzazelBehaviour");

        if (!AzazelMollyComp) {
            return;
        }

        if (killYourself) {
            AzazelMollyComp->CallMethodSafe<void*>("OnKnockout", AzazelMolly, Players::LocalPlayer);
        }
        else {
            for (Unity::CGameObject* player : Players::PlayerList) {
                if (!player || player == Players::LocalPlayer) {
                    continue;
                }

                AzazelMollyComp->CallMethodSafe<void*>("OnKnockout", AzazelMolly, player);
            }
        }
    }

    if (currentMap == IL2CPP::String::New("Town")->ToString()) {

        if (!Players::LocalPlayer) {
            return;
        }

        Unity::CGameObject* AzazaelSam = Unity::GameObject::Find("AzazelSam(Clone)");

        if (!AzazaelSam) {
            return;
        }

        Unity::CComponent* AzazelSamComp = AzazaelSam->GetComponent("AzazelSamBehaviour");

        if (!AzazelSamComp) {
            return;
        }

        if (killYourself) {
            AzazelSamComp->CallMethodSafe<void*>("OnKnockout", AzazaelSam, Players::LocalPlayer);
        }
        else {
            for (Unity::CGameObject* player : Players::PlayerList) {
                if (!player || player == Players::LocalPlayer) {
                    continue;
                }

                AzazelSamComp->CallMethodSafe<void*>("OnKnockout", AzazaelSam, player);
            }
        }
    }

    if (currentMap == IL2CPP::String::New("Inn")->ToString()) {

        if (!Players::LocalPlayer) {
            return;
        }

        Unity::CGameObject* AzazelZara = Unity::GameObject::Find("AzazelZara(Clone)");

        if (!AzazelZara) {
            return;
        }

        Unity::CComponent* AzazelZaraComp = AzazelZara->GetComponent("AzazelZaraBehaviour");

        if (!AzazelZaraComp) {
            return;
        }

        if (killYourself) {
            AzazelZaraComp->CallMethodSafe<void*>("OnKnockout", AzazelZara, Players::LocalPlayer);
        }
        else {
            for (Unity::CGameObject* player : Players::PlayerList) {
                if (!player || player == Players::LocalPlayer) {
                    continue;
                }

                AzazelZaraComp->CallMethodSafe<void*>("OnKnockout", AzazelZara, player);
            }
        }
    }
}

void Misc::Revive(bool reviveEveryone) {
    Unity::CComponent* SurvivalReviveInteractable = Players::LocalPlayer->GetComponent("SurvivalReviveInteractable");

    if (!SurvivalReviveInteractable) {
        return;
    }

    if (reviveEveryone) {
        try {
            for (Unity::CGameObject* player : Players::PlayerList) {
                if (!player || player == Players::LocalPlayer) {
                    continue;
                }

                player->GetComponent("SurvivalReviveInteractable")->CallMethodSafe<void*>("Interact", player);
            }
        }
        catch (...) {
            return;
        }

    }
    else {
        try {
            SurvivalReviveInteractable->CallMethodSafe<void*>("Interact", Players::LocalPlayer);
        }
        catch (...) {
            return;
        }
    }
}

void Misc::SkipLongInteract() {
    /*
    Unity::CGameObject* SurvivalAltarDevour = Unity::GameObject::Find("SurvivalAltar");

    if (SurvivalAltarDevour) {
        Unity::CComponent* SurvivalObjectBurnController = SurvivalAltarDevour->GetComponent("SurvivalObjectBurnController");

        if (!SurvivalObjectBurnController) {
            return;
        }

        try {
            SurvivalObjectBurnController->CallMethodSafe<void*>("PourGasoline");
        }
        catch (...) {
            return;
        }
    }

    Unity::CGameObject* SurvivalAltarTown = Unity::GameObject::Find("SurvivalAltarTown");

    if (SurvivalAltarTown) {
        Unity::CComponent* SurvivalTownAltarController = SurvivalAltarTown->GetComponent("SurvivalTownAltarController");

        if (!SurvivalTownAltarController) {
            return;
        }

        try {
            SurvivalTownAltarController->CallMethodSafe<void*>("PourGasoline");
        }
        catch (...) {
            return;
        }
    }

    Unity::CGameObject* SurvivalAltarMolly = Unity::GameObject::Find("SurvivalAltarMolly");

    if (SurvivalAltarMolly) {
        Unity::CComponent* SurvivalMollyAltarController = SurvivalAltarMolly->GetComponent("SurvivalMollyAltarController");

        if (!SurvivalMollyAltarController) {
            return;
        }

        try {
            SurvivalMollyAltarController->CallMethodSafe<void*>("PlaceFuse");
        }
        catch (...) {
            return;
        }
    }
    */
}

void Misc::TPKeys() {
    for (Unity::CGameObject* object : Objects::ObjectList) {
        if (!object || !object->m_CachedPtr) {
            continue;
        }

        if (object->GetName()->ToString() == IL2CPP::String::New("Key(Clone)")->ToString()) {
            object->GetTransform()->SetLocalPosition(Players::LocalPlayer->GetTransform()->GetPosition());
        }
    }
}

void Misc::ShootEveryone(bool shootEveryone, bool hit) {

    std::string currentMap = Helpers::GetActiveScene();

    if (currentMap == IL2CPP::String::New("Town")->ToString()) {
        if (!Players::LocalPlayer) {
            return;
        }

        Unity::CGameObject* Sam = Unity::GameObject::Find("AzazelSam(Clone)");

        if (!Sam) {
            return;
        }

        Unity::CComponent* SamBehaviour = Sam->GetComponent("AzazelSamBehaviour");

        if (!SamBehaviour) {
            return;
        }


        if (shootEveryone) {
            for (Unity::CGameObject* player : Players::PlayerList) {
                if (!player || player == Players::LocalPlayer) {
                    continue;
                }

                SamBehaviour->CallMethodSafe<void*>("OnShootPlayer", player, hit);
            }
        }
        else {
            SamBehaviour->CallMethodSafe<void*>("OnShootPlayer", Players::LocalPlayer, hit);
        }
    }
    else {
        return;
    }
}

void Misc::Jumpscare(bool inHidingSpot) {
    std::string currentMap = Helpers::GetActiveScene();

    if (!Players::LocalPlayer) {
        return;
    }

    if (currentMap == IL2CPP::String::New("Menu")->ToString()) {
        return;
    }

    if (currentMap == IL2CPP::String::New("Devour")->ToString()) {

        Unity::CGameObject* AzazelAnna = Unity::GameObject::Find("SurvivalAnnaNew(Clone)");

        if (!AzazelAnna) {
            return;
        }

        Unity::CComponent* AzazelAnnaComp = AzazelAnna->GetComponent("SurvivalAzazelBehaviour");

        if (!AzazelAnnaComp) {
            return;
        }

        for (Unity::CGameObject* player : Players::PlayerList) {
            if (!player || player == Players::LocalPlayer) {
                continue;
            }

            AzazelAnnaComp->CallMethodSafe<void*>("OnPickedUpPlayer", AzazelAnna, player, inHidingSpot);
        }
    }

    if (currentMap == IL2CPP::String::New("Molly")->ToString()) {

        Unity::CGameObject* AzazelMolly = Unity::GameObject::Find("SurvivalAzazelMolly(Clone)");

        if (!AzazelMolly) {
            return;
        }

        Unity::CComponent* AzazelMollyComp = AzazelMolly->GetComponent("SurvivalAzazelBehaviour");

        if (!AzazelMollyComp) {
            return;
        }

        for (Unity::CGameObject* player : Players::PlayerList) {
            if (!player || player == Players::LocalPlayer) {
                continue;
            }

            AzazelMollyComp->CallMethodSafe<void*>("OnPickedUpPlayer", AzazelMolly, player, inHidingSpot);
        }
    }

    if (currentMap == IL2CPP::String::New("Town")->ToString()) {

        Unity::CGameObject* AzazaelSam = Unity::GameObject::Find("AzazelSam(Clone)");

        if (!AzazaelSam) {
            return;
        }

        Unity::CComponent* AzazelSamComp = AzazaelSam->GetComponent("AzazelSamBehaviour");

        if (!AzazelSamComp) {
            return;
        }

        for (Unity::CGameObject* player : Players::PlayerList) {
            if (!player || player == Players::LocalPlayer) {
                continue;
            }

            AzazelSamComp->CallMethodSafe<void*>("OnPickedUpPlayer", AzazaelSam, player, inHidingSpot);
        }
    }

    if (currentMap == IL2CPP::String::New("Inn")->ToString()) {

        Unity::CGameObject* AzazelZara = Unity::GameObject::Find("AzazelZara(Clone)");

        if (!AzazelZara) {
            return;
        }

        Unity::CComponent* AzazelZaraComp = AzazelZara->GetComponent("AzazelZaraBehaviour");

        if (!AzazelZaraComp) {
            return;
        }

        for (Unity::CGameObject* player : Players::PlayerList) {
            if (!player || player == Players::LocalPlayer) {
                continue;
            }

            AzazelZaraComp->CallMethodSafe<void*>("OnPickedUpPlayer", AzazelZara, player, inHidingSpot);
        }
    }
}
