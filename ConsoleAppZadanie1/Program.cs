using System;
using System.Globalization;

namespace RegularPolygon
{
    /// <summary>
    /// Способ задания исходных данных для вычисления периметра.
    /// </summary>
    public enum InputMode
    {
        /// <summary>Периметр по длине стороны и числу сторон.</summary>
        BySide = 1,

        /// <summary>Периметр по радиусу вписанной окружности и числу сторон.</summary>
        ByInradius = 2
    }

    internal static class Program
    {
        private const int MinSides = 3;

        private static void Main()
        {
            Console.WriteLine("Периметр правильного многоугольника");

            InputMode mode = ReadMode();
            int n = ReadSides();
            double perimeter;

            switch (mode)
            {
                case InputMode.BySide:
                    double a = ReadPositiveDouble("Введите длину стороны a: ");
                    perimeter = PerimeterBySide(a, n);
                    break;

                case InputMode.ByInradius:
                    double r = ReadPositiveDouble("Введите радиус вписанной окружности r: ");
                    perimeter = PerimeterByInradius(r, n);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }

            Console.WriteLine($"Периметр P = {perimeter.ToString("F4", CultureInfo.InvariantCulture)}");
        }

        /// <summary>
        /// Вычисляет периметр правильного n-угольника по длине стороны: P = n * a.
        /// </summary>
        /// <param name="a">Длина стороны (положительная).</param>
        /// <param name="n">Число сторон (не менее 3).</param>
        /// <returns>Периметр многоугольника.</returns>
        public static double PerimeterBySide(double a, int n)
        {
            return n * a;
        }

        /// <summary>
        /// Вычисляет длину стороны правильного n-угольника по радиусу вписанной окружности.
        /// Из формулы r = a / (2 * tg(180° / n)) получаем a = 2 * r * tg(180° / n).
        /// </summary>
        /// <param name="r">Радиус вписанной окружности (положительный).</param>
        /// <param name="n">Число сторон (не менее 3).</param>
        /// <returns>Длина стороны многоугольника.</returns>
        public static double SideByInradius(double r, int n)
        {
            return 2 * r * Math.Tan(Math.PI / n);
        }

        /// <summary>
        /// Вычисляет периметр правильного n-угольника по радиусу вписанной окружности.
        /// </summary>
        /// <param name="r">Радиус вписанной окружности (положительный).</param>
        /// <param name="n">Число сторон (не менее 3).</param>
        /// <returns>Периметр многоугольника.</returns>
        public static double PerimeterByInradius(double r, int n)
        {
            return PerimeterBySide(SideByInradius(r, n), n);
        }

        /// <summary>
        /// Запрашивает у пользователя способ задания данных, пока не будет введено корректное значение.
        /// </summary>
        /// <returns>Выбранный режим вычисления.</returns>
        private static InputMode ReadMode()
        {
            while (true)
            {
                Console.WriteLine("Выберите способ задания данных:");
                Console.WriteLine("  1 - по длине стороны и числу сторон");
                Console.WriteLine("  2 - по радиусу вписанной окружности и числу сторон");
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int choice)
                    && Enum.IsDefined(typeof(InputMode), choice))
                {
                    return (InputMode)choice;
                }

                Console.WriteLine("Ошибка: введите 1 или 2.");
            }
        }

        /// <summary>
        /// Запрашивает число сторон многоугольника (целое, не менее 3).
        /// </summary>
        /// <returns>Корректное число сторон.</returns>
        private static int ReadSides()
        {
            while (true)
            {
                Console.Write($"Введите число сторон n (целое, не менее {MinSides}): ");

                if (int.TryParse(Console.ReadLine(), out int n) && n >= MinSides)
                {
                    return n;
                }

                Console.WriteLine($"Ошибка: нужно целое число не меньше {MinSides}.");
            }
        }

        /// <summary>
        /// Запрашивает положительное вещественное число. Допускается ввод с точкой или запятой.
        /// </summary>
        /// <param name="prompt">Приглашение к вводу.</param>
        /// <returns>Корректное положительное число.</returns>
        private static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Replace(',', '.');

                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                    && value > 0 && !double.IsInfinity(value))
                {
                    return value;
                }

                Console.WriteLine("Ошибка: нужно положительное число.");
            }
        }
    }
}