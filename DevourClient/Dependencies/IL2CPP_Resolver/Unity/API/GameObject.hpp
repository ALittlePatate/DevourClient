#pragma once

namespace Unity
{
	struct SGameObjectFunctions
	{
		void* m_pAddComponent = nullptr;
		void* m_pCreatePrimitive = nullptr;
		void* m_pFind = nullptr;
		void* m_pFindGameObjectsWithTag = nullptr;
		void* m_pGetComponent = nullptr;
		void* m_pGetComponents = nullptr;
		void* m_pGetActive = nullptr;
		void* m_pGetLayer = nullptr;
		void* m_pGetTransform = nullptr;
		void* m_pSetActive = nullptr;
		void* m_pSetLayer = nullptr;
	};
	extern SGameObjectFunctions GameObjectFunctions;

	class CGameObject : public CObject
	{
	public:
		void AddComponent(il2cppObject* m_pSystemType)
		{
			reinterpret_cast<void(UNITY_CALLING_CONVENTION)(void*, void*)>(GameObjectFunctions.m_pAddComponent)(this, m_pSystemType);
		}

		CComponent* GetComponent(const char* m_pName)
		{
			return reinterpret_cast<CComponent*(UNITY_CALLING_CONVENTION)(void*, System_String*)>(GameObjectFunctions.m_pGetComponent)(this, IL2CPP::String::New(m_pName));
		}

		il2cppArray<CComponent*>* GetComponents(il2cppObject* m_pSystemType)
		{
			/* 
			0 - Object
			1 - Type
			2 - Use search type as array return type
			3 - Recursive
			4 - Include inactive
			5 - Reverse
			6 - Result list
			*/
			return reinterpret_cast<Unity::il2cppArray<CComponent*>*(UNITY_CALLING_CONVENTION)(void*, void*, bool, bool, bool, bool, void*)>(GameObjectFunctions.m_pGetComponents)(this, m_pSystemType, false, false, true, false, nullptr);
		}

		il2cppArray<CComponent*>* GetComponents(const char* m_pSystemTypeName)
		{
			il2cppClass* m_pClass = IL2CPP::Class::Find(m_pSystemTypeName);
			if (!m_pClass) return nullptr;

			return GetComponents(IL2CPP::Class::GetSystemType(m_pClass));
		}

		CComponent* GetComponentByIndex(il2cppObject* m_pSystemType, unsigned int m_uIndex = 0U)
		{
			il2cppArray<CComponent*>* m_pComponents = GetComponents(m_pSystemType);
			if (!m_pComponents || m_uIndex >= m_pComponents->m_uMaxLength) return nullptr;

			return m_pComponents->operator[](m_uIndex);
		}

		CComponent* GetComponentByIndex(const char* m_pSystemTypeName, unsigned int m_uIndex = 0U)
		{
			il2cppClass* m_pClass = IL2CPP::Class::Find(m_pSystemTypeName);
			if (!m_pClass) return nullptr;

			return GetComponentByIndex(IL2CPP::Class::GetSystemType(m_pClass), m_uIndex);
		}

		CTransform* GetTransform()
		{
			return reinterpret_cast<CTransform*(UNITY_CALLING_CONVENTION)(void*)>(GameObjectFunctions.m_pGetTransform)(this);
		}

		bool GetActive()
		{
			return reinterpret_cast<bool(UNITY_CALLING_CONVENTION)(void*)>(GameObjectFunctions.m_pGetActive)(this);
		}

		unsigned int GetLayer()
		{
			return reinterpret_cast<unsigned int(UNITY_CALLING_CONVENTION)(void*)>(GameObjectFunctions.m_pGetLayer)(this);
		}

		/*
		* (WARNING) when you use GameObject::Find and you set the object to unactive, you won't find it anymore with that slow function.
		*/
		void SetActive(bool m_bActive)
		{
			reinterpret_cast<void(UNITY_CALLING_CONVENTION)(void*, bool)>(GameObjectFunctions.m_pSetActive)(this, m_bActive);
		}

		void SetLayer(unsigned int m_uLayer)
		{
			reinterpret_cast<void(UNITY_CALLING_CONVENTION)(void*, unsigned int)>(GameObjectFunctions.m_pSetLayer)(this, m_uLayer);
		}
	};

	namespace GameObject
	{
		enum class m_ePrimitiveType : int
		{
			Default = 0,
			Sphere = 0,
			Capsule,
			Cylinder,
			Cube,
			Plane,
			Quad,
		};

		void Initialize();
		
		CGameObject* CreatePrimitive(m_ePrimitiveType m_eType);

		CGameObject* Find(const char* m_pName);
		
		il2cppArray<CGameObject*>* FindWithTag(const char* m_pTag);
	}
}
