using BibliotecaIA.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaIA.Repositorio.Interfaces
{
    public interface ILivroSqlRepositorio
    {
      

        Task<IEnumerable<Livro>> ListarLivrosPorUsuarioAsync(int usuarioId);
        Task InserirLivroPorUsuarioAsync(Livro livro);
        Task<Livro> BuscarLivroPorIdEUsuarioAsync(int livroId, int usuarioId);
        Task AtualizarLivroUsuarioAsync(Livro livro);
        Task ExcluirLivroUsuarioAsync(int livroId, int usuarioId);
        Task<int> ObterQuantidadeLivrosPorUsuarioAsync(int usuarioId);
        Task<int> ObterTotalPaginasPorUsuarioAsync(int usuarioId);
    }
}