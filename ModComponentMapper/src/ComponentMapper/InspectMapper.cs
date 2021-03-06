﻿using ModComponentAPI;
using UnityEngine;

//did a first pass through; didn 't find anything
//does not need to be declared

namespace ModComponentMapper.ComponentMapper
{
    internal class InspectMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            if (!modComponent.InspectOnPickup)
            {
                return;
            }

            Inspect inspect = ModUtils.GetOrCreateComponent<Inspect>(modComponent);
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
}