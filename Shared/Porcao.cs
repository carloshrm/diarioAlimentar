using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public class Porcao
{
    [Key]
    public Guid porcaoID { get; set; }
    public Alimento alimento { get; set; }
    public double quantidade { get; set; }

}