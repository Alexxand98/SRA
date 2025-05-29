using System.ComponentModel.DataAnnotations;

namespace SRA.ApiRest.Models.DTOs.DiaNoLectivoDTO
{
    public class CreateDiaNoLectivoDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Motivo { get; set; }
    }
}
