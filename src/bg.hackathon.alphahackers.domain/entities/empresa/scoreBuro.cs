namespace bg.hackathon.alphahackers.domain.entities.empresa
{
    public class ScoreInfo
    {
        public int ScoreId { get; set; }
        public string Cedula { get; set; }
        public int Score { get; set; }
        public int ProbMorosidad { get; set; }
        public int MaximoCupoTC { get; set; }
        public string MarcaTarjeta { get; set; }
        public int CupoCreditos { get; set; }
    }
}
