using System;

namespace SRA.Wpf.Models
{
    public class CreateReservaDTO
    {
        public DateTime Fecha { get; set; }
        public string Grupo { get; set; }
        public int ProfesorId { get; set; }
        public int FranjaHorariaId { get; set; }
    }
}
