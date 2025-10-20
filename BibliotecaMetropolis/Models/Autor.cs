using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Autor
    {
        [Key]
        public int IdAutor { get; set; }

        [Required, StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        // Relación N:M con Recurso
        public ICollection<AutoresRecursos> AutoresRecursos { get; set; } = new List<AutoresRecursos>();
    }
}
