using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRA.ApiRest.Models.Entity
{
    public class DiaNoLectivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(100)]
        public string? Motivo { get; set; }
    }
}
