using Il2Cpp;
using Il2CppCollections = Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace ModComponent.API;

[MelonLoader.RegisterTypeInIl2Cpp]
internal class ModLootTable : LootTable
{
	string[] prefabNames = System.Array.Empty<string>();
	int[] weights = System.Array.Empty<int>();

	public ModLootTable(System.IntPtr intPtr) : base(intPtr) { }

	void Awake()
	{
		//Set variable values here

		int len = System.Math.Min(prefabNames.Length, weights.Length);
		m_Prefabs = new Il2CppCollections.List<GameObject>();
		m_Weights = new Il2CppCollections.List<int>();
		for (int i = 0; i < len; i++)
		{
			GameObject? go = Resources.Load(prefabNames[i])?.TryCast<GameObject>();
			if (go == null || weights[i] <= 0)
			{
				continue;
			}
			else
			{
				m_Prefabs.Add(go);
				m_Weights.Add(weights[i]);
			}
		}
	}
}
