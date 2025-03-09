namespace bg.hackathon.alphahackers.domain.entities.pyme
{
    public class Cliente
    {
        public Info Info { get; set; } = new();
        public List<Producto> Productos { get; set; } = new(); 
    }

    public class Info
    {
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Calificacion { get; set; }
        public Contactabilidad Contactabilidad { get; set; } = new();
    }
}
