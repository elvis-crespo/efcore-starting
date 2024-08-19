using AutoMapper;
using EFCore_Ejemplo.Contexto;
using EFCore_Ejemplo.DTOs;
using EFCore_Ejemplo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_Ejemplo.Controllers
{
    [ApiController]
    [Route("api/peliculas/{peliculaId:int}/comentarios")]
    public class ComentariosController: ControllerBase
    {
        private readonly ContextoDB context;
        private readonly IMapper mapper;

        public ComentariosController(ContextoDB context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> Post(int peliculaId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.PeliculaId = peliculaId;
            context.Add(comentario);
            await context.SaveChangesAsync();   
            return Ok();
        }
    }
}
