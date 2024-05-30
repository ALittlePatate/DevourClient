#include "pch-il2cpp.h"
#include "Hooks.hpp"
#include "features/menu.hpp"
#include "settings/settings.hpp"
#include "../features/esp/esp.hpp"
#include "main.h"
#include "utils/utils.hpp"

#include <iostream>
#include "helpers.h"

#include "players/players.h"
#include "UnityCore.h"
#include "ClientHelper.h"
#include "features/misc/misc.h"

#pragma warning(push, 0) //important cuz dx11 throws so much warnings
#include <d3d11.h>
#pragma warning(pop)
#pragma comment (lib, "d3d11.lib" )

bool open_menu = false;

/*
    //Code about hooking stuff
    //Exemple of hooked function :
    typedef int (__stdcall* TEST)(); //We define the function, must be the EXACT same definition as the original one
    TEST test_org = NULL;
    int __stdcall test_hook() //MUST BE the original calling convention
    {
        std::cout << "called" << std::endl;
        return test_org(); //if we want to call the original one, we can just return 1 otherwise
    }

    //Exemple :
    MH_STATUS status_test = MH_CreateHook((LPVOID*)test_sig, &test_hook, reinterpret_cast<LPVOID*>(&test_org));
    //We say that for every call to the test_sig address we want to redirect it to the address of the function named test_hook (& gives the pointer to it)
    //We can store the original pointer to the original function into test_org if we want to call the org later --> trampoline hook
    //original_sum can be NULL if we don't want to trampoline hook

    if (status_test != MH_OK) { //If it failed
        print(MH_StatusToString(status)); //If we are in debug mode, we print the fail status into the console
        return 0; //We exit
    }
*/

typedef void(__stdcall* TDebug_2_Log)(app::Object*, MethodInfo*);
TDebug_2_Log oDebug_2_Log = NULL;
void __stdcall hDebug_Log(app::Object* message, MethodInfo* method) {
	std::cout << "[DevourClient]: " << ToString(message) << std::endl;
	il2cppi_log_write(ToString(message));
}

typedef void(__stdcall* TNolanBehaviour_OnAttributeUpdateValue)(app::NolanBehaviour*, app::Attribute_1*, MethodInfo*);
TNolanBehaviour_OnAttributeUpdateValue oNolanBehaviour_OnAttributeUpdateValue = NULL;
void __stdcall hNolanBehaviour_OnAttributeUpdateValue(app::NolanBehaviour* __this, app::Attribute_1* attribute, MethodInfo* method) {
	if (settings::unlimited_uv && strcmp(il2cppi_to_string(attribute->fields.m_Name).c_str(), "Battery") == 0) {
		attribute->fields.m_Value = 100.0f;
	}
	oNolanBehaviour_OnAttributeUpdateValue(__this, attribute, method);
}

// DO_APP_FUNC(0x004A7D70, void, NolanBehaviour_FixedUpdate, (NolanBehaviour* __this, MethodInfo* method));
typedef void(__stdcall* TNolanBehaviour_FixedUpdate)(app::NolanBehaviour*, MethodInfo*);
TNolanBehaviour_FixedUpdate oNolanBehaviour_FixedUpdate = NULL;
void __stdcall hNolanBehaviour_FixedUpdate(app::NolanBehaviour* __this, MethodInfo* method) {

	if (settings::freeze_azazel && IsHost()) {
		app::GameObject* goAzazel = __this->fields.m_Survival->fields.m_Azazel;

		if (goAzazel) {
			app::String* azazelComponent = reinterpret_cast<app::String*>(il2cpp_string_new("UltimateCharacterLocomotion"));
			auto component = app::GameObject_GetComponentByName(goAzazel, azazelComponent, nullptr);

			if (component) {
				app::UltimateCharacterLocomotion* locomotion = reinterpret_cast<app::UltimateCharacterLocomotion*>(component);

				if (locomotion) {
					// confused Azazel!! fix it
					app::UltimateCharacterLocomotion_set_TimeScale(locomotion, settings::new_azazel_speed, NULL);
				}
			}
		}
	}


	if (settings::change_player_speed && IsLocalPlayer(__this)) {
		// Apply Sprint Speed
		if (__this->fields.speedChangeAbility)
		{
			__this->fields.speedChangeAbility->fields.m_SpeedChangeMultiplier = (float)settings::new_speed;
			__this->fields.speedChangeAbility->fields.m_MaxSpeedChangeValue = (float)settings::new_speed;
		}
	}

	oNolanBehaviour_FixedUpdate(__this, method);
}

// DO_APP_FUNC(0x01D48040, bool, Cursor_1_get_visible, (MethodInfo * method));
typedef bool(__stdcall* TCursor_1_get_visible)(MethodInfo*);
TCursor_1_get_visible oCursor_1_get_visible = NULL;
bool __stdcall hCursor_1_get_visible(MethodInfo* method) {

	if (open_menu)
		return true;

	return oCursor_1_get_visible(method);
}

