namespace SRA.ApiRest.Models.DTOs.ProfesorDTO
{
    public class ProfesorDTO : CreateProfesorDTO
    {
        public int Id { get; set; }
        public DateTime UltimoAcceso { get; set; }
        public new string AppUserId { get; set; }
    }
}
