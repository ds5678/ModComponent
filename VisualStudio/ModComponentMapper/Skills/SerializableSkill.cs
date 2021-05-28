using UnhollowerBaseLib.Attributes;

namespace ModComponentMapper
{
	public class SerializableSkill : Skill
	{
		internal static bool registeredInIl2Cpp = false;
		public SerializableSkill(System.IntPtr intPtr) : base(intPtr) { }
		public static LocalizedString[] CreateLocalizedStrings(string prefix)
		{
			LocalizedString[] result = new LocalizedString[5];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = new LocalizedString() { m_LocalizationID = prefix + (i + 1) };
			}

			return result;
		}

		public static string GetSkillSaveKey(Skill skill)
		{
			if (skill is null)
			{
				//Logger.Log("GetSkillSaveKey returned null");
				return null;
			}
			//if (skill.m_SkillType <= SkillType.ToolRepair)
			//{
			return "m_" + skill.name + "Serialized";
			//}

			//return skill.name;
		}

		public static void MaybeRegisterInIl2Cpp()
		{
			if (!registeredInIl2Cpp)
			{
				UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<SerializableSkill>();
				registeredInIl2Cpp = true;
			}
		}

		[HideFromIl2Cpp]
		public void Deserialize(string text)
		{
			SkillData skillData = MelonLoader.TinyJSON.JSON.Load(text).Make<SkillData>();
			this.SetPoints(skillData.Points, SkillsManager.PointAssignmentMode.AssignInAnyMode);
		}

		[HideFromIl2Cpp]
		public string Serialize()
		{
			SkillData skillData = new SkillData();
			skillData.Points = this.GetPoints();

			return MelonLoader.TinyJSON.JSON.Dump(skillData);
		}
	}

	public class SkillData
	{
		public int Points;
	}
}
