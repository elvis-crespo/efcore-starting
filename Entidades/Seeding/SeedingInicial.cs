using Microsoft.EntityFrameworkCore;

namespace EFCore_Ejemplo.Entidades.Seeding
{
    public class SeedingInicial
    {
        public static void Seed(ModelBuilder modeloBuilder)
        {
            var rachelMcAdams = new Actor() { Id = 2, Name = "Rachel Anne McAdams", FechaNacimiento = new DateTime(1978, 11, 17), Fortuna = 15000 };
            var robertDowneyJunior = new Actor() { Id = 3, Name = "Robert Downey Jr", FechaNacimiento = new DateTime(1965, 4, 4), Fortuna = 18000 };
            
            modeloBuilder.Entity<Actor>().HasData(rachelMcAdams, robertDowneyJunior);

            var strange = new Pelicula() { Id = 2, Titulo = "Doctor Strange in the Multiverse of Madness", FechaEstreno = new DateTime(2022, 5, 13)};
            var avengers = new Pelicula() { Id = 3, Titulo = "Avengers Endgame", FechaEstreno = new DateTime(2022, 12, 13) };
            var spider = new Pelicula() { Id = 4, Titulo = "Spider-Man", FechaEstreno = new DateTime(2019, 10, 7) };

            modeloBuilder.Entity<Pelicula>().HasData(strange, avengers, spider);

            var comentStrange = new Comentario() { Id = 2, Recomendar = true, Contenido = "Simplemente Perfecta", PeliculaId = strange.Id };
            var comentAvenger = new Comentario() { Id = 3, Recomendar = true, Contenido = "Muy buena! ", PeliculaId = avengers.Id };
            var comentSpider = new Comentario() { Id = 4, Recomendar = false, Contenido = "No debieron hacer eso.", PeliculaId = spider.Id };

            modeloBuilder.Entity<Comentario>().HasData(comentAvenger, comentStrange, comentSpider);

            //GeneroPelicula muchos a muchos con salto
            var tablaGeneroPelicula = "GeneroPelicula";
            var generoIdPropiedad = "GenerosId";
            var peliculaIdPropiedad = "PeliculasId";

            var aventura = 5;
            var fantasia = 2;
            var musical = 4;

            modeloBuilder.Entity(tablaGeneroPelicula).HasData
                (
                    new Dictionary<string, object> { [generoIdPropiedad] = aventura, [peliculaIdPropiedad] = avengers.Id },
                    new Dictionary<string, object> { [generoIdPropiedad] = fantasia, [peliculaIdPropiedad] = strange.Id },
                    new Dictionary<string, object> { [generoIdPropiedad] = musical, [peliculaIdPropiedad] = spider.Id }
                );

            //Muchos a muchos sin salto
            var mcAdams = new PeliculaActor
            {
                ActorId = rachelMcAdams.Id,
                PeliculaId = strange.Id,
                Orden = 1,
                Personaje = "Cristine"
            };
            
            var robert = new PeliculaActor
            {
                ActorId = robertDowneyJunior.Id,
                PeliculaId = avengers.Id,
                Orden = 1,
                Personaje = "Iron Man"
            };

            var mcAdams2 = new PeliculaActor
            {
                ActorId = rachelMcAdams.Id,
                PeliculaId = spider.Id,
                Orden = 2,
                Personaje = "Cristine 2"
            };

            modeloBuilder.Entity<PeliculaActor>().HasData( mcAdams , robert, mcAdams2);
           
        }
    }
}
