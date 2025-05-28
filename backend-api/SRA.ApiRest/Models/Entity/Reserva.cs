namespace SRA.ApiRest.Models.Entity
{
    public class Reserva
    {
        public int Id { get; set; }
        public string Profesor { get; set; } = string.Empty;
        public string Grupo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string FranjaHoraria { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
    }
}
