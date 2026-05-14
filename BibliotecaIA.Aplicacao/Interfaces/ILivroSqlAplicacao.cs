using BibliotecaIA.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface ILivroSqlAplicacao
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