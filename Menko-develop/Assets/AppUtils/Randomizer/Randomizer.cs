using UnityEngine;

namespace AppUtils
{
	public static class Randomizer
	{
		public static int Choose(float[] percents)
		{
			float total, shotPoint;
			total = shotPoint = 0f;

			for (int i = 0; i < percents.Length; ++i)
				total += percents[i];

			shotPoint = Random.value * total;

			for (int i = 0; i < percents.Length; ++i)
			{
				if (shotPoint < percents [i])
					return i;
				else
					shotPoint -= percents [i];
			}

			return percents.Length - 1;
		}
	}
}