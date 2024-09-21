#include "pch-il2cpp.h"

#include "UnityEngine/Transform.h"
#include <UnityEngine/Object.h>

app::Transform* Transform::GetTransform(app::GameObject* go)
{
	if (Object::IsNull(reinterpret_cast<app::Object_1*>(go)) || !app::GameObject_get_transform) return nullptr;
	app::Transform* __transform = app::GameObject_get_transform(go, nullptr);

	return __transform ? __transform : nullptr;
}

app::Vector3 Transform::GetPosition(app::Transform* transform)
{
	if (!transform || !app::Transform_get_position) return app::Vector3();

	return app::Transform_get_position(transform, nullptr);
}

app::Quaternion Transform::GetRotation(app::Transform* transform)
{
	return app::Transform_get_rotation(transform, nullptr);
}

app::Vector3 Transform::GetForward(app::Transform* transform)
{
	return app::Transform_get_forward(transform, nullptr);
}

app::Vector3 Transform::GetRight(app::Transform* transform)
{
	return app::Transform_get_right(transform, nullptr);
}

app::Vector3 Transform::GetEulerAngles(app::Quaternion* rotation)
{
	return app::Quaternion_get_eulerAngles(rotation, nullptr);
}

app::Quaternion Transform::QuaternionEuler(app::Vector3 eulerAngles)
{
	float x = eulerAngles.x;
	float y = eulerAngles.y;
	float z = eulerAngles.z;

	return app::Quaternion_Euler(x, y, z, nullptr);
}

app::Quaternion Transform::QuaternionIdentity()
{
	return app::Quaternion_get_identity(nullptr);
}
