public class Location<T> {

    public T x { get; set; }
    public T y { get; set; }

    public Location(T x, T y)
    {
        this.x = x; 
        this.y = y;
    }

    public override string ToString()
    {
        return string.Format("Y:{0},X:{1}", y,x);
    }

    //Hacemos override de los siguientes métodos para que al intentar buscar realice la comparación entre los objetos de tipo location usando toString()
    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return this.ToString().Equals(obj?.ToString());
    }
}