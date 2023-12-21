

using System.Security.Cryptography;
using System.Text;

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

    public void ShowMap(char[][] map, string separator = "") {
        for (int i=0; i< map.Length;i++) { 
            for (int j =0 ;j<map[0].Length;j++) { 
                System.Console.Write(map[i][j]+separator);
            }
            System.Console.WriteLine("");
        }
    }

    public void RotateMatrixClockwise(char[][] matrix)
    {
         int n = matrix.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // change (i, j) with (j, i)
                char temp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }

        FlipMatrixHorizontal(matrix);
    }

    public void FlipMatrixHorizontal(char[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
            Array.Reverse(matrix[i]);
    }

    public string GetSHA256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convertir la cadena de entrada en bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Calcular el hash SHA-256
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convertir el hash en una cadena hexadecimal
            StringBuilder hashStringBuilder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hashStringBuilder.Append(b.ToString("x2"));
            }

            return hashStringBuilder.ToString();
        }
    }

}
