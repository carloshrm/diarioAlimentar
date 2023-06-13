using ApexCharts;

using diarioAlimentar.Shared;

using Newtonsoft.Json;

using System;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

using static System.Net.Mime.MediaTypeNames;

namespace diarioAlimentar.Client.Services;

public class ReceitaService
{
    private readonly HttpClient _http;

    public ReceitaService(HttpClient client)
    {
        _http = client;
    }

    public async Task<List<Receita>> BuscarReceita(string q)
    {
        var _modeloAPI = new
        {
            from = 0.0,
            to = 0.0,
            count = 0.0,

            _links = new
            {
                self = new
                {
                    href = "",
                    title = ""
                },
                next = new
                {
                    href = "",
                    title = ""
                }
            },

            hits = new[] { new {
                    recipe = new {
                        uri = "",
                        label = "",
                        image = "",
                        images = new {
                            THUMBNAIL = new {
                                url = "",
                                width = 0.0,
                                height = 0.0
                            },
                            SMALL = new {
                                url = "",
                                width = 0.0,
                                height = 0.0
                            },
                            REGULAR = new {
                                url = "",
                                width = 0.0,
                                height = 0.0
                            },
                            LARGE = new {
                                url = "",
                                width = 0.0,
                                height = 0.0
                            }
                        },

                source = "",
                url = "",
                shareAs = "",
                yield = 0.0,

                dietLabels = new [] { "" },
                healthLabels = new [] { "" },
                cautions = new [] { "" },
                ingredientLines = new [] { "" },

                ingredients = new [] { new {
                                    text = "",
                                    quantity = 0.0,
                                    measure = "",
                                    food = "",
                                    weight = 0.0,
                                    foodId = ""
                                }},

                calories = 0.0,
                glycemicIndex = 0.0,
                totalCO2Emissions = "",
                co2EmissionsClass = "",
                totalWeight = 0.0,

                cuisineType = new []{ "" },
                mealType =  new []{ "" },
                dishType =  new []{ "" },
                instructions =  new []{ "" },
                tags = new []{ "" },

                externalId = "",
                totalNutrients = new {},
                totalDaily = new {},

                digest = new [] { new {
                                    label = "",
                                    tag = "",
                                    schemaOrgTag = "",
                                    total = 0.0,
                                    hasRDI = true,
                                    daily = 0.0,
                                    unit = "",
                                    sub = new [] { new { } } }
                                }},
                    _links = new {
                        self = new {
                           href = "",
                           title = ""
                        },
                        next = new {
                            href = "",
                            title = ""
                        }
                    }
                }
            }
        };

        string url = $"https://api.edamam.com/api/recipes/v2?type=public&q={q}&app_id=e411cea7&app_key=3c66c5a1c258dc1aaeca4b8926f55ee2";
        var refRequest = await _http.GetAsync(url);
        if (refRequest.IsSuccessStatusCode)
        {
            var resultado = await refRequest.Content.ReadAsStringAsync();
            var resultadoJson = JsonConvert.DeserializeAnonymousType(resultado, _modeloAPI);
            var conteudo = new List<Receita>();
            foreach (var i in resultadoJson.hits.Take(4))
            {
                conteudo.Add(new Receita
                {
                    nome = i.recipe.label ?? "",
                    img = i.recipe.image ?? "",
                    url = i.recipe.url,
                    porcoes = (int)i.recipe.yield,
                    calorias = i.recipe.calories,
                });
            }
            return conteudo;
        }
        else
            return new List<Receita>();
    }
}

public class Receita
{
    public double calorias { get; set; }
    public string nome { get; set; }
    public string img { get; set; }
    public string url { get; set; }
    public int porcoes { get; set; }
    public List<string> ingredientes { get; set; } = new List<string>();
    public List<string> instrucoes { get; set; } = new List<string>();
}