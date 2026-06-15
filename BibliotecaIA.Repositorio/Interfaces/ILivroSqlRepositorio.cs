using BibliotecaIA.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
// Interface para o repositório de livros utilizando Dapper para acesso direto ao banco de dados
namespace BibliotecaIA.Repositorio.Interfaces
{
    public interface ILivroSqlRepositorio
    {
      // Implementação dos métodos para manipulação de livros no banco de dados

        Task<IEnumerable<Livro>> ListarLivrosPorUsuarioAsync(int usuarioId);
        Task InserirLivroPorUsuarioAsync(Livro livro);
        Task<Livro> BuscarLivroPorIdEUsuarioAsync(int livroId, int usuarioId);
        Task AtualizarLivroUsuarioAsync(Livro livro);
        Task ExcluirLivroUsuarioAsync(int livroId, int usuarioId);
        Task<int> ObterQuantidadeLivrosPorUsuarioAsync(int usuarioId);
        Task<int> ObterTotalPaginasPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<Livro>> ListarLivrosViewAsync();
    }
}