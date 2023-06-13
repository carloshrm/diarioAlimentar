namespace diarioAlimentar.Shared;

public readonly struct Minerais
{
    public readonly double magnesio { get; init; }
    public readonly double manganes { get; init; }
    public readonly double fosforo { get; init; }
    public readonly double ferro { get; init; }
    public readonly double sodio { get; init; }
    public readonly double potassio { get; init; }
    public readonly double cobre { get; init; }
    public readonly double zinco { get; init; }

    public static Minerais operator + (Minerais a, Minerais b)
    {
        return new Minerais()
        {
            magnesio = a.magnesio + b.magnesio,
            manganes = a.manganes + b.manganes,
            fosforo = a.fosforo + b.fosforo,
            ferro = a.ferro + b.ferro,
            sodio = a.sodio + b.sodio,
            potassio = a.potassio + b.potassio,
            cobre = a.cobre + b.cobre,
            zinco = a.zinco + b.zinco,
        };
    }

    public static Minerais operator * (Minerais a, double b)
    {
        return new Minerais()
        {
            magnesio = a.magnesio * b,
            manganes = a.manganes * b,
            fosforo = a.fosforo * b,
            ferro = a.ferro * b,
            sodio = a.sodio * b,
            potassio = a.potassio * b,
            cobre = a.cobre * b,
            zinco = a.zinco * b,
        };
    }
}
