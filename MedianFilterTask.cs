using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
			
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var newOriginal=new double[width,height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					List<double> medianCells=new List<double>();
					medianCells.Add(original[i, j]);

					if ((i - 1) >= 0) //левая сторона
					{
						medianCells.Add(original[i - 1, j]);
						if ((j - 1) >= 0)
							medianCells.Add(original[i - 1, j - 1]);
						if ((j + 1) <= (height-1))
							medianCells.Add(original[i - 1, j + 1]);
					}
					
					if ((i + 1) <= (width-1)) //правая сторона
					{
						medianCells.Add(original[i + 1, j]);
						if ((j - 1) >= 0)
							medianCells.Add(original[i + 1, j - 1]);
						if ((j + 1) <= (height-1))
							medianCells.Add(original[i + 1, j + 1]);
					}
					
					if ((j - 1) >= 0) 
					{
						medianCells.Add(original[i, j - 1]);
					}

					if ((j + 1) <= (height-1)) 
					{
						medianCells.Add(original[i, j + 1]);
					}
					
					var newMass=medianCells.ToArray();
					newOriginal[i,j]= MedianCell(newMass);
					
				}
			}
			return newOriginal;
		}

		public static double MedianCell(double[] inputMass)
		{
			Array.Sort(inputMass);

			var min = 0;
			var max = inputMass.GetLength(0)-1;
			while (min < max)
			{
				min++;
				max--;
			}

			if (min==max)
				return inputMass[(int)(min)];
			else
				return (inputMass[min]+inputMass[max])/2;

		}
	}
}