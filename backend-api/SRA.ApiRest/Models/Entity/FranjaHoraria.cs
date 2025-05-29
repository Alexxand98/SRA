using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRA.ApiRest.Models.Entity
{
    public class FranjaHoraria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string HoraInicio { get; set; }

        [Required]
        [MaxLength(20)]
        public string HoraFin { get; set; }

        [NotMapped]
        public string Rango => $"{HoraInicio} - {HoraFin}";

        public ICollection<Reserva>? Reservas { get; set; }
    }
}
