using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Api.Models.CatalogoLivros.Requisicao;
using BibliotecaIA.Api.Models.CatalogoLivros.Resposta;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoLivroController : ControllerBase
    {
        private readonly ICatalogoLivroAplicacao _catalogoAplicacao;

        public CatalogoLivroController(ICatalogoLivroAplicacao catalogoAplicacao)
        {
            _catalogoAplicacao = catalogoAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public ActionResult Criar([FromBody] CatalogoLivroCriar livroCriar)
        {
            try
            {
                var livroDominio = new CatalogoLivro
                {
                    Titulo = livroCriar.Titulo,
                    Autor = livroCriar.Autor,
                    Genero = livroCriar.Genero,
                    QuantPaginas = livroCriar.QuantPaginas,
                    Resumo = livroCriar.Resumo
                };

                var livroID = _catalogoAplicacao.Criar(livroDominio);

                return Ok(new CatalogoLivroResponse
                {
                    Id = livroID,
                    Titulo = livroCriar.Titulo,
                    Autor = livroCriar.Autor,
                    Genero = livroCriar.Genero,
                    QuantPaginas = livroCriar.QuantPaginas,
                    Resumo = livroCriar.Resumo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public ActionResult Atualizar([FromBody] CatalogoLivroAtualizar livroAtualizar)
        {
            try
            {
                var livroDominio = new CatalogoLivro
                {
                    ID = livroAtualizar.Id,
                    Titulo = livroAtualizar.Titulo,
                    Autor = livroAtualizar.Autor,
                    Genero = livroAtualizar.Genero,
                    QuantPaginas = livroAtualizar.QuantPaginas,
                    Resumo = livroAtualizar.Resumo
                };

                _catalogoAplicacao.Atualizar(livroDominio);

                return Ok("Livro do catálogo atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{livroID}")]
        public ActionResult Obter([FromRoute] int livroID)
        {
            try
            {
                var livro = _catalogoAplicacao.Obter(livroID);

                return Ok(new CatalogoLivroResponse
                {
                    Id = livro.ID,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    Genero = livro.Genero,
                    QuantPaginas = livro.QuantPaginas,
                    Resumo = livro.Resumo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar/{ativo}")]
        public ActionResult Listar([FromRoute] bool ativo)
        {
            try
            {
                var livros = _catalogoAplicacao.Listar(ativo);

                var resposta = livros.Select(livro => new CatalogoLivroResponse
                {
                    Id = livro.ID,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    Genero = livro.Genero,
                    QuantPaginas = livro.QuantPaginas,
                    Resumo = livro.Resumo
                });

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListarPorGenero/{genero}")]
        public ActionResult ListarPorGenero([FromRoute] string genero)
        {
            try
            {
                var livros = _catalogoAplicacao.ListarPorGenero(genero);

                var resposta = livros.Select(livro => new CatalogoLivroResponse
                {
                    Id = livro.ID,
                    Titulo = livro.Titulo,
                    Autor = livro.Autor,
                    Genero = livro.Genero,
                    QuantPaginas = livro.QuantPaginas,
                    Resumo = livro.Resumo
                });

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
    [HttpGet]
    [Route("BuscarPorTitulo")]
    public ActionResult BuscarPorTitulo([FromQuery] string titulo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return BadRequest("Título é obrigatório.");

            var livros = _catalogoAplicacao.Listar(true)
                .Where(livro => livro.Titulo.ToLower().Contains(titulo.ToLower()))
                .ToList();

            var resposta = livros.Select(livro => new CatalogoLivroResponse
            {
                Id = livro.ID,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                Genero = livro.Genero,
                QuantPaginas = livro.QuantPaginas,
                Resumo = livro.Resumo
            });

            return Ok(resposta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
}