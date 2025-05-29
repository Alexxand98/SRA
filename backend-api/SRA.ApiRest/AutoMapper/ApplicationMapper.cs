using AutoMapper;
using SRA.ApiRest.Models.DTOs.DiaNoLectivoDTO;
using SRA.ApiRest.Models.DTOs.FranjaHorariaDTO;
using SRA.ApiRest.Models.DTOs.ProfesorDTO;
using SRA.ApiRest.Models.DTOs.ReservaDTO;
using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Reserva, ReservaDTO>().ReverseMap();
            CreateMap<Reserva, CreateReservaDTO>().ReverseMap();

            CreateMap<Profesor, ProfesorDTO>().ReverseMap();
            CreateMap<Profesor, CreateProfesorDTO>().ReverseMap();

            CreateMap<FranjaHoraria, FranjaHorariaDTO>().ReverseMap();
            CreateMap<FranjaHoraria, CreateFranjaHorariaDTO>().ReverseMap();

            CreateMap<DiaNoLectivo, DiaNoLectivoDTO>().ReverseMap();
            CreateMap<DiaNoLectivo, CreateDiaNoLectivoDTO>().ReverseMap();
        }
    }
}
