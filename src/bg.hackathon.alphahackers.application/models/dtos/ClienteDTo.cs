using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bg.hackathon.alphahackers.application.models.dtos
{

    public class ClienteDTo
    {
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Logo { get; set; }
        public double Calificacion { get; set; }
        public UbicacionDto Ubicacion { get; set; }
    }
}
