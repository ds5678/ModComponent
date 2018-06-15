using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace ModComponentMapper
{
    public class RestoreMaterialQueue : MonoBehaviour
    {
        private List<int> queues = new List<int>();

        public void Awake()
        {
            foreach (Renderer eachRenderer in this.GetComponentsInChildren<MeshRenderer>(true))
            {
                foreach (Material eachMaterial in eachRenderer.materials)
                {
                    queues.Add(eachMaterial.renderQueue);
                }
            }
        }

        public void OnDestroy()
        {
            int queueIndex = 0;
            foreach (Renderer eachRenderer in this.GetComponentsInChildren<MeshRenderer>(true))
            {
                foreach (Material eachMaterial in eachRenderer.materials)
                {
                    eachMaterial.renderQueue = this.queues[queueIndex++];
                }
            }
        }
    }
}
