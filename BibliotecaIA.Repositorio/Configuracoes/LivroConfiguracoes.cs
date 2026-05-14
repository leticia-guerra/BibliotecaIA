using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Repositorio.Configuracoes
{
    public class LivroConfiguracoes : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro").HasKey(x => x.ID);

            builder.Property(nameof(Livro.ID)).HasColumnName("LivroId");
            builder.Property(nameof(Livro.Titulo)).HasColumnName("Titulo").IsRequired(true);
            builder.Property(nameof(Livro.Autor)).HasColumnName("Autor").IsRequired(true);
            builder.Property(nameof(Livro.Genero)).HasColumnName("Genero").IsRequired(true);
            builder.Property(nameof(Livro.QuantPaginas)).HasColumnName("QuantPaginas").IsRequired(true);
            builder.Property(nameof(Livro.DataLeitura)).HasColumnName("DataLeitura").IsRequired(true);
            builder.Property(nameof(Livro.Comentario)).HasColumnName("Comentario").IsRequired(false);
            builder.Property(nameof(Livro.UsuarioID)).HasColumnName("UsuarioID").IsRequired(true);
            builder.Property(nameof(Livro.Avaliacao)).HasColumnName("Avaliacao").IsRequired(true);
            builder.Property(nameof(Livro.Ativo)).HasColumnName("Ativo").IsRequired(true);
        }
    }
}