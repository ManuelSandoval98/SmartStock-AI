using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartStock_AI.Domain.Categories.Entities;

namespace SmartStock_AI.Domain.Products.Entities;

[Table("productos")]
public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("cod_producto")]
    public string CodProducto { get; set; }
    [Column("nombre")]
    public string Nombre { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    [Column("stock")]
    public int Stock { get; set; }
    [Column("umbral")]
    public int Umbral { get; set; }
    [Column("id_categoria")]
    public int IdCategoria { get; set; }
    [Column("precio_venta")]
    public decimal PrecioVenta { get; set; }
    [Column("precio_compra")]
    public decimal PrecioCompra { get; set; }
    [Column("precio_descuento")]
    public decimal PrecioDescuento { get; set; }
    [Column("fecha_ingreso")]
    public DateTime? FechaIngreso { get; set; }
    [Column("fecha_egreso")]
    public DateTime? FechaEgreso { get; set; }
    [Column("fecha_expiracion")]
    public DateTime? FechaExpiracion { get; set; } 
    
    public Categoria Categoria { get; set; }
}