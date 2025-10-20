using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Editorial
    {
        [Key]
        public int IdEdit { get; set; }

        [Required, StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        // 🔹 Relación 1:N con Recurso
        public ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
    }
}
