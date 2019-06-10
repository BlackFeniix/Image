using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var n = original.GetLength(0) * original.GetLength(1);
			var amountOfWhitePixels = (int) (whitePixelsFraction * n);
			var pixels= original.Cast<double>().ToList();
			pixels.Sort();
			var newMass = pixels.ToArray();
			var middleCell = 1.0;
			if (amountOfWhitePixels > 0)
				middleCell = newMass[n - amountOfWhitePixels];

			for (var i = n - 1 - amountOfWhitePixels; i >= 0; i--)
			{
				if (newMass[i] == middleCell && amountOfWhitePixels>0 && amountOfWhitePixels < n)
					amountOfWhitePixels++;
			}
			for (var i = n - 1 - amountOfWhitePixels; i >= 0; i--)
			{
				WhiteCicle(original, newMass[i], false); 
			}

			for (var i = n - 1; i >= n - amountOfWhitePixels; i--)
			{
				WhiteCicle(original, newMass[i], true);
			}
			return original;
		}

		public static double[,] WhiteCicle(double[,] original, double newMassCell, bool whiteOrBlack)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			for (var j = 0; j < width; j++)
				for (var k = 0; k < height; k++)
				{
					if ((newMassCell == original[j,k]) && whiteOrBlack)
					{
						original[j, k] = 1.0;
						return original;
					}
					if ((newMassCell == original[j,k]) && !whiteOrBlack)
					{
						original[j, k] = 0.0;
						return original;
					}				
				}
			return original;
		}
	}
}