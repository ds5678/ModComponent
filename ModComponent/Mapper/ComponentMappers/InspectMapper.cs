﻿using Il2Cpp;
using ModComponent.API.Components;

namespace ModComponent.Mapper.ComponentMappers;

internal static class InspectMapper
{
	internal static void Configure(ModBaseComponent modComponent)
	{
		if (!modComponent.InspectOnPickup)
		{
			return;
		}

		Inspect inspect = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<Inspect>(modComponent);
		inspect.m_DistanceFromCamera = modComponent.InspectDistance;
		inspect.m_Scale = modComponent.InspectScale;
		inspect.m_Angles = modComponent.InspectAngles;
		inspect.m_Offset = modComponent.InspectOffset;

		if (modComponent.InspectModel != null && modComponent.NormalModel != null)
		{
			inspect.m_NormalMesh = modComponent.NormalModel;
			inspect.m_NormalMesh.SetActive(true);

			inspect.m_InspectModeMesh = modComponent.InspectModel;
			inspect.m_InspectModeMesh.SetActive(false);
		}
	}
}