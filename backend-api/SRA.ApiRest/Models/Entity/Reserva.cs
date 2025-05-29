using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRA.ApiRest.Models.Entity
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(50)]
        public string Grupo { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

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
