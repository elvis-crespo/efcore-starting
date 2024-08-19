using System.ComponentModel.DataAnnotations;

namespace EFCore_Ejemplo.Entidades
{
    public class Genero
    {
        //[Key] //Por convención reconoce que id es una key, por anotación debemos colocar [Key]
        public int Id { get; set; }
        //[StringLength(maximumLength: 150)]
       
        public string Name { get; set; } = null!;
        public HashSet<Pelicula> Peliculas { get; set; } = new HashSet<Pelicula>();
    }
}
