﻿using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public enum NivelAtividade
{
    [Display(Name = "Sedentário (pouco ou nenhum exercício) ")]
    Sedentario = 1,

    [Display(Name = "Levemente Ativo (exercícios leves/esportes de 1 a 3 dias por semana) ")]
    AtivoLeve = 2,

    [Display(Name = "Atividades Moderadas (exercícios moderados/esportes de 3 a 5 dias por semana)")]
    AtivoModerado = 3,

    [Display(Name = "Muito Ativo (exercícios pesados/esportes de 6 a 7 dias por semana) ")]
    AtivoMuito = 4,

    [Display(Name = "Extremamente Ativo (exercícios pesados/esportes todos os dias mais trabalho pesado, ou treinos duplos ao dia.)")]
    AtivoExtremo = 5
}
