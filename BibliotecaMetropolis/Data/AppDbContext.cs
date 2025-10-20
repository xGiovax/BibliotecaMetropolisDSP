using BibliotecaMetropolis.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMetropolis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 🔹 Tablas
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<AutoresRecursos> AutoresRecursos { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<TipoRecurso> TiposRecurso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Desactivar pluralización automática (opcional)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            // 🔹 Relación N:M entre Autor y Recurso
            modelBuilder.Entity<AutoresRecursos>()
                .HasKey(ar => new { ar.IdRec, ar.IdAutor });

            modelBuilder.Entity<AutoresRecursos>()
                .HasOne(ar => ar.Recurso)
                .WithMany(r => r.AutoresRecursos)
                .HasForeignKey(ar => ar.IdRec)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AutoresRecursos>()
                .HasOne(ar => ar.Autor)
                .WithMany(a => a.AutoresRecursos)
                .HasForeignKey(ar => ar.IdAutor)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Configurar las relaciones de Recurso para evitar FK duplicadas
            modelBuilder.Entity<Recurso>()
                .HasOne(r => r.Pais)
                .WithMany(p => p.Recursos)
                .HasForeignKey(r => r.IdPais)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recurso>()
                .HasOne(r => r.TipoRecurso)
                .WithMany(t => t.Recursos)
                .HasForeignKey(r => r.IdTipoR)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recurso>()
                .HasOne(r => r.Editorial)
                .WithMany(e => e.Recursos)
                .HasForeignKey(r => r.IdEdit)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
