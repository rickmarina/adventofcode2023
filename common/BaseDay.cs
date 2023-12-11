

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
        return a*b / GCD(a, b);
    }

    #endregion

}