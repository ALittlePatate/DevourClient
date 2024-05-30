#include "pch-il2cpp.h"
#include "settings.hpp"

namespace settings {
	int height = 1080;
	int width = 1920;

	bool fullBright = false;
	float flashlight_color[4] = { 255.f, 255.f, 255.f, 255.f };

	bool unlimited_uv = false;
	bool fullbright = false;

	bool player_esp = false;
	float player_esp_color[4] = { 0, 255.f, 0, 255.f };
	bool player_snaplines = false;
	float player_snaplines_color[4] = { 0, 255.f, 0, 255.f };
	bool azazel_esp = false;
	float azazel_esp_color[4] = { 255.f, 0, 0, 255.f };
	bool azazel_snaplines = false;
	float azazel_snaplines_color[4] = { 255.f, 0, 0, 255.f };
	bool item_esp = false;
	float item_esp_color[4] = { 255.f, 255.f, 255.f, 255.f };
	bool demon_esp = false;
	float demon_esp_color[4] = { 255.f, 0, 0, 255.f };
	bool goat_esp = false;
	float goat_esp_color[4] = { 0, 255.f, 0, 255.f };

	bool chat_spam = false;
	std::string message = "deez nuts";
	bool spoof_level = false;
	int new_level = 0;
	bool steam_name_spoof = false;
	std::string new_name = "patate";
	bool server_name_spoof = false;
	std::string server_name = "Jadis0x";
	bool fly = false;
	float fly_speed = 5.f;
	bool unlock_all = true;
	bool exp_modifier = false;
	int new_exp = 2000;
	bool walk_in_lobby = false;
	bool auto_respawn = false;
	bool change_player_speed = false;
	int new_speed = 1;

	bool freeze_azazel = false;
	float new_azazel_speed = 0.f;

	bool disable_longInteract = false;
}
