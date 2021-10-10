using MelonLoader.TinyJSON;

namespace ModComponentMapper.SaveData
{
	internal class SaveProxy
	{
		public string data;

		public SaveProxy() => data = "";
		public SaveProxy(string data) => this.data = data;

		internal string DumpJson()
		{
			return JSON.Dump(this, EncodeOptions.NoTypeHints);
		}

		public static SaveProxy ParseJson(string jsonText)
		{
			var result = new SaveProxy();
			var dict = JSON.Load(jsonText) as ProxyObject;
			result.data = dict["data"];
			return result;
		}
	}
}