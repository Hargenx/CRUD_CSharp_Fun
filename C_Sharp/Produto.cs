using System.ComponentModel.DataAnnotations;
#nullable disable
public class Produto{
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}
