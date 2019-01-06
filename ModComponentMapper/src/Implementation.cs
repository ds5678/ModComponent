using System.Reflection;

namespace ModComponentMapper
{
    public class Implementation
    {
        public static event SceneReady OnSceneReady;

        public static void OnLoad()
        {
            LogUtils.Log("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);

            AutoMapper.Initialize();
            ModHealthManager.Initialize();
            GearSpawner.Initialize();
            BlueprintReader.Initialize();
        }

        internal static void SceneReady()
        {
            LogUtils.Log("Invoking 'SceneReady' for scene '{0}' ...", GameManager.m_ActiveScene);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            OnSceneReady?.Invoke();

            stopwatch.Stop();
            LogUtils.Log("Completed 'SceneReady' for scene '{0}' in {1} ms", GameManager.m_ActiveScene, stopwatch.ElapsedMilliseconds);
        }
    }
}