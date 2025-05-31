using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRA.ApiRest.Models.Entity
{
    public class Reserva
    {
        public enum EstadoReserva
        {
            Pendiente,
            Aprobada,
            Rechazada
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(50)]
        public string Grupo { get; set; }

        [Required]
        public EstadoReserva Estado { get; set; } = EstadoReserva.Pendiente;

        //Relaciones

        [Required]
        public int ProfesorId { get; set; }

        [ForeignKey("ProfesorId")]
        public Profesor Profesor { get; set; }

        [Required]
        public int FranjaHorariaId { get; set; }

        [ForeignKey("FranjaHorariaId")]
        public FranjaHoraria FranjaHoraria { get; set; }
    }
}
