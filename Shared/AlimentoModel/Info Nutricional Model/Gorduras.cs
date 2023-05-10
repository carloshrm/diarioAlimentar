namespace diarioAlimentar.Shared;

public readonly struct Gorduras
{
    public readonly double sat { get; init; }
    public readonly double monoinsat { get; init; }
    public readonly double poliinsat { get; init; }
    public readonly double trans { get; init; }

    public static Gorduras operator +(Gorduras a, Gorduras b)
    {
        return new Gorduras()
        {
            sat = a.sat + b.sat,
            monoinsat = a.monoinsat + b.monoinsat,
            poliinsat = a.poliinsat + b.poliinsat,
            trans = a.trans + b.trans,
        };
    }

    public static Gorduras operator *(Gorduras a, double b)
    {
        return new Gorduras()
        {
            sat = a.sat * b,
            monoinsat = a.monoinsat * b,
            poliinsat = a.poliinsat * b,
            trans = a.trans * b,
        };
    }
}
