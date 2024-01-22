using AutoMapper;
using SGCU.Models.DbSet;
using SGCU.Models.Dto;

namespace SGCU.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UsuarioModel, UsuarioReadDto>();
        CreateMap<UsuarioCreateDto, UsuarioModel>();

        CreateMap<UnidadeModel, UnidadeReadDto>();
        CreateMap<UnidadeCreateDto, UnidadeModel>();
        
        CreateMap<ColaboradorModel, ColaboradorReadDto>();
        CreateMap<ColaboradorCreateDto, ColaboradorModel>();
    }
}
