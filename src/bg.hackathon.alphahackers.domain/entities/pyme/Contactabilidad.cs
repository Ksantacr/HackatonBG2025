namespace bg.hackathon.alphahackers.domain.entities.pyme
{
    public class Contactabilidad
    {
        public string Pais { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string CallePrincipal { get; set; } = string.Empty;
        public string CalleSecundaria { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Email1 { get; set; } = string.Empty;
        public string Email2 { get; set; } = string.Empty;
        public string Telefono1 { get; set; } = string.Empty;
        public string Telefono2 { get; set; } = string.Empty;
    }
}
