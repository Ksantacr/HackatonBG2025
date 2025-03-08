using bg.hackathon.alphahackers.domain.entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bg.hackathon.alphahackers.domain.entities.pyme
{
    public class LineaCredito
    {
        public int Id { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal Utilizado { get; set; }
        public decimal Disponible => TotalCredito - Utilizado;
        public string Moneda { get; set; } = "USD";
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoLineaCredito Estado { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = new();
    }
}
