using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Nome { get; set; }

    [Range(0, 9999.99)]
    public decimal Price { get; set; }
}

