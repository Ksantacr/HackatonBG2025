using bg.hackathon.alphahackers.api.middleware;
using bg.hackathon.alphahackers.infrastructure.ioc;
using bg.hackathon.alphahackers.application.ioc;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllers();  // Habilitar controladores MVC
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Hackathon BG API",
        Version = "v1",
        Description = "API para gestión de clientes y productos"
    });
});

// Configurar dependencias
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hackathon BG API v1");
        c.RoutePrefix = "swagger";  // Acceder en /swagger
    });
}

app.UseHttpsRedirection();
app.UseRouting();

// Middlewares personalizados
app.UseMiddleware<ExceptionMiddleware>();

// Habilitar endpoints de controladores
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();