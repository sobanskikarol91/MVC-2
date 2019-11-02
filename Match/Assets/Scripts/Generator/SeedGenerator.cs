using System.Collections.Generic;

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
                data[r, c] = GetRandomNotRepeatingObject<T>(data, variants, r, c);
            }
        }
    }

    private static T GetRandomNotRepeatingObject<T>(T[,] data, T[] variants, int r, int c)
    {
        List<T> leftVariants = new List<T>(variants);
        List<T> variantsToRemove = new List<T>();

        if (c > 0) variantsToRemove.Add(data[r, c - 1]);
        if (r > 0) variantsToRemove.Add(data[r - 1, c]);

        for (int i = 0; i < variantsToRemove.Count; i++)
            leftVariants.Remove(variantsToRemove[i]);

        int nr = UnityEngine.Random.Range(0, leftVariants.Count);

        return leftVariants[nr];
    }

    public static T SetRandomNotRepeatingObjectInCollection<T>(ref T[,] data, T[] variants, int colorsAmount, int seed, int objectRow, int objectColumn)
    {
        return data[objectRow, objectColumn] = GetRandomNotRepeatingObject<T>(data, variants, objectColumn, objectColumn);
    }
}