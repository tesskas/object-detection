using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IVisualRecognitionService, VisualRecognitionService>();

builder.Services.AddDbContext<VisualRecognitionDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConnectionString")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();