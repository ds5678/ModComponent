namespace ModComponentMain
{
	internal static class Injections
	{
		private static void Inject<T>() where T : class
		{
			UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<T>(false);
		}

		internal static void InjectAll()
		{
			API_Component_Injections();
			API_Behaviour_Injections();
			API_FireMaking_Injections();
			API_Special_Injections();
			SceneLoader_Injections();
		}

		private static void API_FireMaking_Injections()
		{
			Inject<ModComponentAPI.ModFireMakingComponent>();
			Inject<ModComponentAPI.ModAccelerantComponent>();
			Inject<ModComponentAPI.ModBurnableComponent>();
			Inject<ModComponentAPI.ModFireStarterComponent>();
			Inject<ModComponentAPI.ModTinderComponent>();
		}

		private static void API_Behaviour_Injections()
		{
			Inject<ModComponentAPI.ModCarryingCapacityComponent>();
			Inject<ModComponentAPI.ModEvolveComponent>();
			Inject<ModComponentAPI.ModHarvestableComponent>();
			Inject<ModComponentAPI.ModMillableComponent>();
			Inject<ModComponentAPI.ModRepairableComponent>();
			Inject<ModComponentAPI.ModScentComponent>();
			Inject<ModComponentAPI.ModSharpenableComponent>();
			Inject<ModComponentAPI.ModStackableComponent>();
		}

		private static void API_Component_Injections()
		{
			Inject<ModComponentAPI.ChangeLayer>();
			Inject<ModComponentAPI.ModSaveBehaviour>();
			Inject<ModComponentAPI.ModComponent>();
			Inject<ModComponentAPI.EquippableModComponent>();
			Inject<ModComponentAPI.ModBedComponent>();
			Inject<ModComponentAPI.ModBodyHarvestComponent>();
			Inject<ModComponentAPI.ModCharcoalComponent>();
			Inject<ModComponentAPI.ModClothingComponent>();
			Inject<ModComponentAPI.ModCollectibleComponent>();
			Inject<ModComponentAPI.ModCookableComponent>();
			Inject<ModComponentAPI.ModCookingPotComponent>();
			Inject<ModComponentAPI.ModExplosiveComponent>();
			Inject<ModComponentAPI.ModExplosiveSave>();
			Inject<ModComponentAPI.ModFirstAidComponent>();
			Inject<ModComponentAPI.ModFoodComponent>();
			Inject<ModComponentAPI.ModGenericComponent>();
			Inject<ModComponentAPI.ModGenericEquippableComponent>();
			Inject<ModComponentAPI.ModLiquidComponent>();
			Inject<ModComponentAPI.ModPowderComponent>();
			Inject<ModComponentAPI.ModPurificationComponent>();
			Inject<ModComponentAPI.ModRandomItemComponent>();
			Inject<ModComponentAPI.ModRandomWeightedItemComponent>();
			Inject<ModComponentAPI.ModResearchComponent>();
			Inject<ModComponentAPI.ModRifleComponent>();
			Inject<ModComponentAPI.ModToolComponent>();
		}

		private static void API_Special_Injections()
		{
			Inject<ModComponentAPI.AddTag>();
			Inject<ModComponentAPI.AlternativeAction>();
			Inject<ModComponentAPI.AttachBehaviour>();
			Inject<ModComponentAPI.ModSkill>();
			Inject<ModComponentAPI.PlayAkSound>();
		}

		private static void SceneLoader_Injections()
		{
			Inject<SceneLoader.Shaders.SubstituteShadersSingle>();
			Inject<SceneLoader.Shaders.SubstituteShadersRecursive>();
		}
	}
}
