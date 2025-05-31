using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRA.ApiRest.Models.Entity
{
    public class Profesor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Correo { get; set; }

        public DateTime UltimoAcceso { get; set; }

        [Required]
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }
}
