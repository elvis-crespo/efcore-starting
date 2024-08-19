using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCore_Ejemplo.Entidades.Configuraciones
{
    public class ComentariosConfig : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            //Comentario
            builder.Property(c => c.Contenido).HasMaxLength(500);
        }
    }
}
