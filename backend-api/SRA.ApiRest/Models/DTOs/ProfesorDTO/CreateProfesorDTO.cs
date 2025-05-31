using System.ComponentModel.DataAnnotations;

namespace SRA.ApiRest.Models.DTOs.ProfesorDTO
{
    public class CreateProfesorDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El AppUserId es obligatorio")]
        public string AppUserId { get; set; }
    }
}
