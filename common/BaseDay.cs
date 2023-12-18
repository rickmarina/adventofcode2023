

public abstract class BaseDay
{


    #region "Math Utils"
    public decimal GCD(decimal a, decimal b)
    {
        if (a == 0)
            return b;
        return GCD(b % a, a);
    }

    public decimal LCM(decimal a, decimal b)
    {
        return a * b / GCD(a, b);
    }

    //TODO: Gauss Area Formula
    //TODO: Pick's theorem
    #endregion

    public char[][] CopyMatrix(char[][] original)
    {
        int filas = original.Length;
        char[][] copia = new char[filas][];

        for (int i = 0; i < filas; i++)
        {
            copia[i] = (char[])original[i].Clone();
        }

        return copia;
    }

    /// <summary>
    /// Total different chars between two strings
    /// </summary>
    /// <returns></returns>
    public int TotalDiffChars(string a, string b) { 
       return Enumerable.Range(0,a.Length).Select(i => a[i] ^ b[i]).Count(x=> x>0);
    }

}
