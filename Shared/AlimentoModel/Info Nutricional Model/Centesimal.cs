namespace diarioAlimentar.Shared;

public readonly struct Centesimal
{
    public readonly double umidade { get; init; }
    public readonly double energiaKcal { get; init; }
    public readonly double energiaKJ { get; init; }
    public readonly double proteina { get; init; }
    public readonly double lipideos { get; init; }
    public readonly double colesterol { get; init; }
    public readonly double carboidrato { get; init; }
    public readonly double fibras { get; init; }
    public readonly double cinzas { get; init; }

    public static Centesimal operator +(Centesimal a, Centesimal b)
    {
        return new Centesimal()
        {
            umidade = a.umidade + b.umidade,
            energiaKcal = a.energiaKcal + b.energiaKJ,
            energiaKJ = a.energiaKJ + b.energiaKJ,
            proteina = a.proteina + b.proteina,
            lipideos = a.lipideos + b.lipideos,
            colesterol = a.colesterol + b.colesterol,
            carboidrato = a.carboidrato + b.carboidrato,
            fibras = a.fibras + b.fibras,
            cinzas = a.cinzas + b.cinzas,
        };
    }

    public static Centesimal operator *(Centesimal a, double b)
    {
        return new Centesimal()
        {
            umidade = a.umidade * b,
            energiaKcal = a.energiaKcal * b,
            energiaKJ = a.energiaKJ * b,
            proteina = a.proteina * b,
            lipideos = a.lipideos * b,
            colesterol = a.colesterol * b,
            carboidrato = a.carboidrato * b,
            fibras = a.fibras * b,
            cinzas = a.cinzas * b,
        };
    }
}
