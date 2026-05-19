using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BibliotecaIA.Repositorio
{
    public class LivroSqlRepositorio : ILivroSqlRepositorio
    {
        private readonly string _connectionString;

        public LivroSqlRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CriarConexao()
        {
            return new SqlConnection(_connectionString);
        }
        // Implementação dos métodos para manipulação de livros no banco de dados
        public async Task<IEnumerable<Livro>> ListarLivrosPorUsuarioAsync(int usuarioId)
        {
            using var conexao = CriarConexao();
            // Chamando a stored procedure para listar os livros do usuário
            return await conexao.QueryAsync<Livro>(
                "sp_ListarLivrosPorUsuario",
                new { usuarioId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task InserirLivroPorUsuarioAsync(Livro livro)
        {
            using var conexao = CriarConexao();
            // Chamando a stored procedure para inserir um livro para o usuário
            var parametros = new
            {
                titulo = livro.Titulo,
                autor = livro.Autor,
                genero = livro.Genero,
                paginas = livro.QuantPaginas,
                usuarioId = livro.UsuarioID
            };

            await conexao.ExecuteAsync(
                "sp_InserirLivroPorUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Livro> BuscarLivroPorIdEUsuarioAsync(int livroId, int usuarioId)
        { // Chamando a stored procedure para buscar o livro do usuário por ID
            using var conexao = CriarConexao();

            return await conexao.QueryFirstOrDefaultAsync<Livro>(
                "sp_BuscarLivroPorIdEUsuario",
                new { livroId, usuarioId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task AtualizarLivroUsuarioAsync(Livro livro)
        { // Chamando a stored procedure para atualizar o livro do usuário
            using var conexao = CriarConexao();

            var parametros = new
            {
                livroId = livro.ID,
                titulo = livro.Titulo,
                autor = livro.Autor,
                genero = livro.Genero,
                paginas = livro.QuantPaginas,
                usuarioId = livro.UsuarioID
            };

            await conexao.ExecuteAsync(
                "sp_AtualizarLivroUsuario",
                parametros,
                commandType: CommandType.StoredProcedure);
        }

        public async Task ExcluirLivroUsuarioAsync(int livroId, int usuarioId)
        {
            using var conexao = CriarConexao();
            // Chamando a stored procedure para excluir o livro do usuário
            await conexao.ExecuteAsync(
                "sp_ExcluirLivroUsuario",
                new { livroId, usuarioId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> ObterQuantidadeLivrosPorUsuarioAsync(int usuarioId)
        { // Chamando a função para obter a quantidade de livros do usuário
            using var conexao = CriarConexao();

            return await conexao.ExecuteScalarAsync<int>(
                "SELECT dbo.fn_QuantidadeLivrosPorUsuario(@usuarioId)",
                new { usuarioId });
        }

        public async Task<int> ObterTotalPaginasPorUsuarioAsync(int usuarioId)
        {
            using var conexao = CriarConexao();
            // Chamando a função para obter o total de páginas dos livros do usuário
            return await conexao.ExecuteScalarAsync<int>(
                "SELECT dbo.fn_TotalPaginasPorUsuario(@usuarioId)",
                new { usuarioId });
        }
        public async Task<IEnumerable<Livro>> ListarLivrosViewAsync()
        {// Chamando a view para listar os livros do usuário
            using var conexao = CriarConexao();

            return await conexao.QueryAsync<Livro>(
                "SELECT * FROM vw_Livros");
        }
    }
}