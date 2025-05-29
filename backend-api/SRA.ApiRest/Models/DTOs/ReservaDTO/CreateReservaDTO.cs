using System.ComponentModel.DataAnnotations;

namespace SRA.ApiRest.Models.DTOs.ReservaDTO
{
    public class CreateReservaDTO
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(50)]
        public string Grupo { get; set; }

        [Required]
        public int ProfesorId { get; set; }

        [Required]
        public int FranjaHorariaId { get; set; }
    }
}
