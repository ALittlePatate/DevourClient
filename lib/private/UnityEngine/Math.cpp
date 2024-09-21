#include "pch-il2cpp.h"

#include "UnityEngine/Math.h"
#include <helpers.h>

const char* Math::Vector3::ToString(app::Vector3* v)
{
	app::String* str = app::Vector3_ToString(v, nullptr);

	return str ? il2cppi_to_string(str).c_str() : "Vector::ToString returned nullptr!\n";
}

const char* Math::Vector3::ToString(app::Vector3 v)
{
	return ("x: " + std::to_string(v.x) + " y: " + std::to_string(v.y) + " z: " + std::to_string(v.z)).c_str();
}

namespace app {
	Vector3 operator+(const Vector3& lhs, const Vector3& rhs) {
		return { lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z };
	}

	Vector3 operator*(const Vector3& vec, float scalar) {
		return { vec.x * scalar, vec.y * scalar, vec.z * scalar };
	}

	Vector3 operator-(const Vector3& lhs, const Vector3& rhs) {
		return { lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z };
	}
}
