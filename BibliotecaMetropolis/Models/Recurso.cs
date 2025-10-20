using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMetropolis.Models
{
    public class Recurso
    {
        [Key]
        public int IdRec { get; set; }

        [Required, StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [Range(1500, 2100)]
        [Display(Name = "Año de Publicación")]
        public int AnnoPublic { get; set; }

        [StringLength(50)]
        public string? Edicion { get; set; }

        [StringLength(300)]
        [Display(Name = "Palabras de Búsqueda")]
        public string? PalabrasBusqueda { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        // 🔹 Campos adicionales solicitados en el texto del caso
        [Range(1, 10000)]
        [Display(Name = "Cantidad de Unidades Compradas")]
        public int CantidadUnidades { get; set; }

        [Range(0.0, 100000.0)]
        [Display(Name = "Precio Individual")]
        [Precision(10, 2)] // 🔹 Nuevo: define que tendrá 10 dígitos en total, 2 decimales
        public decimal PrecioIndividual { get; set; }


        // 🔹 Relaciones
        [ForeignKey("Pais")]
        public int IdPais { get; set; }
        public Pais? Pais { get; set; }

        [ForeignKey("TipoRecurso")]
        public int IdTipoR { get; set; }
        public TipoRecurso? TipoRecurso { get; set; }

        [ForeignKey("Editorial / Institucion educativa")]
        public int IdEdit { get; set; }
        public Editorial? Editorial { get; set; }


        // 🔹 Relación N:M con Autor
        public ICollection<AutoresRecursos> AutoresRecursos { get; set; } = new List<AutoresRecursos>();
    }
}
