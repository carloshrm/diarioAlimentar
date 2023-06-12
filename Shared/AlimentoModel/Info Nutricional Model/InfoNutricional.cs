namespace diarioAlimentar.Shared;

public class InfoNutricional
{
    public Centesimal Centesimal { get; set; }
    public Vitaminas Vitaminas { get; set; }
    public Minerais Minerais { get; set; }
    public Gorduras Gorduras { get; set; }

    public static InfoNutricional operator +(InfoNutricional a, InfoNutricional b)
    {
        return new InfoNutricional()
        {
            Centesimal = a.Centesimal + b.Centesimal,
            Vitaminas = a.Vitaminas + b.Vitaminas,
            Minerais = a.Minerais + b.Minerais,
            Gorduras = a.Gorduras + b.Gorduras,
        };
    }

    public static InfoNutricional operator *(InfoNutricional a, double b)
    {
        return new InfoNutricional()
        {
            Centesimal = a.Centesimal * b,
            Vitaminas = a.Vitaminas * b,
            Minerais = a.Minerais * b,
            Gorduras = a.Gorduras * b,
        };
    }

};
