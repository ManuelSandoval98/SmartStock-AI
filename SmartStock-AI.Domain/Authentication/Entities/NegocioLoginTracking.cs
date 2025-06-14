namespace SmartStock_AI.Domain.Authentication.Entities;

public class NegocioLoginTracking
{
    public int Id { get; set; }
    public string CorreoAdmin { get; set; }
    public int IntentosFallidos { get; set; }
    public DateTime? BloqueadoHasta { get; set; }
}