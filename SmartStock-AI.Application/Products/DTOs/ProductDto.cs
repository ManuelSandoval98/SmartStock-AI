namespace SmartStock_AI.Application.Products.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string CodProducto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Stock { get; set; }
    public int Umbral { get; set; }
    public int IdCategoria { get; set; }
    public string? NombreCategoria { get; set; } 
    public decimal PrecioVenta { get; set; }
    public decimal PrecioCompra { get; set; }
    public decimal PrecioDescuento { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public DateTime? FechaEgreso { get; set; }
    public DateTime? FechaExpiracion { get; set; }
}