using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diarioAlimentar.Shared;

public class Porcao
{
    [Key]
    public Guid porcaoID { get; set; } = Guid.NewGuid();

    [NotMapped]
    public Alimento Alimento { get; set; } 
    public int alimentoID { get; set; }
    public double quantidade { get; set; }

    public Guid refeicaoID { get; set; }

    public Porcao()
    {
            
    }

}