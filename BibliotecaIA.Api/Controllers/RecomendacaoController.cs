//primeira IA pensada
using Microsoft.AspNetCore.Mvc;
using BibliotecaIA.Aplicacao.Interfaces;

namespace BibliotecaIA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        private readonly IRecomendacaoIAService _recomendacaoService;

        public RecomendacaoController(IRecomendacaoIAService recomendacaoService)
        {
            _recomendacaoService = recomendacaoService;
        }

        [HttpGet]
        [Route("{usuarioID}/{generoDesejado}")]
        public async Task<ActionResult> ObterRecomendacao([FromRoute] int usuarioID, [FromRoute] string generoDesejado)
        {
            try
            {
                var resultado = await _recomendacaoService.GerarRecomendacaoAsync(usuarioID, generoDesejado);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}