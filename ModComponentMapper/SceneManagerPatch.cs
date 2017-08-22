using UnityEngine.SceneManagement;

namespace ModComponentMapper
{
    internal class SceneManagerPatch
    {
        public static void OnLoad()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene != null && !string.IsNullOrEmpty(scene.name) && mode == LoadSceneMode.Single)
            {
                GearSpawner.SpawnGearForScene(scene.name);
            }
        }
    }
}
