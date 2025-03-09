using bg.hackathon.alphahackers.domain.entities.enums;

namespace bg.hackathon.alphahackers.domain.entities.pyme
{
    public class LineaCredito
    {
        public int Id_Cliente { get; set; }
        public decimal Total_Credito { get; set; }
        public decimal Utilizado { get; set; }
        public decimal Disponible => Total_Credito - Utilizado;
        public string Moneda { get; set; } = "USD";
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public EstadoLineaCredito Estado { get; set; }
    }
}
