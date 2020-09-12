using System.Collections.Generic;
using System;

/*
 * Class used as a extansion of arrays
 * Used to shuffle array
 */
public static class ArrayRandomizer
{
    //Random variable
    private static readonly Random rng = new Random();


    //Method used to shuffle any array
    //
    //Parameter => list, current array to shuffle
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}