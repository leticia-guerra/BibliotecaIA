using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaIA.Aplicacao
{
    public class LivroSqlAplicacao : ILivroSqlAplicacao
    {
        private readonly ILivroSqlRepositorio _livroSqlRepositorio;

        public LivroSqlAplicacao(ILivroSqlRepositorio livroSqlRepositorio)
        {
            _livroSqlRepositorio = livroSqlRepositorio;
        }

       
        public async Task<IEnumerable<Livro>> ListarLivrosPorUsuarioAsync(int usuarioId)
        {
            return await _livroSqlRepositorio.ListarLivrosPorUsuarioAsync(usuarioId);
        }

        public async Task InserirLivroPorUsuarioAsync(Livro livro)
        {
            await _livroSqlRepositorio.InserirLivroPorUsuarioAsync(livro);
        }

        public async Task<Livro> BuscarLivroPorIdEUsuarioAsync(int livroId, int usuarioId)
        {
            return await _livroSqlRepositorio.BuscarLivroPorIdEUsuarioAsync(livroId, usuarioId);
        }

        public async Task AtualizarLivroUsuarioAsync(Livro livro)
        {
            await _livroSqlRepositorio.AtualizarLivroUsuarioAsync(livro);
        }

        public async Task ExcluirLivroUsuarioAsync(int livroId, int usuarioId)
        {
            await _livroSqlRepositorio.ExcluirLivroUsuarioAsync(livroId, usuarioId);
        }

        public async Task<int> ObterQuantidadeLivrosPorUsuarioAsync(int usuarioId)
        {
            return await _livroSqlRepositorio.ObterQuantidadeLivrosPorUsuarioAsync(usuarioId);
        }

        public async Task<int> ObterTotalPaginasPorUsuarioAsync(int usuarioId)
        {
            return await _livroSqlRepositorio.ObterTotalPaginasPorUsuarioAsync(usuarioId);
        }
        public async Task<IEnumerable<Livro>> ListarLivrosViewAsync()
        {
            return await _livroSqlRepositorio.ListarLivrosViewAsync();
        }
    }
}