using Microsoft.EntityFrameworkCore;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Configuracoes;

namespace BibliotecaIA.Repositorio.Contexto
{
    public class BibliotecaIAContext : DbContext
    {
        private readonly DbContextOptions<BibliotecaIAContext> _options;

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<CatalogoLivro> CatalogoLivros { get; set; }

        public BibliotecaIAContext()
        {
        }

        public BibliotecaIAContext(DbContextOptions<BibliotecaIAContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
            modelBuilder.ApplyConfiguration(new LivroConfiguracoes());
            modelBuilder.ApplyConfiguration(new CatalogoLivroConfiguracoes());

            base.OnModelCreating(modelBuilder);
        }
    }
}