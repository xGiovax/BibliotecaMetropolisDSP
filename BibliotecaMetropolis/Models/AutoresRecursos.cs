namespace BibliotecaMetropolis.Models
{
    public class AutoresRecursos
    {
        public int IdRec { get; set; }
        public Recurso Recurso { get; set; } = default!;

        public int IdAutor { get; set; }
        public Autor Autor { get; set; } = default!;

        public bool EsPrincipal { get; set; }
    }
}
