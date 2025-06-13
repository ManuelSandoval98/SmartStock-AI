using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartStock_AI.Domain.Products.Entities;

namespace SmartStock_AI.Domain.Categories.Entities;

[Table("categorias")]
public class Categoria
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; } 
    [Column("nombre")]
    public string Nombre { get; set; }
    
    public ICollection<Producto> Productos { get; set; }
}