// DO_APP_FUNC(0x02D25380, void, Cursor_1_set_visible, (bool value, MethodInfo * method));
typedef bool(__stdcall* TCursor_1_set_visible)(bool, MethodInfo*);
TCursor_1_set_visible oCursor_1_set_visible = NULL;
bool __stdcall hCursor_1_set_visible(bool value, MethodInfo* method) {

	if (open_menu)
		value = true;

	return oCursor_1_set_visible(value, method);
}


// DO_APP_FUNC(0x02D253D0, CursorLockMode__Enum, Cursor_1_get_lockState, (MethodInfo * method));
typedef app::CursorLockMode__Enum(__stdcall* TCursor_1_get_lockState)(MethodInfo*);
TCursor_1_get_lockState oCursor_1_get_lockState = NULL;
app::CursorLockMode__Enum __stdcall hCursor_1_get_lockState(MethodInfo* method) {

	if (open_menu)
		return app::CursorLockMode__Enum::None;

	return oCursor_1_get_lockState(method);
}

// DO_APP_FUNC(0x02D25420, void, Cursor_1_set_lockState, (CursorLockMode__Enum value, MethodInfo * method));
typedef void(__stdcall* TCursor_1_set_lockState)(app::CursorLockMode__Enum, MethodInfo*);
TCursor_1_set_lockState oCursor_1_set_lockState = NULL;
void __stdcall hCursor_1_set_lockState(app::CursorLockMode__Enum value, MethodInfo* method) {

	if (open_menu)
		value = app::CursorLockMode__Enum::None;

	return oCursor_1_set_lockState(value, method);
}

// DO_APP_FUNC(0x005E5E10, bool, OptionsHelpers_IsRobeUnlocked, (OptionsHelpers * __this, String * robe, String * character, MethodInfo * method));
typedef bool(__stdcall* TOptionsHelpers_IsRobeUnlocked)(app::OptionsHelpers*, app::String*, app::String*, MethodInfo*);
TOptionsHelpers_IsRobeUnlocked oOptionsHelpers_IsRobeUnlocked = NULL;
bool __stdcall hOptionsHelpers_IsRobeUnlocked(app::OptionsHelpers* __this, app::String* robe, app::String* character, MethodInfo* method) {

	if (settings::unlock_all)
		return true;

	return oOptionsHelpers_IsRobeUnlocked(__this, robe, character, method);
}

// app::DevourInput_GetLongPress
// DO_APP_FUNC(0x005A1890, bool, DevourInput_GetLongPress, (DevourInput * __this, String * name, float duration, bool waitForRelease, MethodInfo * method));
typedef bool(__stdcall* TDevourInput_GetLongPress)(app::DevourInput*, app::String*, float, bool, MethodInfo*);
TDevourInput_GetLongPress oDevourInput_GetLongPress = NULL;
bool __stdcall hDevourInput_GetLongPress(app::DevourInput* __this, app::String* name, float duration, bool waitForRelease, MethodInfo* method) {

	if (settings::disable_longInteract) {
		duration = 0.0f;
	}

	return oDevourInput_GetLongPress(__this, name, duration, waitForRelease, method);
}

//DO_APP_FUNC(0x005AD0F0, bool, LockedInteractable_CanInteract, (LockedInteractable * __this, GameObject * character, MethodInfo * method));
typedef bool(__stdcall* TLockedInteractable_CanInteract)(app::LockedInteractable*, app::GameObject*, MethodInfo*);
TLockedInteractable_CanInteract oLockedInteractable_CanInteract = NULL;
bool __stdcall hLockedInteractable_CanInteract(app::LockedInteractable* __this, app::GameObject* character, MethodInfo* method) {

	if (settings::unlock_all)
	{
		__this->fields.isLocked = false;
		return true;
	}

	return oLockedInteractable_CanInteract(__this, character, method);
}

// DO_APP_FUNC(0x005E44C0, bool, OptionsHelpers_IsCharacterUnlocked, (OptionsHelpers * __this, String * prefab, MethodInfo * method));
typedef bool(__stdcall* TOptionsHelpers_IsCharacterUnlocked)(app::OptionsHelpers*, app::String*, MethodInfo*);
TOptionsHelpers_IsCharacterUnlocked oOptionsHelpers_IsCharacterUnlocked = NULL;
bool __stdcall hOptionsHelpers_IsCharacterUnlocked(app::OptionsHelpers* __this, app::String* prefab, MethodInfo* method) {

	if (settings::unlock_all)
		return true;

	return oOptionsHelpers_IsCharacterUnlocked(__this, prefab, method);
}

typedef app::RankHelpers_ExpGainInfo* (__stdcall* TRankHelpers_CalculateExpGain)(app::RankHelpers*, int32_t, int32_t, app::GameConfigToken*, MethodInfo*);
TRankHelpers_CalculateExpGain oRankHelpers_CalculateExpGain = NULL;
app::RankHelpers_ExpGainInfo* __stdcall hRankHelpers_CalculateExpGain(app::RankHelpers* __this, int32_t mapProgress, int32_t numAwards, app::GameConfigToken* gameConfigToken, MethodInfo* method) {
	app::RankHelpers_ExpGainInfo* gain = oRankHelpers_CalculateExpGain(__this, mapProgress, numAwards, gameConfigToken, method);

	if (settings::exp_modifier) {
		gain->fields.awardsBonus = settings::new_exp;
		gain->fields.winBonus = settings::new_exp;
		gain->fields.totalExp = settings::new_exp;
	}
	return gain;
}


