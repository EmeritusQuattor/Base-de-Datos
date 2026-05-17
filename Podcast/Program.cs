using Microsoft.EntityFrameworkCore;
using Podcast;
using Podcast.Repositories;
using Podcast.Repositories.UsuarioRepository;
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PodcastRepository>();
builder.Services.AddScoped<EpisodioRepository>();
builder.Services.AddScoped<ReproduccionRepository>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();