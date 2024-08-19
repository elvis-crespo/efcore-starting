using AutoMapper;
using EFCore_Ejemplo.Contexto;
using EFCore_Ejemplo.DTOs;
using EFCore_Ejemplo.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Ejemplo.Controllers
{
    //Los controladores son los encargados de recibir peticiones http
    [ApiController]
    [Route("api/generos")]
    public class GeneroController : ControllerBase
    {
        private readonly ContextoDB context;
        private readonly IMapper mapper;

        //Digita ctor + tab = Consturctor. A continuacion inicializamos como campo
        public GeneroController(ContextoDB context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(GeneroCracionDTO generoCreacion)//No puedo trabajar con GeneroCreacionDTO porque no es una entidad
        {
            //Tengo que mapear - Mapear resulta dificil si tengo muchas propiedades, se usa automapper
            //var genero = new Genero
            //{
            //    Name = generoCreacion.Nombre
            //};
            var yaExisteGeneroConEsteNombre = await context.Generos.AnyAsync(
                g => g.Name == generoCreacion.Name
                );

            if (yaExisteGeneroConEsteNombre)
            {
                return BadRequest("Ya existe un género con el nombre " + generoCreacion.Name);
            }


            var genero = mapper.Map<Genero>(generoCreacion);
            //I/O input out, es buena practica usar asincronismo
            context.Add(genero);
            //context.Add(generoCreacion);
            await context.SaveChangesAsync();//Recoge todos los objetos que han sido agg y los empuja a la DB
            return Ok();
        }

        [HttpPost("varios")]//endpoints
        public async Task<ActionResult> Post(GeneroCracionDTO[] generoCracionDTO)//ActionReslt representa una pag https un objeto json
        {
            var generos = mapper.Map<Genero[]>(generoCracionDTO);
            context.AddRange(generos);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()//Tipo de dato genero qe retornamos de una acion en endpoints
        {
            return await context.Generos.ToListAsync();
        }

        //Conectado, se llama asi porque obtengo un registro del ContextoDB y lo actualizo
        [HttpPut("{id:int}/nombre2")]
        public async Task<ActionResult> Put(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            if (genero is null)
            {
                return NotFound();
            }

            genero.Name = "Terror";
            //genero.Name = genero.Name + " 2";

            await context.SaveChangesAsync();
            return Ok();
        }

        //Desconectado
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GeneroCracionDTO generoCracionDTO)
        {
            var genero = mapper.Map<Genero>(generoCracionDTO);
            genero.Id = id;
            context.Update(genero);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        { 
            var filasAlteradas = await context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync();

            if (filasAlteradas == 0)
            {
                return NotFound();
            }
            //return NotFound(); Cambie a ok porque me salia error 404 y en el tutorial salia 204 sin error
            return Ok();  
        }


        //Manera anterior
        [HttpDelete("{id:int}/anterior")]
        public async Task<ActionResult> DeleteAnterior(int id)
        {
            //Pregunta si existe
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            if (genero is null)
            {
                return NotFound();
            }
            context.Remove(genero);
            //Luego lo elimina
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
