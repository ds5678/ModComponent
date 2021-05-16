using System;
using System.Security.Cryptography;

namespace ModComponentUtils
{
	public static class RandomUtils
	{
		private static readonly RNGCryptoServiceProvider cryptoRandom = new RNGCryptoServiceProvider();

		public static int Range(int min, int max, bool maxInclusive)
		{
			if (maxInclusive) return Range(min, max + 1);
			else return Range(min, max);
		}

		/// <summary>
		/// Uniformly chooses a random integer in a given range.
		/// </summary>
		/// <param name="min">inclusive</param>
		/// <param name="max">exclusive</param>
		/// <returns>A random integer between the min and the max.</returns>
		public static int Range(int min, int max)
		{
			if (min > max)
			{
				int newMin = max;
				int newMax = min;
				min = newMin;
				max = newMax;
			}
			if (min == max || min == max - 1) return min;
			int result = (int)Math.Floor(RandomFloat() * (max - min) + min);
			if (result >= max) result = max - 1;
			return result;
		}

		public static float Range(float min, float max)
		{
			return RandomFloat() * (max - min) + min;
		}

		/// <summary>
		/// Rolls the chance that an action is successful.
		/// </summary>
		/// <param name="percent">Chance of success between 0 and 100</param>
		/// <returns>True if successful and false otherwise.</returns>
		public static bool RollChance(float percent)
		{
			if (percent <= 0) return false;
			else if (percent >= 100) return true;
			else return RandomFloat() < UnityEngine.Mathf.Clamp01(percent / 100);
		}

		public static int RandomInt()
		{
			byte[] byteArray = new byte[4];
			cryptoRandom.GetBytes(byteArray);
			return System.BitConverter.ToInt32(byteArray, 0);
		}

		public static long RandomLong()
		{
			byte[] byteArray = new byte[8];
			cryptoRandom.GetBytes(byteArray);
			return System.BitConverter.ToInt64(byteArray, 0);
		}

		public static uint RandomUInt()
		{
			byte[] byteArray = new byte[4];
			cryptoRandom.GetBytes(byteArray);
			return System.BitConverter.ToUInt32(byteArray, 0);
		}

		public static ulong RandomULong()
		{
			byte[] byteArray = new byte[8];
			cryptoRandom.GetBytes(byteArray);
			return System.BitConverter.ToUInt64(byteArray, 0);
		}

		public static float RandomFloat()
		{
			//byte[] byteArray = new byte[4];
			//cryptoRandom.GetBytes(byteArray);
			//return System.BitConverter.ToSingle(byteArray, 0);
			return (float)((double)RandomULong() / (double)ulong.MaxValue);
		}

		/// <summary>
		/// Returns a random double precision number
		/// </summary>
		/// <returns>A double between 0 and 1 inclusive</returns>
		public static double RandomDouble()
		{
			//byte[] byteArray = new byte[8];
			//cryptoRandom.GetBytes(byteArray);
			//return System.BitConverter.ToDouble(byteArray, 0);// /double.MaxValue/2 + 0.5;
			return (double)RandomULong() / (double)ulong.MaxValue;
		}
	}
}
