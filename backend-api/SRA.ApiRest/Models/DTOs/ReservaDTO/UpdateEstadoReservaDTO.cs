using System.ComponentModel.DataAnnotations;

namespace SRA.ApiRest.Models.DTOs.ReservaDTO
{
    public class UpdateEstadoReservaDTO
    {
        [Required]
        [RegularExpression("^(Aprobada|Rechazada)$", ErrorMessage = "Estado inválido")]
        public string Estado { get; set; }
    }
}