typedef app::UIPerkSelectionType* (__stdcall* TMenu_SetupPerk)(app::Menu*, app::CharacterPerk*, MethodInfo*);
TMenu_SetupPerk oMenu_SetupPerk = NULL;
app::UIPerkSelectionType* __stdcall hMenu_SetupPerk(app::Menu* __this, app::CharacterPerk* perk, MethodInfo* method) {
	if (settings::unlock_all) {
		perk->fields.cost = 0;
		perk->fields.isOwned = true;
		perk->fields.isHidden = false;
	}
	return oMenu_SetupPerk(__this, perk, method);
}

typedef app::UIOutfitSelectionType* (__stdcall* TMenu_SetupOutfit)(app::Menu*, app::CharacterOutfit*, MethodInfo*);
TMenu_SetupOutfit oMenu_SetupOutfit = NULL;
app::UIOutfitSelectionType* __stdcall hMenu_SetupOutfit(app::Menu* __this, app::CharacterOutfit* outfit, MethodInfo* method) {
	if (settings::unlock_all) {
		outfit->fields.isHidden = false;
		outfit->fields.isOwned = true;
		outfit->fields.isSupporter = true;
	}
	return oMenu_SetupOutfit(__this, outfit, method);
}

typedef app::UIFlashlightSelectionType* (__stdcall* TMenu_SetupFlashlight)(app::Menu*, app::CharacterFlashlight*, MethodInfo*);
TMenu_SetupFlashlight oMenu_SetupFlashlight = NULL;
app::UIFlashlightSelectionType* __stdcall hMenu_SetupFlashlight(app::Menu* __this, app::CharacterFlashlight* flashlight, MethodInfo* method) {
	if (settings::unlock_all) {
		flashlight->fields.isHidden = false;
		flashlight->fields.isOwned = true;
		flashlight->fields.isSupporter = true;
		flashlight->fields.cost = 0;
	}
	return oMenu_SetupFlashlight(__this, flashlight, method);
}

// DO_APP_FUNC(0x00646F00, UIEmoteSelectionType *, Menu_SetupEmote, (Menu * __this, CharacterEmote * emote, MethodInfo * method));
typedef app::UIEmoteSelectionType* (__stdcall* TMenu_SetupEmote)(app::Menu*, app::CharacterEmote*, MethodInfo*);
TMenu_SetupEmote oMenu_SetupEmote = NULL;
app::UIEmoteSelectionType* __stdcall hMenu_SetupEmote(app::Menu* __this, app::CharacterEmote* emote, MethodInfo* method) {
	if (settings::unlock_all) {
		emote->fields.cost = 0;
		emote->fields.isHidden = false;
		emote->fields.isOwned = true;
		emote->fields.isSupporter = true;
		emote->fields.requiresAchievement = false;
		emote->fields.requiresPurchase = false;
	}
	return oMenu_SetupEmote(__this, emote, method);
}


// DO_APP_FUNC(0x00550DD0, bool, SurvivalLobbyController_CanReady, (SurvivalLobbyController* __this, MethodInfo* method));
typedef bool(__stdcall* TSurvivalLobbyController_CanReady)(app::SurvivalLobbyController*, MethodInfo*);
TSurvivalLobbyController_CanReady oSurvivalLobbyController_CanReady = NULL;
bool __stdcall hSurvivalLobbyController_CanReady(app::SurvivalLobbyController* __this, MethodInfo* method) {
	if (settings::unlock_all) {
		__this->fields.m_ready = true;
		return true;
	}

	return oSurvivalLobbyController_CanReady(__this, method);
}

// DO_APP_FUNC(0x005510E0, bool, SurvivalLobbyController_PlayableCharacterSelected, (SurvivalLobbyController* __this, MethodInfo* method));
typedef bool(__stdcall* TSurvivalLobbyController_PlayableCharacterSelected)(app::SurvivalLobbyController*, MethodInfo*);
TSurvivalLobbyController_PlayableCharacterSelected oSurvivalLobbyController_PlayableCharacterSelected = NULL;
bool __stdcall hSurvivalLobbyController_PlayableCharacterSelected(app::SurvivalLobbyController* __this, MethodInfo* method) {
	if (settings::unlock_all)
		return true;

	return oSurvivalLobbyController_PlayableCharacterSelected(__this, method);
}

// DO_APP_FUNC(0x00551670, bool, SurvivalLobbyController_UnlockedCharacterSelected, (SurvivalLobbyController* __this, MethodInfo* method));
typedef bool(__stdcall* TSurvivalLobbyController_UnlockedCharacterSelected)(app::SurvivalLobbyController*, MethodInfo*);
TSurvivalLobbyController_UnlockedCharacterSelected oSurvivalLobbyController_UnlockedCharacterSelected = NULL;
bool __stdcall hSurvivalLobbyController_UnlockedCharacterSelected(app::SurvivalLobbyController* __this, MethodInfo* method) {
	if (settings::unlock_all)
		return true;

	return oSurvivalLobbyController_UnlockedCharacterSelected(__this, method);
}

