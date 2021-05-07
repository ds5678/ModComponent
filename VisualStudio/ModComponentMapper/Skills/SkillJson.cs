using MelonLoader.TinyJSON;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper
{
	internal static class SkillJson
	{
		public static void InitializeModSkill(ref GameObject prefab)
		{
			if (prefab == null || prefab.GetComponent<ModSkill>() != null) return;

			string data = JsonHandler.GetJsonText(prefab.name);
			ProxyObject dict = JSON.Load(data) as ProxyObject;
			InitializeModSkill(ref prefab, dict);
		}

		public static void InitializeModSkill(ref GameObject prefab, ProxyObject dict, string className = "ModSkill")
		{
			if (prefab == null || prefab.GetComponent<ModSkill>() != null || !JsonUtils.ContainsKey(dict, className)) return;
			ModSkill newSkill = prefab.AddComponent<ModSkill>();
			newSkill.DisplayName = dict[className]["DisplayName"];
			newSkill.Icon = dict[className]["Icon"];
			newSkill.Image = dict[className]["Image"];

			newSkill.PointsLevel2 = dict[className]["PointsLevel2"];
			newSkill.PointsLevel3 = dict[className]["PointsLevel3"];
			newSkill.PointsLevel4 = dict[className]["PointsLevel4"];
			newSkill.PointsLevel5 = dict[className]["PointsLevel5"];
			newSkill.DescriptionLevel1 = dict[className]["DescriptionLevel1"];
			newSkill.DescriptionLevel2 = dict[className]["DescriptionLevel2"];
			newSkill.DescriptionLevel3 = dict[className]["DescriptionLevel3"];
			newSkill.DescriptionLevel4 = dict[className]["DescriptionLevel4"];
			newSkill.DescriptionLevel5 = dict[className]["DescriptionLevel5"];
			newSkill.EffectsLevel1 = dict[className]["EffectsLevel1"];
			newSkill.EffectsLevel2 = dict[className]["EffectsLevel2"];
			newSkill.EffectsLevel3 = dict[className]["EffectsLevel3"];
			newSkill.EffectsLevel4 = dict[className]["EffectsLevel4"];
			newSkill.EffectsLevel5 = dict[className]["EffectsLevel5"];
		}
	}
}
