using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Editorial
    {
        [Key]
        public int IdEdit { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [StringLength(300)]
        public string Descripcion { get; set; }

        // ✅ Nuevos campos para instituciones (opcional)
        [StringLength(150)]
        [Display(Name = "Correo de contacto")]
        public string? CorreoContacto { get; set; }

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [StringLength(200)]
        [Display(Name = "Sitio Web")]
        public string? SitioWeb { get; set; }

        // Relación con Recursos
        public ICollection<Recurso>? Recursos { get; set; }

    }
}
