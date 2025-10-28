using descontinuarProductosBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar CORS para permitir peticiones desde la aplicación Windows Forms
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWindowsFormsApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Registrar el servicio de productos
builder.Services.AddScoped<IProductoService, ProductoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Productos Descontinuados",
        Version = "v1",
        Description = "API para gestionar productos descontinuados en la tienda"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Productos v1");
    });
}

// Habilitar CORS
app.UseCors("AllowWindowsFormsApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
