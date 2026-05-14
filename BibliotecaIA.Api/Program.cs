using BibliotecaIA.Aplicacao;
using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Aplicacao.Services;
using BibliotecaIA.Repositorio;
using BibliotecaIA.Repositorio.Contexto;
using BibliotecaIA.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BibliotecaIAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddScoped<ILivroAplicacao, LivroAplicacao>();
builder.Services.AddScoped<ILivroRepositorio, LivroRepositorio>();

builder.Services.AddScoped<IRecomendacaoIAService, RecomendacaoIAService>();

builder.Services.AddHttpClient<IAIService, AIService>();

builder.Services.AddScoped<ICatalogoLivroRepositorio, CatalogoLivroRepositorio>();
builder.Services.AddScoped<ICatalogoLivroAplicacao, CatalogoLivroAplicacao>();

builder.Services.AddScoped<ILivroSqlAplicacao, LivroSqlAplicacao>();
builder.Services.AddScoped<ILivroSqlRepositorio, LivroSqlRepositorio>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirReact");

app.UseAuthorization();

app.MapControllers();

app.Run();