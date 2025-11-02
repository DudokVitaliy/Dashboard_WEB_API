using Dashboard_WEB_API.BLL.Services.Game;
using Dashboard_WEB_API.BLL.Services.Genre;
using Dashboard_WEB_API.BLL.Settings;
using Dashboard_WEB_API.DAL;
using Dashboard_WEB_API.DAL.Entities.Identity;
using Dashboard_WEB_API.DAL.Initializer;
using Dashboard_WEB_API.DAL.Repositories.GameRepositores;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDb"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkyOTcyODAwIiwiaWF0IjoiMTc2MTUwNjM0MCIsImFjY291bnRfaWQiOiIwMTlhMjFmNDQ1YTU3ZjJkOTZmYTYwMTQ3OWJmMDIwZCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazhnemJic253NDkyODU5Y2dqMTJlamYwIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.oj7mybddIU6rQTNpksDnsqmG9N5kG6rxlK8e1vuHzgdFU8N2vwbnVyjjptHYFNOLmKpR8_mCM0-Pcbvw4ijLxk0WtMDZiQCJz9Gc9JHUIuA3IDdQkhIrXu-icxaZiEHeWwfngTZbVxRirX9ZyeJWDuN4MeOkqR9UnpOAU_w_3CI21-h__BNkj7nfNOWG5o97yyXYGW3HFyIzh646RgUyjHPUenW3TtXMK0gG_onVaYuKCvHBYejZ52rwJqnlwx-KtGU_D9luV7vCWAu0UkbyZZZGIq5gddJeULZEhh5z5uSmQhDstqKCxnoLM64MCqU8QyseR0Yrm_azpGpvbqpTuA";
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

builder.Services.AddScoped<IGenreService, GenreService>();  
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

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

app.Seed();

app.Run();