// DO_APP_FUNC(0x00552DC0, bool, SurvivalLobbyController_AllPlayersReady, (SurvivalLobbyController * __this, MethodInfo * method));
typedef bool(__stdcall* TSurvivalLobbyController_AllPlayersReady)(app::SurvivalLobbyController*, MethodInfo*);
TSurvivalLobbyController_AllPlayersReady oSurvivalLobbyController_AllPlayersReady = NULL;
bool __stdcall hSurvivalLobbyController_AllPlayersReady(app::SurvivalLobbyController* __this, MethodInfo* method) {

	if (settings::unlock_all && IsHost()) {
		app::GameObject_set_active(__this->fields.m_Menu->fields.lobbyChangeCharacterBlocked, true, NULL);
		return true;
	}

	return oSurvivalLobbyController_AllPlayersReady(__this, method);
}

// DO_APP_FUNC(0x00554840, void, SurvivalLobbyController_OnSelectOutfit, (SurvivalLobbyController * __this, CharacterOutfit * outfit, MethodInfo * method));
typedef void(__stdcall* TSurvivalLobbyController_OnSelectOutfit)(app::SurvivalLobbyController*, app::CharacterOutfit*, MethodInfo*);
TSurvivalLobbyController_OnSelectOutfit oSurvivalLobbyController_OnSelectOutfit = NULL;
void __stdcall hSurvivalLobbyController_OnSelectOutfit(app::SurvivalLobbyController* __this, app::CharacterOutfit* outfit, MethodInfo* method) {

	if (settings::unlock_all) {
		outfit->fields.isHidden = false;
		outfit->fields.isOwned = true;
		outfit->fields.isSupporter = true;
	}

	return oSurvivalLobbyController_OnSelectOutfit(__this, outfit, method);
}

// DO_APP_FUNC(0x0059A070, void, UIOutfitSelectionType_SetLocked, (UIOutfitSelectionType * __this, bool locked, MethodInfo * method));
typedef void(__stdcall* TUIOutfitSelectionType_SetLocked)(app::UIOutfitSelectionType*, bool, MethodInfo*);
TUIOutfitSelectionType_SetLocked oUIOutfitSelectionType_SetLocked = NULL;
void __stdcall hUIOutfitSelectionType_SetLocked(app::UIOutfitSelectionType* __this, bool locked, MethodInfo* method) {

	if (settings::unlock_all) {
		locked = false;
		//__this->fields.owned
		__this->fields.outfitType->fields.isHidden = false;
		__this->fields.outfitType->fields.isSupporter = true;
		__this->fields.outfitType->fields.isOwned = true;
	}

	return oUIOutfitSelectionType_SetLocked(__this, locked, method);
}

// DO_APP_FUNC(0x005203A0, bool, SteamInventoryManager_UserHasItem, (SteamInventoryManager * __this, int32_t steamItemDefID, MethodInfo * method));
typedef bool(__stdcall* TSteamInventoryManager_UserHasItem)(app::SteamInventoryManager*, int32_t, MethodInfo*);
TSteamInventoryManager_UserHasItem oSteamInventoryManager_UserHasItem = NULL;
bool __stdcall hSteamInventoryManager_UserHasItem(app::SteamInventoryManager* __this, int32_t steamItemDefID, MethodInfo* method) {

	if (settings::unlock_all) {
		return true;
	}

	return oSteamInventoryManager_UserHasItem(__this, steamItemDefID, method);
}

// DO_APP_FUNC(0x0051E650, bool, SteamInventoryManager_HasRetrievedUserInventoryItems, (SteamInventoryManager * __this, MethodInfo * method));
typedef bool(__stdcall* TSteamInventoryManager_HasRetrievedUserInventoryItems)(app::SteamInventoryManager*, MethodInfo*);
TSteamInventoryManager_HasRetrievedUserInventoryItems oSteamInventoryManager_HasRetrievedUserInventoryItems = NULL;
bool __stdcall hSteamInventoryManager_HasRetrievedUserInventoryItems(app::SteamInventoryManager* __this, MethodInfo* method) {

	if (settings::unlock_all) {
		return true;
	}

	return oSteamInventoryManager_HasRetrievedUserInventoryItems(__this, method);
}


