using System;
using System.Linq;

namespace Utils.Numeric
{
    /// <summary>
    ///     Класс, представляющий вектор
    /// </summary>
    public class Vector
    {
        private readonly int[] components;

        public Vector(params int[] components)
        {
            this.components = components;
        }

        /// <summary>
        ///     Метод, проверяющий равенство количества компонент двух векторов
        /// </summary>
        /// <param name="first">Первый вектор</param>
        /// <param name="second">Второй вектор</param>
        /// <returns>Логическое значение true, если равны. Иначе false</returns>
        /// 4
        private static bool IsSameSize(Vector first, Vector second)
        {
            return first.components.Length == second.components.Length;
        }

        /// <summary>
        ///     Метод для сложения двух векторов
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Покомпонентная сумма двух векторов</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector Sum(Vector first, Vector second)
        {
            if (!IsSameSize(first, second))
            {
                throw new ArgumentException("Различный размер векторов");
            }

            return new Vector(first.components.Zip(second.components,
                (fromFirst, fromSecond) => fromFirst + fromSecond).ToArray());
        }

        /// <summary>
        ///     Перегрузка оператора + для вычисления суммы двух векторов
        /// </summary>
        /// <param name="first">Первый вектор</param>
        /// <param name="second">Второй вектор</param>
        /// <returns>Покомпонентная сумма двух векторов</returns>
        public static Vector operator +(Vector first, Vector second)
        {
            return Sum(first, second);
        }

        /// <summary>
        ///     Метод, проверяющий равны ли два вектора
        /// </summary>
        /// <param name="first">Первый вектор</param>
        /// <param name="second">Второй вектор</param>
        /// <returns>True, если векторы равны. Иначе False</returns>
        public static bool AreEquals(Vector first, Vector second)
        {
            return first.components.SequenceEqual(second.components);
        }

        /// <summary>
        ///     Метод, возвращающий n-ю компоненту вектора
        /// </summary>
        /// <param name="n">Номер компоненты (начиная с 1)</param>
        /// <returns>N-я компонента вектора</returns>
        public int GetNComponent(int n)
        {
            int? nComponent = components.ElementAtOrDefault(n - 1);
            if (nComponent == null)
            {
                throw new ArgumentException("Вектор не содержит указанной компоненты");
            }

            return nComponent.Value;
        }
    }
}