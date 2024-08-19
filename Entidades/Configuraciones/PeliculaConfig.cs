using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCore_Ejemplo.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            //Pelicula
            //modelBuilder.Entity<Pelicula>().Property(p => p.titulo).HasMaxLength(150);                       
            builder.Property(p => p.FechaEstreno).HasColumnType("date");
        }
    }
}