void CreateHooks() {
	/*
	//Exemple :
	MH_STATUS SceneLoadLocalDoneStatus = MH_CreateHook((LPVOID*)test_sig, &hkSceneLoadLocalDone, reinterpret_cast<LPVOID*>(&oSceneLoadLocalDone));
	//We say that for every call to the test_sig address we want to redirect it to the address of the function named test_hook (& gives the pointer to it)
	//We can store the original pointer to the original function into test_org if we want to call the org later --> trampoline hook
	//original_sum can be NULL if we don't want to trampoline hook
	*/

	// DEBUG LOOK
	MH_STATUS status_Debug_Log = MH_CreateHook((LPVOID*)app::Debug_2_Log, &hDebug_Log, reinterpret_cast<LPVOID*>(&oDebug_2_Log));
	if (status_Debug_Log != MH_OK) {
		std::cout << "Failed to create Debug_Log hook: " << MH_StatusToString(status_Debug_Log) << std::endl;
		return;
	}

	// UV HOOK
	MH_STATUS status_uv = MH_CreateHook((LPVOID*)app::NolanBehaviour_OnAttributeUpdateValue, &hNolanBehaviour_OnAttributeUpdateValue, reinterpret_cast<LPVOID*>(&oNolanBehaviour_OnAttributeUpdateValue));
	if (status_uv != MH_OK) {
		std::cout << "Failed to create uv hook: " << MH_StatusToString(status_uv) << std::endl;
		return;
	}

	// NOLAN HOOK
	MH_STATUS status_nolan = MH_CreateHook((LPVOID*)app::NolanBehaviour_FixedUpdate, &hNolanBehaviour_FixedUpdate, reinterpret_cast<LPVOID*>(&oNolanBehaviour_FixedUpdate));
	if (status_nolan != MH_OK) {
		std::cout << "Failed to create nolan hook: " << MH_StatusToString(status_nolan) << std::endl;
		return;
	}

	// XP HOOK
	MH_STATUS status_xp = MH_CreateHook((LPVOID*)app::RankHelpers_CalculateExpGain, &hRankHelpers_CalculateExpGain, reinterpret_cast<LPVOID*>(&oRankHelpers_CalculateExpGain));
	if (status_xp != MH_OK) {
		std::cout << "Failed to create xp hook: " << MH_StatusToString(status_xp) << std::endl;
		return;
	}

	// ROBE HOOK
	MH_STATUS status_robe = MH_CreateHook((LPVOID*)app::OptionsHelpers_IsRobeUnlocked, &hOptionsHelpers_IsRobeUnlocked, reinterpret_cast<LPVOID*>(&oOptionsHelpers_IsRobeUnlocked));
	if (status_robe != MH_OK) {
		std::cout << "Failed to create robe hook: " << MH_StatusToString(status_robe) << std::endl;
		return;
	}

	// Cursor_1_get_visible HOOK
	MH_STATUS status_getVisible = MH_CreateHook((LPVOID*)app::Cursor_1_get_visible, &hCursor_1_get_visible, reinterpret_cast<LPVOID*>(&oCursor_1_get_visible));
	if (status_getVisible != MH_OK) {
		std::cout << "Failed to create Cursor_1_get_visible hook: " << MH_StatusToString(status_getVisible) << std::endl;
		return;
	}

	// Cursor_1_set_visible HOOK
	MH_STATUS status_setVisible = MH_CreateHook((LPVOID*)app::Cursor_1_set_visible, &hCursor_1_set_visible, reinterpret_cast<LPVOID*>(&oCursor_1_set_visible));
	if (status_setVisible != MH_OK) {
		std::cout << "Failed to create Cursor_1_set_visible hook: " << MH_StatusToString(status_setVisible) << std::endl;
		return;
	}

	// Cursor_1_get_lockState HOOK
	MH_STATUS status_getLockState = MH_CreateHook((LPVOID*)app::Cursor_1_get_lockState, &hCursor_1_get_lockState, reinterpret_cast<LPVOID*>(&oCursor_1_get_lockState));
	if (status_getLockState != MH_OK) {
		std::cout << "Failed to create Cursor_1_get_lockState hook: " << MH_StatusToString(status_getLockState) << std::endl;
		return;
	}

	// Cursor_1_set_lockState HOOK
	MH_STATUS status_setLockState = MH_CreateHook((LPVOID*)app::Cursor_1_set_lockState, &hCursor_1_set_lockState, reinterpret_cast<LPVOID*>(&oCursor_1_set_lockState));
	if (status_setLockState != MH_OK) {
		std::cout << "Failed to create Cursor_1_set_lockState hook: " << MH_StatusToString(status_setLockState) << std::endl;
		return;
	}

	// LOCKED HOOK
	MH_STATUS status_CanInteract = MH_CreateHook((LPVOID*)app::LockedInteractable_CanInteract, &hLockedInteractable_CanInteract, reinterpret_cast<LPVOID*>(&oLockedInteractable_CanInteract));
	if (status_CanInteract != MH_OK) {
		std::cout << "Failed to create LockedInteractable hook: " << MH_StatusToString(status_CanInteract) << std::endl;
		return;
	}

	// CHARACTER UNLOCK HOOK
	MH_STATUS status_characterUnlock = MH_CreateHook((LPVOID*)app::OptionsHelpers_IsCharacterUnlocked, &hOptionsHelpers_IsCharacterUnlocked, reinterpret_cast<LPVOID*>(&oOptionsHelpers_IsCharacterUnlocked));
	if (status_characterUnlock != MH_OK) {
		std::cout << "Failed to create character unlock hook: " << MH_StatusToString(status_characterUnlock) << std::endl;
		return;
	}

	// INPUT HOOK (LONGI INTERACT)
	MH_STATUS status_devourInput = MH_CreateHook((LPVOID*)app::DevourInput_GetLongPress, &hDevourInput_GetLongPress, reinterpret_cast<LPVOID*>(&oDevourInput_GetLongPress));
	if (status_devourInput != MH_OK) {
		std::cout << "Failed to create DevourInput " << MH_StatusToString(status_devourInput) << std::endl;
		return;
	}

	// PERKS HOOK
	MH_STATUS status_perks = MH_CreateHook((LPVOID*)app::Menu_SetupPerk, &hMenu_SetupPerk, reinterpret_cast<LPVOID*>(&oMenu_SetupPerk));
	if (status_perks != MH_OK) {
		std::cout << "Failed to create character perks hook: " << MH_StatusToString(status_perks) << std::endl;
		return;
	}

	// OUTFIT HOOK
	MH_STATUS status_outfit = MH_CreateHook((LPVOID*)app::Menu_SetupOutfit, &hMenu_SetupOutfit, reinterpret_cast<LPVOID*>(&oMenu_SetupOutfit));
	if (status_outfit != MH_OK) {
		std::cout << "Failed to create character outfit hook: " << MH_StatusToString(status_outfit) << std::endl;
		return;
	}

	// FLASHLIGHT HOOK
	MH_STATUS status_flashlight = MH_CreateHook((LPVOID*)app::Menu_SetupFlashlight, &hMenu_SetupFlashlight, reinterpret_cast<LPVOID*>(&oMenu_SetupFlashlight));
	if (status_flashlight != MH_OK) {
		std::cout << "Failed to create flashlight hook: " << MH_StatusToString(status_flashlight) << std::endl;
		return;
	}

	// EMOTE HOOK
	MH_STATUS status_emote = MH_CreateHook((LPVOID*)app::Menu_SetupEmote, &hMenu_SetupEmote, reinterpret_cast<LPVOID*>(&oMenu_SetupEmote));
	if (status_emote != MH_OK) {
		std::cout << "Failed to create emote hook: " << MH_StatusToString(status_emote) << std::endl;
		return;
	}

	// LOBBY HOOKS
	// SurvivalLobbyController_CanReady
	MH_STATUS status_canReady = MH_CreateHook((LPVOID*)app::SurvivalLobbyController_CanReady, &hSurvivalLobbyController_CanReady, reinterpret_cast<LPVOID*>(&oSurvivalLobbyController_CanReady));
	if (status_canReady != MH_OK) {
		std::cout << "Failed to create ready hook: " << MH_StatusToString(status_canReady) << std::endl;
		return;
	}

	// SurvivalLobbyController_PlayableCharacterSelected
	MH_STATUS status_playableCharacterSelected = MH_CreateHook((LPVOID*)app::SurvivalLobbyController_PlayableCharacterSelected, &hSurvivalLobbyController_PlayableCharacterSelected, reinterpret_cast<LPVOID*>(&oSurvivalLobbyController_PlayableCharacterSelected));
	if (status_playableCharacterSelected != MH_OK) {
		std::cout << "Failed to create status_playableCharacterSelected hook: " << MH_StatusToString(status_playableCharacterSelected) << std::endl;
		return;
	}

	// SurvivalLobbyController_UnlockedCharacterSelected
	MH_STATUS status_unlockedCharacterSelected = MH_CreateHook((LPVOID*)app::SurvivalLobbyController_UnlockedCharacterSelected, &hSurvivalLobbyController_UnlockedCharacterSelected, reinterpret_cast<LPVOID*>(&oSurvivalLobbyController_UnlockedCharacterSelected));
	if (status_unlockedCharacterSelected != MH_OK) {
		std::cout << "Failed to create unlockedCharacterSelected hook: " << MH_StatusToString(status_unlockedCharacterSelected) << std::endl;
		return;
	}

	// SurvivalLobbyController_AllPlayersReady
	MH_STATUS status_allPlayersReady = MH_CreateHook((LPVOID*)app::SurvivalLobbyController_AllPlayersReady, &hSurvivalLobbyController_AllPlayersReady, reinterpret_cast<LPVOID*>(&oSurvivalLobbyController_AllPlayersReady));
	if (status_allPlayersReady != MH_OK) {
		std::cout << "Failed to create allPlayersReady hook: " << MH_StatusToString(status_allPlayersReady) << std::endl;
		return;
	}

	// SurvivalLobbyController_OnSelectOutfit
	MH_STATUS status_OnSelectOutfit = MH_CreateHook((LPVOID*)app::SurvivalLobbyController_OnSelectOutfit, &hSurvivalLobbyController_OnSelectOutfit, reinterpret_cast<LPVOID*>(&oSurvivalLobbyController_OnSelectOutfit));
	if (status_OnSelectOutfit != MH_OK) {
		std::cout << "Failed to create status_OnSelectOutfit hook: " << MH_StatusToString(status_OnSelectOutfit) << std::endl;
		return;
	}

	// UIOutfitSelectionType_SetLocked
	MH_STATUS status_outfitSetLocked = MH_CreateHook((LPVOID*)app::UIOutfitSelectionType_SetLocked, &hUIOutfitSelectionType_SetLocked, reinterpret_cast<LPVOID*>(&oUIOutfitSelectionType_SetLocked));
	if (status_outfitSetLocked != MH_OK) {
		std::cout << "Failed to create status_outfitSetLocked hook: " << MH_StatusToString(status_outfitSetLocked) << std::endl;
		return;
	}

	// SteamInventoryManager_UserHasItem
	MH_STATUS status_userHasItem = MH_CreateHook((LPVOID*)app::SteamInventoryManager_UserHasItem, &hSteamInventoryManager_UserHasItem, reinterpret_cast<LPVOID*>(&oSteamInventoryManager_UserHasItem));
	if (status_userHasItem != MH_OK) {
		std::cout << "Failed to create status_userHasItem hook: " << MH_StatusToString(status_userHasItem) << std::endl;
		return;
	}

	// SteamInventoryManager_HasRetrievedUserInventoryItems
	MH_STATUS status_HasRetrieved = MH_CreateHook((LPVOID*)app::SteamInventoryManager_HasRetrievedUserInventoryItems, &hSteamInventoryManager_HasRetrievedUserInventoryItems, reinterpret_cast<LPVOID*>(&oSteamInventoryManager_HasRetrievedUserInventoryItems));
	if (status_HasRetrieved != MH_OK) {
		std::cout << "Failed to create status_HasRetrieved hook: " << MH_StatusToString(status_HasRetrieved) << std::endl;
		return;
	}


	// SteamInventoryValidator_PlayerHasItem
	//MH_STATUS status_playerHasItem = MH_CreateHook((LPVOID*)app::SteamInventoryValidator_PlayerHasItem, &hSteamInventoryValidator_PlayerHasItem, reinterpret_cast<LPVOID*>(&oSteamInventoryValidator_PlayerHasItem));
	//if (status_playerHasItem != MH_OK) {
	//	std::cout << "Failed to create status_playerHasItem hook: " << MH_StatusToString(status_playerHasItem) << std::endl;
	//	return;
	//}

	MH_STATUS enable_status_Debug_Log = MH_EnableHook(MH_ALL_HOOKS);
	if (enable_status_Debug_Log != MH_OK) {
		std::cout << "Failed to enable hooks: " << MH_StatusToString(enable_status_Debug_Log) << std::endl;
		return;
	}
}

