namespace EFCore_Ejemplo.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;    
        public decimal Fortuna { get; set; }
        public  DateTime FechaNacimiento { get; set; }
        public List<PeliculaActor> PeliculasActores { get; set; } = new List<PeliculaActor>();
    }
}
