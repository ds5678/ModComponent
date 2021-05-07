namespace ModComponentMapper
{
	internal class TestFunctions
	{
		internal static void TestFunction()
		{
			foreach (var _ in new int[1000])
			{
				//Logger.Log(RandomUtils.RandomInt().ToString());
				//Logger.Log(RandomUtils.RandomDouble().ToString());
				Logger.Log(RandomUtils.RandomFloat().ToString());
			}
			foreach (var _ in new int[1000])
			{
				Logger.Log(RandomUtils.Range(0, 27).ToString());
			}
		}
	}
}