typedef HRESULT(__stdcall* D3D11PresentHook) (IDXGISwapChain* pSwapChain, UINT SyncInterval, UINT Flags);
D3D11PresentHook phookD3D11Present = NULL;

ID3D11Device* pDevice = NULL;
ID3D11DeviceContext* pContext = NULL;

extern LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);
HWND window = NULL;
WNDPROC oWndProc;
LRESULT __stdcall WndProc(const HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	if (open_menu && ImGui_ImplWin32_WndProcHandler(hWnd, uMsg, wParam, lParam)) {
		return true;
	}

	ImGuiIO& io = ImGui::GetIO();
	if (io.WantCaptureMouse && open_menu) {
		return true;
	}

	return CallWindowProc(oWndProc, hWnd, uMsg, wParam, lParam);
}

static bool pressed = false;
bool initonce = false;
static bool cursor_switch = false;
ID3D11RenderTargetView* mainRenderTargetViewD3D11 = NULL;

HRESULT __stdcall hookD3D11Present(IDXGISwapChain* pSwapChain, UINT SyncInterval, UINT Flags) {
	if (!initonce)
	{
		if (SUCCEEDED(pSwapChain->GetDevice(__uuidof(ID3D11Device), (void**)&pDevice)))
		{
			pDevice->GetImmediateContext(&pContext);
			DXGI_SWAP_CHAIN_DESC sd;
			pSwapChain->GetDesc(&sd);
			window = sd.OutputWindow;
			ID3D11Texture2D* pBackBuffer;
			pSwapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer);
			pDevice->CreateRenderTargetView(pBackBuffer, NULL, &mainRenderTargetViewD3D11);
			pBackBuffer->Release();
			oWndProc = (WNDPROC)SetWindowLongPtr(window, GWLP_WNDPROC, (LONG_PTR)WndProc);
			auto context = ImGui::CreateContext();
			ImGui::SetCurrentContext(context);
			InitStyle();
			ImGui_ImplWin32_Init(window);
			ImGui_ImplDX11_Init(pDevice, pContext);
			initonce = true;
		}
		else
			return phookD3D11Present(pSwapChain, SyncInterval, Flags);
	}

	if (GetKeyDown(KeyCode::Insert)) {
		pressed = true;
	}


	else if (!GetKeyDown(KeyCode::Insert) && pressed) {
		open_menu = !open_menu;
		pressed = false;

		if (IsInGame()) {
			if (open_menu) {
				app::UIHelpers_ShowMouseCursor(NULL);
			}
			else {
				app::UIHelpers_HideMouseCursor(NULL);
			}
		}
	}

	ImGui_ImplDX11_NewFrame();
	ImGui_ImplWin32_NewFrame();
	ImGui::NewFrame();

	/*
	seems that there are issues with opening the menu in the pause menu
	the cursor will still be visible but locked in the middle of the screen
	//TOFIX
	*/

	if (open_menu) {
		DrawMenu(open_menu);
	}
	
	if (settings::player_esp)
		ESP::RunPlayersESP();

	if (settings::fly)
		Misc::Fly(settings::fly_speed);
		
	if (settings::fullBright)
		Misc::FullBright();

	ImGui::GetIO().MouseDrawCursor = open_menu;

	ImGui::EndFrame();
	ImGui::Render();

	ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());

	return phookD3D11Present(pSwapChain, SyncInterval, Flags);
}

