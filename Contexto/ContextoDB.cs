using EFCore_Ejemplo.Entidades;
using EFCore_Ejemplo.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCore_Ejemplo.Contexto
{
    public class ContextoDB : DbContext
    {
        public ContextoDB(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aquí puedo realizar configuración con el Fluent API
            //override OnMode + tab
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Genero>().HasKey(g => g.Id);
            //modelBuilder.Entity<Genero>().Property(g => g.Name).HasMaxLength(150);

            //Para poder usar la configuracion assetsbli : Buscsa todas las clases que implementen IEntityTypeConfiguration de config
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            
            SeedingInicial1.Seed(modelBuilder); 
        }

        //Override ConfigureCon + tab
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //Configuro mi propiedad string para que por defecto el campo tome el valor max de 150
            configurationBuilder.Properties<string>().HaveMaxLength(150);    
        }

        //Convierto mi clase Genero en una entidad(tabla) en la DB
        public DbSet<Genero> Generos => Set<Genero>();
        public DbSet<Actor> Actores => Set<Actor>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Comentario> Comentarios => Set<Comentario>();
        public DbSet<PeliculaActor> PeliculasActores => Set<PeliculaActor>();

    }
}
