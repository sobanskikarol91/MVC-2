using System.Collections.Generic;
using UnityEngine;

public static class SeedGenerator
{
    public static void SetRandomNotRepeatingCollection<T>(ref T[,] data, T[] variants, int colorsAmount, int seed)
    {
        int rows = data.GetLength(0);
        int columns = data.GetLength(1);
        UnityEngine.Random.InitState(seed);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                data[r, c] = GetRandomNotRepeatingObject(data, variants, r, c);
            }
        }
    }

    private static T GetRandomNotRepeatingObject<T>(T[,] data, T[] variants, int r, int c)
    {
        List<T> leftVariants = new List<T>(variants);

        if (c > 0) leftVariants.Remove(data[r, c - 1]);
        if (r > 0) leftVariants.Remove(data[r - 1, c]);

        int nr = UnityEngine.Random.Range(0, leftVariants.Count);

        return leftVariants[nr];
    }
}