using System.ComponentModel.DataAnnotations.Schema;

namespace diarioAlimentar.Shared;

public class Alimento
{
    public int AlimentoID { get; set; }

    [NotMapped]
    public string nome { get; set; }
    [NotMapped]
    public InfoNutricional informacao { get; set; }
    [NotMapped]
    public Categoria categoria { get; set; }

    public Alimento()
    {

    }

    public Alimento(int id, string nome, InfoNutricional informacao, Categoria categoria)
    {
        this.AlimentoID = id;
        this.nome = nome;
        this.informacao = informacao;
        this.categoria = categoria;
    }

    public override string ToString()
    {
        return nome;
    }

}
