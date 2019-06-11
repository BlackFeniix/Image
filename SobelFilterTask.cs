using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            int deltax = sx.GetLength(0) / 2;
            int deltay = sx.GetLength(1) / 2;
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            
            var sy = Transpose(sx);  //транспонированная матрица sx
            
            var result = new double[width, height];
            for (var x = deltax; x < width - deltax; x++)
                for (var y = deltay; y < height - deltay; y++)
                {
                    var neighbourhood = GetNeighbourhood(g, x, y, sx.GetLength(0));
                    var gx = MultiplyMatrix(sx, neighbourhood);
                    var gy = MultiplyMatrix(sy, neighbourhood);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }

            if (width == 1 && height == 1)
            {
                var neighbourhood = GetNeighbourhood(g, 0, 0, sx.GetLength(0));
                var gx = MultiplyMatrix(sx, neighbourhood);
                var gy = MultiplyMatrix(sy, neighbourhood);
                result[0, 0] = Math.Sqrt(gx * gx + gy * gy);
            }
            return result;
        }

        public static double[,] Transpose(double[,] sx)   //транспонирование матрицы 
        {
            var size = sx.GetLength(0);
            var sy = new double[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    sy[i, j] = sx[j, i];
                }
            }
            return sy;
        }

        public static double[,] GetNeighbourhood(double[,] g, int x, int y, int size)  //получение окрестности точки [x,y]
        {
            var result = new double[size, size];
            var delta = size / 2;
            if (size == 1)
            {
                result[0,0]=g[x, y];
                return result;
            }
            
            for (var i = -delta; i < delta+1; i++)
                for (var j = -delta; j < delta+1; j++)
                {
                    var num = g[x + i, y + j];
                    result[i + delta, j + delta] = g[x + i, y + j];
                }
            return result;
        }

        public static double MultiplyMatrix(double[,] sxy, double[,] neighbourhood)
        {
            var sum = 0d;
            var size = neighbourhood.GetLength(0);
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    sum += sxy[i, j] * neighbourhood[i, j];
                }
            }
            return sum;
        }
    }
}