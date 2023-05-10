namespace diarioAlimentar.Shared;

public readonly struct Vitaminas
{
    public readonly double retinol { get; init; }
    public readonly double re { get; init; }
    public readonly double rae { get; init; }
    public readonly double vitC { get; init; }
    public readonly double tiamina { get; init; }
    public readonly double riboflavina { get; init; }
    public readonly double niacina { get; init; }
    public readonly double vitB6 { get; init; }

    public static Vitaminas operator +(Vitaminas a, Vitaminas b)
    {
        return new Vitaminas()
        {
            retinol = a.retinol + b.retinol,
            re = a.re + b.re,
            rae = a.rae + b.rae,
            vitC = a.vitC + b.vitC,
            tiamina = a.tiamina + b.tiamina,
            riboflavina = a.riboflavina + b.riboflavina,
            niacina = a.niacina + b.niacina,
            vitB6 = a.vitB6 + b.vitB6,
        };
    }

    public static Vitaminas operator *(Vitaminas a, double b)
    {
        return new Vitaminas()
        {
            retinol = a.retinol * b,
            re = a.re * b,
            rae = a.rae * b,
            vitC = a.vitC * b,
            tiamina = a.tiamina * b,
            riboflavina = a.riboflavina * b,
            niacina = a.niacina * b,
            vitB6 = a.vitB6 * b,
        };
    }
}
