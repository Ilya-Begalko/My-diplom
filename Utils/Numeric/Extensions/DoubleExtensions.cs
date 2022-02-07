using System;

namespace Utils.Numeric.Extensions
{
    /// <summary>
    ///     Утилитарный статический класс, предоставляющий методы для выполнения операций над числами
    ///     с плавающей точкой двойной точности
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        ///     Метод расширения для правильного сравнения чисел с плавающей точкой двойной точности
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <param name="epsilon">Точность</param>
        /// <returns>Значение True, если числа считаются равными. Иначе False</returns>
        public static bool AreTrueEquals(this double a, double b, double epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }
    }
}