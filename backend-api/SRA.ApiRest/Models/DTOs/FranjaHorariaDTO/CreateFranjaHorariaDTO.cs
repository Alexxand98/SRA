using System.ComponentModel.DataAnnotations;

namespace SRA.ApiRest.Models.DTOs.FranjaHorariaDTO
{
    public class CreateFranjaHorariaDTO
    {
        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }
    }
}
