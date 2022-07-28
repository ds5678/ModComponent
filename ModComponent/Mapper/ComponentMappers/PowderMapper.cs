extern alias Hinterland;
using Hinterland;
using ModComponent.API.Components;
using ModComponent.Utils;

namespace ModComponent.Mapper.ComponentMappers
{
	internal static class PowderMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModPowderComponent modPowderComponent = modComponent.TryCast<ModPowderComponent>();
			if (modPowderComponent == null) return;

			PowderItem powderItem = ComponentUtils.GetOrCreateComponent<PowderItem>(modComponent);
			powderItem.m_Type = ModPowderComponent.GetPowderType(modPowderComponent.PowderType);
			powderItem.m_WeightLimitKG = modPowderComponent.CapacityKG;
			powderItem.m_WeightKG = modPowderComponent.CapacityKG;
		}
	}
}