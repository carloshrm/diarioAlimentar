namespace diarioAlimentar.Shared;

public static class NivelAtividadeHelper
{
    public static double GetValorAtividade(NivelAtividade n)
    {
        return n switch
        {
            NivelAtividade.Sedentario => 1.2,
            NivelAtividade.AtivoLeve => 1.375,
            NivelAtividade.AtivoModerado => 1.55,
            NivelAtividade.AtivoMuito => 1.725,
            NivelAtividade.AtivoExtremo => 1.9,
        };
    }
}
