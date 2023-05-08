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
}
