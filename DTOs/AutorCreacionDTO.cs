using System.ComponentModel.DataAnnotations;

namespace EFCore_Ejemplo.DTOs
{
    public class AutorCreacionDTO
    {
        [StringLength(150)]
        public string Name { get; set; } = null!;
        public decimal Fortuna { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
