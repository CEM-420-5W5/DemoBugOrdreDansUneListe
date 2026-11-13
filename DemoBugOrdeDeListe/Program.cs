using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DemoBugOrdeDeListeContext") ?? throw new InvalidOperationException("Connection string 'DemoBugOrdeDeListeContext' not found.");

builder.Services.AddDbContext<DemoBugOrdeDeListeContext>(options => {
    options.UseSqlServer(connectionString);
    options.UseLazyLoadingProxies(true); // Active le lazy loading
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
