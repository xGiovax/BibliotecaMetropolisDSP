using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Pais
    {
        [Key]
        public int IdPais { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        // 🔹 Relación 1:N con Recurso
        public ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
    }
}
