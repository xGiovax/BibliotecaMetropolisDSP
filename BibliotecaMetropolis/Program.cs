using BibliotecaMetropolis.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization; // Necesitas esta nueva directiva

var builder = WebApplication.CreateBuilder(args);

// ======================================================================
// 1. CONFIGURACIÓN DE LOCALIZACIÓN
// ======================================================================

// Habilita la localización y define la carpeta 'Resources' para los archivos .resx
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Define la cultura soportada (solo español para este caso)
const string supportedCulture = "es";
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCulture)
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);

// Configuración de Entity Framework con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BibliotecaMetropolisBD")));

// Añade el soporte para la localización de vistas y DataAnnotations
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(); // <--- ESTO TRADUCE LOS ERRORES DE [Required], [Range], etc.

var app = builder.Build();

// ======================================================================
// 2. USO DE LOCALIZACIÓN
// ======================================================================

// 4. Aplica las opciones de localización al pipeline de la aplicación (antes de UseRouting)
app.UseRequestLocalization(localizationOptions);

// Configuración del entorno
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();