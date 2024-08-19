using AutoMapper;
using EFCore_Ejemplo.Contexto;
using EFCore_Ejemplo.DTOs;
using EFCore_Ejemplo.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EFCore_Ejemplo.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculaController : ControllerBase
    {
        private readonly ContextoDB context;
        private readonly IMapper mapper;

        public PeliculaController(ContextoDB context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);

            if (pelicula.Generos is not null)
            {
                foreach (var genero in pelicula.Generos)
                {
                    context.Entry(genero).State = EntityState.Unchanged;
                    ////No tiene que crear más generos, da seguimiento
                    //Este objeto tiene un status sin cambiar. Internamiente da seguimiento al objeto, verifica su estado y toma una decision en base al statu
                }
            }
            if (pelicula.PeliculasActores is not null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();

        }

        //eagerloading
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> GetSelect(int id)
        {
            var pelicula = await context.Peliculas
                .Include(p => p.Comentarios)
                .Include(p => p.Generos)
                .Include(p => p.PeliculasActores.OrderBy(or => or.Orden))
                    .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }
            return pelicula;
        }


        //Selectloading
        [HttpGet("select/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var pelicula = await context.Peliculas
                .Select(pel =>//Instancio a objetos anónimos
                new 
                {
                    pel.Id,
                    pel.Titulo,
                    Generos = pel.Generos.Select(g => g.Name).ToList(),
                    Actores = pel.PeliculasActores.OrderBy(pa => pa.Orden).Select(pa =>
                    new 
                    {
                        Id = pa.ActorId,
                        pa.Actor.Name,
                        pa.Personaje
                    }),
                    CantidadComnetarios = pel.Comentarios.Count()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            { 
                return NotFound();
            }
            return Ok(pelicula);//El 200 ok es porque estamos proyectando a tipo anónimo
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAlteradas = await context.Peliculas.Where(g => g.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            //return NotFound(); Cambie a ok porque me salia error 404 y en el tutorial salia 204 sin error
            return Ok();
        }

    }
}
