using AutoMapper;
using EFCore_Ejemplo.DTOs;
using EFCore_Ejemplo.Entidades;

namespace EFCore_Ejemplo.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GeneroCracionDTO, Genero>();
            CreateMap<AutorCreacionDTO, Actor>();
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Actor, ActorDTO>();

            //Convierte de int a Genero
            CreateMap<PeliculaCreacionDTO, Pelicula>().ForMember(ent => ent.Generos, dto => dto.MapFrom(campo => campo.Generos.Select
            (id => new Genero { Id = id })));//Hacer proyecciones

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();
        }
    }
}
