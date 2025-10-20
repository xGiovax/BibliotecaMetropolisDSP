using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class TipoRecurso
    {
        [Key]
        public int IdTipoR { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        // 🔹 Relación 1:N con Recurso
        public ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
    }
}
