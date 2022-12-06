using System;
using System.Collections.Generic;

namespace LR3module2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double eps = 0.001;
            double x = 2;
            double y = 1;
            Console.WriteLine($"Градиентный спуск/Метод наискорейшего спуска: {gradientDescent(x, y, eps).Item1} {gradientDescent(x, y, eps).Item2} \nИтераций: {gradientDescent(x, y, eps).Item3}");
            Console.WriteLine($"Метод сопряжённых градиентов: {conjugateGradients(x, y, eps).Item1} {conjugateGradients(x, y, eps).Item2} \nИтераций: {conjugateGradients(x, y, eps).Item3}");

        }
        static double function(double x, double y)
        {
            return 9 * x + 7 * y - 9 * Math.Pow(x, 2) - 3 * Math.Pow(y, 2) + 1;

        }
        static (double, double) Gradient(double x, double y)
        {
            return (x - 18 * x, 7 - 6 * y);

        }
        static (double, double, int) gradientDescent(double x, double y, double eps)
        {
            //гайд: https://ru.wikipedia.org/wiki/Градиентный_спуск

            List<double> xList = new List<double>() { x };
            List<double> yList = new List<double>() { y };
            double tmp1 = int.MaxValue;
            double tmp2 = int.MaxValue;
            int k = 0;
            do
            {
                k++;
                tmp1 = xList[xList.Count - 1] - 0.001 * (Gradient(xList[xList.Count - 1], yList[yList.Count - 1]).Item1 * -1);
                tmp2 = yList[yList.Count - 1] - 0.001 * (Gradient(xList[xList.Count - 1], yList[yList.Count - 1]).Item2 * -1);

                xList.Add(tmp1);
                yList.Add(tmp2);
            }
            while (Math.Abs(tmp1 - xList[xList.Count - 2]) > eps);
            return (xList[xList.Count - 1], yList[yList.Count - 1], k);
        }
        static (double, double, int) conjugateGradients(double x, double y, double eps)
        {

            int k = 0;
            double x0 = x;
            List<double> xList = new List<double>() { x };
            List<double> yList = new List<double>() { y };
            double tmpX = int.MaxValue;
            double tmpY = int.MaxValue;
            double tmpDX = int.MaxValue;
            double tmpDY = int.MaxValue;
            do
            {
                double gX = Gradient(xList[xList.Count - 1], yList[yList.Count - 1]).Item1;
                double gY = Gradient(xList[xList.Count - 1], yList[yList.Count - 1]).Item2;

                double dX = -1 * gX;
                double dY = -1 * gY;

                tmpX = x + 0.001 * dX;
                tmpY = y + 0.001 * dY;
                xList.Add(tmpX);
                yList.Add(tmpY);

                k++;
            }
            while (Math.Abs(xList[xList.Count - 1] - xList[xList.Count - 2]) > eps);

            return (xList[xList.Count - 1], yList[yList.Count - 1], k);
        }
    }
}
