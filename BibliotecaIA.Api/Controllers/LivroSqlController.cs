using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BibliotecaIA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroSqlController : ControllerBase
    {
        private readonly ILivroSqlAplicacao _livroSqlAplicacao;

        public LivroSqlController(ILivroSqlAplicacao livroSqlAplicacao)
        {
            _livroSqlAplicacao = livroSqlAplicacao;
        }

        [HttpGet("listar-por-usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPorUsuario(int usuarioId)
        {
            var livros = await _livroSqlAplicacao.ListarLivrosPorUsuarioAsync(usuarioId);
            return Ok(livros);
        }

        [HttpPost("inserir-por-usuario")]
        public async Task<IActionResult> InserirPorUsuario([FromBody] Livro livro)
        {
            await _livroSqlAplicacao.InserirLivroPorUsuarioAsync(livro);
            return Ok("Livro do usuário inserido com sucesso.");
        }

        [HttpGet("buscar-por-usuario/{livroId}/{usuarioId}")]
        public async Task<IActionResult> BuscarPorIdEUsuario(int livroId, int usuarioId)
        {
            var livro = await _livroSqlAplicacao.BuscarLivroPorIdEUsuarioAsync(livroId, usuarioId);

            if (livro == null)
                return NotFound("Livro não encontrado para esse usuário.");

            return Ok(livro);
        }

        [HttpPut("atualizar-usuario")]
        public async Task<IActionResult> AtualizarLivroUsuario([FromBody] Livro livro)
        {
            await _livroSqlAplicacao.AtualizarLivroUsuarioAsync(livro);
            return Ok("Livro atualizado com sucesso.");
        }

        [HttpDelete("excluir-usuario/{livroId}/{usuarioId}")]
        public async Task<IActionResult> ExcluirLivroUsuario(int livroId, int usuarioId)
        {
            await _livroSqlAplicacao.ExcluirLivroUsuarioAsync(livroId, usuarioId);
            return Ok("Livro excluído com sucesso.");
        }

        [HttpGet("quantidade-por-usuario/{usuarioId}")]
        public async Task<IActionResult> QuantidadeLivrosPorUsuario(int usuarioId)
        {
            var quantidade = await _livroSqlAplicacao.ObterQuantidadeLivrosPorUsuarioAsync(usuarioId);
            return Ok(quantidade);
        }

        [HttpGet("total-paginas-por-usuario/{usuarioId}")]
        public async Task<IActionResult> TotalPaginasPorUsuario(int usuarioId)
        {
            var totalPaginas = await _livroSqlAplicacao.ObterTotalPaginasPorUsuarioAsync(usuarioId);
            return Ok(totalPaginas);
        }
    }
}