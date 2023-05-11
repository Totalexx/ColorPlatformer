using System.Collections.Generic;

namespace RougeBuilder.Utils;

public class BoolArray3x3EqualityComparer : IEqualityComparer<bool[,]>
{
    public bool Equals(bool[,] x, bool[,] y)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (x[i, j] != y[i, j])
                    return false;
        return true;
    }

    public int GetHashCode(bool[,] obj)
    {
        var result = 17;
        for (var i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                unchecked
                {
                    result = result * i * 23 + obj[i, j].GetHashCode() * j;
                }
            }
        }
        return result;
    }
}