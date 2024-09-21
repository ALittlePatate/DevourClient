#pragma once

namespace Transform {
	app::Transform* GetTransform(app::GameObject* go);
	
	// e.g. NolanBehaviour
	template<typename T>
	inline app::Transform* GetTransform(T* component);

	app::Vector3 GetPosition(app::Transform* transform);
	app::Quaternion GetRotation(app::Transform* transform);
	app::Vector3 GetForward(app::Transform* transform);
	app::Vector3 GetRight(app::Transform* transform);
	app::Vector3 GetEulerAngles(app::Quaternion* rotation);
	app::Quaternion QuaternionEuler(app::Vector3 eulerAngles);
	app::Quaternion QuaternionIdentity();

	template<typename T>
	app::Transform* GetTransform(T* component)
	{
		if (!component) return nullptr;

		if (app::Component_get_transform != nullptr) {
			return app::Component_get_transform((app::Component*)component, nullptr);
		}

		return nullptr;
	}
}