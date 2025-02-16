﻿namespace EFCore_Ejemplo.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public HashSet<Comentario> Comentarios { get; set; } = new HashSet<Comentario>();
        public HashSet<Genero> Generos { get; set; } = new HashSet<Genero>();

        //Se usa list para ordenar, Hashset no garantiza que se ordenen
        public List<PeliculaActor> PeliculasActores { get; set; } = new List<PeliculaActor>();//Es una entidad intermedia
    }
}
