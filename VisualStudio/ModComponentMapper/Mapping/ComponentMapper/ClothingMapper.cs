using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
	internal static class ClothingMapper
	{
		internal static void Configure(ModComponent modComponent)
		{
			ModClothingComponent modClothingItem = modComponent.TryCast<ModClothingComponent>();
			if (modClothingItem == null) return;

			ClothingItem clothingItem = ModComponentUtils.ComponentUtils.GetOrCreateComponent<ClothingItem>(modClothingItem);

			clothingItem.m_DailyHPDecayWhenWornInside = Mapper.GetDecayPerStep(modClothingItem.DaysToDecayWornInside, modClothingItem.MaxHP);
			clothingItem.m_DailyHPDecayWhenWornOutside = Mapper.GetDecayPerStep(modClothingItem.DaysToDecayWornOutside, modClothingItem.MaxHP);
			clothingItem.m_DryBonusWhenNotWorn = 1.5f;
			clothingItem.m_DryPercentPerHour = 100f / modClothingItem.HoursToDryNearFire;
			clothingItem.m_DryPercentPerHourNoFire = 100f / modClothingItem.HoursToDryWithoutFire;
			clothingItem.m_FreezePercentPerHour = 100f / modClothingItem.HoursToFreeze;

			clothingItem.m_Region = ModComponentUtils.EnumUtils.TranslateEnumValue<ClothingRegion, Region>(modClothingItem.Region);
			clothingItem.m_MaxLayer = ModComponentUtils.EnumUtils.TranslateEnumValue<ClothingLayer, Layer>(modClothingItem.MaxLayer);
			clothingItem.m_MinLayer = ModComponentUtils.EnumUtils.TranslateEnumValue<ClothingLayer, Layer>(modClothingItem.MinLayer);
			clothingItem.m_FootwearType = ModComponentUtils.EnumUtils.TranslateEnumValue<FootwearType, Footwear>(modClothingItem.Footwear);
			clothingItem.m_WornMovementSoundCategory = ModComponentUtils.EnumUtils.TranslateEnumValue<ClothingMovementSound, MovementSound>(modClothingItem.MovementSound);

			clothingItem.m_PaperDollTextureName = modClothingItem.MainTexture;
			clothingItem.m_PaperDollBlendmapName = modClothingItem.BlendTexture;

			clothingItem.m_Warmth = modClothingItem.Warmth;
			clothingItem.m_WarmthWhenWet = modClothingItem.WarmthWhenWet;
			clothingItem.m_Waterproofness = modClothingItem.Waterproofness / 100f;
			clothingItem.m_Windproof = modClothingItem.Windproof;
			clothingItem.m_SprintBarReductionPercent = modClothingItem.SprintBarReduction;
			clothingItem.m_Toughness = modClothingItem.Toughness;

			ConfigureWolfIntimidation(modClothingItem);
		}

		private static void ConfigureWolfIntimidation(ModClothingComponent modClothingItem)
		{
			if (modClothingItem.DecreaseAttackChance == 0 && modClothingItem.IncreaseFleeChance == 0)
			{
				return;
			}

			WolfIntimidationBuff wolfIntimidationBuff = ModComponentUtils.ComponentUtils.GetOrCreateComponent<WolfIntimidationBuff>(modClothingItem);
			wolfIntimidationBuff.m_DecreaseAttackChancePercentagePoints = modClothingItem.DecreaseAttackChance;
			wolfIntimidationBuff.m_IncreaseFleePercentagePoints = modClothingItem.IncreaseFleeChance;
		}
	}
}