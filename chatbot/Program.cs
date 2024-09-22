using chatbot.Endpoints;
using chatbot.Repositorios;
using chatbot.Servicios;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//Inicio de area de los servicios
builder.Services.AddAuthorization();
builder.Services.AddCors(opciones => {

    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();//permitir que swagger permita explorar los endpoints y listarlos
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Chatbot API",
        Description = "Este es un API de Chatbot",
    });
});//usar swaggerGen

builder.Services.AddTransient<IAlmacenadorArchivos,AlmacenadorArchivosAzure>();
builder.Services.AddTransient<IRepositorioProductos, RepositorioProductos>();
builder.Services.AddTransient<IRepositorioOpcionesChatbot, RepositorioOpcionesChatbot>();
builder.Services.AddTransient<IRepositorioUsuarios,RepositorioUsarios>();
builder.Services.AddTransient<IRepositorioCategorias, RepositorioCategorias>();
builder.Services.AddTransient<IRepositorioRespuestasSimples,RepositorioRespuestasSimples>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

//Me va permitir configurar nuestra aplicacion para que retorne codigos de status cuando haya un error.
app.UseStatusCodePages();

app.UseStaticFiles();//Para que el ciente de la aplicacion pueda acceder a los archivos de la carpeta wwwroot
app.UseCors();

app.UseOutputCache();

app.UseAuthorization();

app.MapGroup("/api/productos").MapProductos();
app.MapGroup("/api/opciones").MapOpcionesChatbot();
app.MapGroup("/api/usuarios").MapUsuarios();
app.MapGroup("/api/categorias").MapCategorias();
app.MapGroup("/api/respuestasSimples").MapRespuestasSimples();
app.Run();
