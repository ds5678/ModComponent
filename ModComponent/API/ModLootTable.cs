using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	internal class ModLootTable : LootTable
	{
		string[] prefabNames = new string[0];
		int[] weights = new int[0];

		public ModLootTable(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			//Set variable values here

			int len = System.Math.Min(prefabNames.Length, weights.Length);
			m_Prefabs = new List<GameObject>();
			m_Weights = new List<int>();
			for (int i = 0; i < len; i++)
			{
				GameObject go = Resources.Load(prefabNames[i])?.TryCast<GameObject>();
				if (go == null || weights[i] <= 0) continue;
				else
				{
					m_Prefabs.Add(go);
					m_Weights.Add(weights[i]);
				}
			}
		}
	}
}
