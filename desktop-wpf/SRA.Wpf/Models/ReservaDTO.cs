using System;

namespace SRA.Wpf.Models
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Grupo { get; set; }
        public int ProfesorId { get; set; }
        public int FranjaHorariaId { get; set; }
        public string Estado { get; set; }
    }
}
