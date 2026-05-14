using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Api.Models.Livros.Requisicao;
using BibliotecaIA.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaIA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroAplicacao _livroAplicacao;

        public LivroController(ILivroAplicacao livroAplicacao)
        {
            _livroAplicacao = livroAplicacao;
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromBody] LivroCriar request)
        {
            try
            {
                var livroJaCadastrado = _livroAplicacao.Listar(true)
                    .Any(l =>
                        l.UsuarioID == request.UsuarioID &&
                        l.Titulo.ToLower() == request.Titulo.ToLower()
                    );

                if (livroJaCadastrado)
                {
                    return BadRequest("Este livro já foi cadastrado por este usuário.");
                }

                var livro = new Livro
                {
                    Titulo = request.Titulo,
                    Autor = request.Autor,
                    Genero = request.Genero,
                    QuantPaginas = request.QuantPaginas,
                    DataLeitura = request.DataLeitura,
                    Avaliacao = request.Avaliacao,
                    Comentario = request.Comentario,
                    UsuarioID = request.UsuarioID
                };

                var id = _livroAplicacao.Criar(livro);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Obter/{livroId}")]
        public IActionResult Obter(int livroId)
        {
            try
            {
                var livro = _livroAplicacao.Obter(livroId);
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Listar")]
        public IActionResult Listar([FromQuery] bool ativo = true)
        {
            try
            {
                var livros = _livroAplicacao.Listar(ativo);
                return Ok(livros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarPorUsuario/{usuarioId}")]
        public IActionResult ListarPorUsuario(int usuarioId)
        {
            try
            {
                var livros = _livroAplicacao.Listar(true)
                    .Where(l => l.UsuarioID == usuarioId)
                    .ToList();

                return Ok(livros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BuscarPorTitulo")]
        public IActionResult BuscarPorTitulo([FromQuery] string titulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(titulo))
                    return BadRequest("Título é obrigatório.");

                var livros = _livroAplicacao.Listar(true)
                    .Where(l => l.Titulo.ToLower().Contains(titulo.ToLower()))
                    .ToList();

                return Ok(livros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Deletar/{livroId}")]
        public IActionResult Deletar(int livroId)
        {
            try
            {
                _livroAplicacao.Deletar(livroId);
                return Ok("Livro deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Restaurar/{livroId}")]
        public IActionResult Restaurar(int livroId)
        {
            try
            {
                _livroAplicacao.Restaurar(livroId);
                return Ok("Livro restaurado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}