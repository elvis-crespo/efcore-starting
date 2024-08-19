using System.ComponentModel.DataAnnotations;

namespace EFCore_Ejemplo.DTOs
{
    public class GeneroCracionDTO
    {
        [StringLength(maximumLength: 150)]
        public string Name { get; set; } = null!;
    }
}
