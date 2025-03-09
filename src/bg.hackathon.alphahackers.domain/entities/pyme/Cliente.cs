namespace bg.hackathon.alphahackers.domain.entities.pyme
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Calificacion { get; set; }

        public Contactabilidad Contactabilidad { get; set; } = new();

        public List<Producto> Productos { get; set; } = new(); 
    }
}
