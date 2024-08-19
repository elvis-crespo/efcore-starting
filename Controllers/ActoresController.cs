using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_Ejemplo.Contexto;
using EFCore_Ejemplo.DTOs;
using EFCore_Ejemplo.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Ejemplo.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ContextoDB context;
        private readonly IMapper mapper;

        public ActoresController(ContextoDB context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()//Tipo de dato genero qe retornamos de una acion en endpoints
        {
            //return await context.Actores.ToListAsync();
            //ordenar
            return await context.Actores.OrderByDescending(f => f.Fortuna).ToListAsync();    
        }

        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> GET(string nombre)
        {
            //Version 1
            //return await context.Actores.Where(a => a.Name == nombre).ToListAsync();
            //Ordenar por nombre y fecha de nacimiento
            return await context.Actores
                .Where(a => a.Name == nombre)
                .OrderBy(a => a.Name)
                    .ThenByDescending(a => a.FechaNacimiento)
                .ToListAsync();
        }

        [HttpGet("nombre/v2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GETv2(string nombre)
        {
            //Version 2
            //nombre = nombre.ToUpper();
            return await context.Actores.Where(a => a.Name.Contains(nombre)).ToListAsync();
        }

        [HttpGet("fechaNaciemiento/rango")]
        public async Task<ActionResult<IEnumerable<Actor>>> GET(DateTime inicio, DateTime fin)
        {
            return await context.Actores
                .Where(a => a.FechaNacimiento >= inicio && a.FechaNacimiento <= fin)
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> GET(int id)
        {
            //Obtiene un actor por id
            var actor = await context.Actores.FirstOrDefaultAsync(a => a.Id == id);

            if (actor is null)
            { 
                return NotFound();
            }
            return actor;
        }

        [HttpGet("idynombre")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GETidynombre()
        {
            //Select para hacer proyecciones a un objeto anonimo(Hace que EFC emita un query a las columnas id, name)
            //var actores = await context.Actores.Select(a => new { a.Id, a.Name} ).ToListAsync();
            //return Ok(actores);//Hago un 200 ok y le paso actores que me serializa a json

            //Ahora con Actor DTO
            //return await context.Actores.Select(a => 
            //new ActorDTO 
            //{
            //    Id = a.Id, 
            //    Nombre = a.Name
            //}).ToListAsync();

            //Con Automapper
            return await context.Actores
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
            //Recordar que solo me muestra los campos que he creado con actroDTO
            //Automapper se encarga de aquello
           
        }
    }
}
