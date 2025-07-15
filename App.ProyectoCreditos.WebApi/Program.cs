
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

#region CONFIGURANDO SWAGGER
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CreditosAPI",
        Version = "v1",
        Description = "API para gestión de créditos",
        Contact = new OpenApiContact
        {
            Name = "Mario Leiva",
            Email = "mleiva@gmail.com",
            Url = new Uri("https://aka.ms/aspnet/openapi")
        },
    });
    x.MapType<string>(() => new OpenApiSchema { Nullable = true });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    x.IncludeXmlComments(xmlPath);
});


#endregion CONFIGURANDO SWAGGER

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
