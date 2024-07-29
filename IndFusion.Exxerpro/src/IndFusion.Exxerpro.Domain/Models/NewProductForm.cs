using System.ComponentModel.DataAnnotations;

namespace IndFusion.Exxerpro.Domain.Models;

public class NewProductForm(string name)
{
    [Required]
    [StringLength(10, ErrorMessage = "Name length can't be more than 10.")]
    public string Name { get; set; } = name;
}