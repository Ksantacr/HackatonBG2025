namespace bg.hackathon.alphahackers.domain.entities.pyme
{
    public class Producto
    {
        public int Id_Cliente { get; set; }
        public Guid Id_Producto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
