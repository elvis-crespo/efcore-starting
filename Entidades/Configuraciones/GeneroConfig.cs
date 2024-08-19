using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore_Ejemplo.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            var documental = new Genero { Id = 2, Name = "Documental"};
            var fantasia = new Genero { Id = 3, Name = "Fantasía"};
            var musical = new Genero { Id = 4, Name = "Musical"};
            var aventura = new Genero { Id = 5, Name = "Aventura"};

            builder.HasData(documental, fantasia, musical, aventura);

            //Definir que solo puede exisitir un unico genero
            builder.HasIndex(u => u.Name).IsUnique();
        }
    }
}
