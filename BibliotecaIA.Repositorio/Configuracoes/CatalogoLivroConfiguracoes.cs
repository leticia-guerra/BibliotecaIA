using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Repositorio.Configuracoes
{
    public class CatalogoLivroConfiguracoes : IEntityTypeConfiguration<CatalogoLivro>
    {
        public void Configure(EntityTypeBuilder<CatalogoLivro> builder)
        {
            builder.ToTable("CatalogoLivro").HasKey(x => x.ID);

            builder.Property(nameof(CatalogoLivro.ID)).HasColumnName("CatalogoLivroId");
            builder.Property(nameof(CatalogoLivro.Titulo)).IsRequired(true);
            builder.Property(nameof(CatalogoLivro.Autor)).IsRequired(true);
            builder.Property(nameof(CatalogoLivro.Genero)).IsRequired(true);
            builder.Property(nameof(CatalogoLivro.QuantPaginas)).IsRequired(true);
            builder.Property(nameof(CatalogoLivro.Resumo)).IsRequired(false);
            builder.Property(nameof(CatalogoLivro.Ativo)).IsRequired(true);
        }
    }
}