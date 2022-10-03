#ifndef SETTINGS_HPP
#define SETTINGS_HPP

#include <imgui/imgui.h>
#include <string>

namespace settings {
	extern int height;
	extern int width;

	extern bool big_flashlight;
	extern float flashlight_color[4];

	extern bool unlimited_uv;
	extern bool unlimited_uv_reset;
	extern bool fullbright;

	extern bool player_esp;
	extern float player_esp_color[4];
	extern bool player_snaplines;
	extern float player_snaplines_color[4];
	extern bool azazel_esp;
	extern float azazel_esp_color[4];
	extern bool azazel_snaplines;
	extern float azazel_snaplines_color[4];
	extern bool item_esp;
	extern float item_esp_color[4];
	extern bool demon_esp;
	extern float demon_esp_color[4];
	extern bool goat_esp;
	extern float goat_esp_color[4];

	extern bool chat_spam;
	extern std::string message;
	extern bool spoof_level;
	extern int new_level;
	extern bool steam_name_spoof;
	extern std::string new_name;
	extern bool server_name_spoof;
	extern std::string server_name;
	extern bool fly;
	extern bool unlock_all;
	extern bool exp_modifier;
	extern int new_exp;
	extern bool walk_in_lobby;
	extern bool auto_respawn;
	extern bool change_player_speed;
	extern int new_speed;
}

#endif
