using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diarioAlimentar.Shared;

public class Porcao
{
    [Key]
    public Guid porcaoID { get; set; } = Guid.NewGuid();

    [NotMapped]
    public Alimento alimento { get; set; } 
    public int alimentoID { get; set; }
    public double quantidade { get; set; }

    public Guid refeicaoID { get; set; }

    public Porcao()
    {
            
    }

    public override string ToString()
    {
        return $"\n\t Nome: {alimento.nome}  \t Quantidade: {quantidade} g/ml";
    }

}