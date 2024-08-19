using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Ejemplo.Entidades.Configuraciones
{
    public class PeliculaAcatorConfig : IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
            //Llave primaria compuesta
            builder.HasKey(pa => new { pa.ActorId, pa.PeliculaId });
        }
    }
}
