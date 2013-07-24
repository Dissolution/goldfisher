using System;

namespace Goldfisher
{
	[Flags]
	public enum Color
	{
		None = 0,
		White = 1,
		Blue = 2,
		Black = 4,
		Red = 8,
		Green = 16,
		Any = 32,
	}

	public static class ColorExtensions
	{
		public static int FlagCount(this Color color)
		{
			var colorValue = (long) color;
			var count = 0;
			while (colorValue != 0)
			{
				//Remove the end bit
				colorValue = colorValue & (colorValue - 1);
				//Add to count
				count += 1;
			}

			return count;
		}

		public static bool Gold(this Color color)
		{
			return color.FlagCount() > 1;
		}
	}
}