DWORD_PTR* pSwapChainVtable = NULL;
DWORD_PTR* pContextVTable = NULL;
DWORD_PTR* pDeviceVTable = NULL;
IDXGISwapChain* pSwapChain;
bool HookDX11() {
	HMODULE hDXGIDLL = GetModuleHandle(L"dxgi.dll");
	while (!hDXGIDLL) {
		hDXGIDLL = GetModuleHandle(L"dxgi.dll");
		Sleep(100);
	}

	oWndProc = (WNDPROC)SetWindowLongPtr(window, GWLP_WNDPROC, (LONG_PTR)WndProc);

	D3D_FEATURE_LEVEL requestedLevels[] = { D3D_FEATURE_LEVEL_11_0, D3D_FEATURE_LEVEL_10_1 };
	D3D_FEATURE_LEVEL obtainedLevel;
	ID3D11Device* d3dDevice = nullptr;
	ID3D11DeviceContext* d3dContext = nullptr;

	DXGI_SWAP_CHAIN_DESC scd;
	ZeroMemory(&scd, sizeof(scd));
	scd.BufferCount = 1;
	scd.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
	scd.BufferDesc.Scaling = DXGI_MODE_SCALING_UNSPECIFIED;
	scd.BufferDesc.ScanlineOrdering = DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED;
	scd.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;

	scd.Flags = DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH;
	scd.OutputWindow = GetForegroundWindow();
	scd.SampleDesc.Count = 1;
	scd.SwapEffect = DXGI_SWAP_EFFECT_DISCARD;
	scd.Windowed = true;

	// LibOVR 0.4.3 requires that the width and height for the backbuffer is set even if
	// you use windowed mode, despite being optional according to the D3D11 documentation.
	scd.BufferDesc.Width = 1;
	scd.BufferDesc.Height = 1;
	scd.BufferDesc.RefreshRate.Numerator = 0;
	scd.BufferDesc.RefreshRate.Denominator = 1;

	UINT createFlags = 0;
#ifdef _DEBUG
	// This flag gives you some quite wonderful debug text. Not wonderful for performance, though!
	createFlags |= D3D11_CREATE_DEVICE_DEBUG;
#endif

	IDXGISwapChain* d3dSwapChain = 0;

	if (FAILED(D3D11CreateDeviceAndSwapChain(
		nullptr,
		D3D_DRIVER_TYPE_HARDWARE,
		nullptr,
		createFlags,
		requestedLevels,
		sizeof(requestedLevels) / sizeof(D3D_FEATURE_LEVEL),
		D3D11_SDK_VERSION,
		&scd,
		&pSwapChain,
		&pDevice,
		&obtainedLevel,
		&pContext)))
	{
		return false;
	}


	pSwapChainVtable = (DWORD_PTR*)pSwapChain;
	pSwapChainVtable = (DWORD_PTR*)pSwapChainVtable[0];

	pContextVTable = (DWORD_PTR*)pContext;
	pContextVTable = (DWORD_PTR*)pContextVTable[0];

	pDeviceVTable = (DWORD_PTR*)pDevice;
	pDeviceVTable = (DWORD_PTR*)pDeviceVTable[0];

	if (MH_CreateHook((DWORD_PTR*)pSwapChainVtable[8], hookD3D11Present, reinterpret_cast<void**>(&phookD3D11Present)) != MH_OK) { return false; }
	if (MH_EnableHook((DWORD_PTR*)pSwapChainVtable[8]) != MH_OK) { return false; }

	DWORD dwOld;
	VirtualProtect(phookD3D11Present, 2, PAGE_EXECUTE_READWRITE, &dwOld);

	return true;
}

bool InitializeHooks() {
	return MH_Initialize() == MH_OK;
}

void DisableHooks() {
	SetWindowLongPtr(window, GWLP_WNDPROC, (LONG_PTR)(oWndProc));
	if (pSwapChain) { pSwapChain->Release(); }
	if (mainRenderTargetViewD3D11) { mainRenderTargetViewD3D11->Release(); mainRenderTargetViewD3D11 = NULL; }
	if (pContext) { pContext->Release(); pContext = NULL; }
	if (pDevice) { pDevice->Release(); pDevice = NULL; }
	MH_DisableHook(MH_ALL_HOOKS);
	MH_Uninitialize();
	ImGui_ImplDX11_Shutdown();
	ImGui_ImplWin32_Shutdown();
	ImGui::DestroyContext();
}