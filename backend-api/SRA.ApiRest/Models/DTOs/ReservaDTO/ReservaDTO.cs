namespace SRA.ApiRest.Models.DTOs.ReservaDTO
{
    public class ReservaDTO : CreateReservaDTO
    {
        public int Id { get; set; }
        public string Estado { get; set; }
    }
}
