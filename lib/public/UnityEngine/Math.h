#pragma once

namespace Math {
	namespace Vector3 {
		const char* ToString(app::Vector3* v);
		const char* ToString(app::Vector3 v);
	}
}

namespace app {
	Vector3 operator+(const Vector3& lhs, const Vector3& rhs);
	Vector3 operator*(const Vector3& vec, float scalar);
	Vector3 operator-(const Vector3& lhs, const Vector3& rhs);
}
