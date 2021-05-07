namespace ModComponentMapper
{
	public class AlcoholUptake
	{
		public float amountPerGameSecond;

		public float remainingGameSeconds;

		public static AlcoholUptake Create(float amount, float gameSeconds)
		{
			AlcoholUptake result = new AlcoholUptake();

			result.amountPerGameSecond = amount / gameSeconds;
			result.remainingGameSeconds = gameSeconds;

			return result;
		}
	}

	public class ModHealthManagerData
	{
		public float alcoholPermille;
		public AlcoholUptake[] uptakes;
	}

	public class SaveProxy
	{
		public string data;

		public SaveProxy()
		{
			this.data = "";
		}
	}
}
