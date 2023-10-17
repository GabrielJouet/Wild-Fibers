using System;
using System.Collections.Generic;

namespace Miscellanious
{
    /// <summary>
    /// Class used to randomize array.
    /// </summary>
    public static class ArrayRandomizer
    {
        /// <summary>
        /// Rng variable.
        /// </summary>
        private static readonly Random Rng = new();


        /// <summary>
        /// Method used to shuffle an array.
        /// </summary>
        /// <param name="list">The list to shuffle</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}