using System.Text;
using BibliotecaIA.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaIA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;
        private readonly ILivroSqlAplicacao _livroSqlAplicacao;

        public AIController(IAIService aiService, ILivroSqlAplicacao livroSqlAplicacao)
        {
            _aiService = aiService;
            _livroSqlAplicacao = livroSqlAplicacao;
        }

        [HttpPost("completar")]
        public async Task<IActionResult> Completar([FromBody] string prompt)
        {
            var resposta = await _aiService.GerarRespostaAsync(prompt);
            return Ok(resposta);
        }

        [HttpGet("recomendar-por-usuario/{usuarioId}")]
        public async Task<IActionResult> RecomendarPorUsuario(int usuarioId, [FromQuery] string genero)
        {
            if (string.IsNullOrWhiteSpace(genero))
            {
                return BadRequest("Gênero é obrigatório.");
            }

            var livros = await _livroSqlAplicacao.ListarLivrosPorUsuarioAsync(usuarioId);

            var promptBuilder = new StringBuilder();

            promptBuilder.AppendLine("Você é uma IA especialista em recomendação de livros.");
            promptBuilder.AppendLine($"Gênero escolhido pelo usuário: {genero}.");
            promptBuilder.AppendLine("Recomende exatamente 3 livros.");
            promptBuilder.AppendLine("Não recomende livros repetidos.");
            promptBuilder.AppendLine("A justificativa de cada recomendação deve ter entre 3 e 5 linhas.");
            promptBuilder.AppendLine();
            promptBuilder.AppendLine("Responda exatamente neste formato:");
            promptBuilder.AppendLine();
            promptBuilder.AppendLine("TÍTULO: nome do livro");
            promptBuilder.AppendLine("AUTOR: nome do autor");
            promptBuilder.AppendLine("PÁGINAS: quantidade aproximada de páginas");
            promptBuilder.AppendLine("JUSTIFICATIVA: explicação detalhada");
            promptBuilder.AppendLine("---");
            promptBuilder.AppendLine("TÍTULO: nome do livro");
            promptBuilder.AppendLine("AUTOR: nome do autor");
            promptBuilder.AppendLine("PÁGINAS: quantidade aproximada de páginas");
            promptBuilder.AppendLine("JUSTIFICATIVA: explicação detalhada");
            promptBuilder.AppendLine("---");
            promptBuilder.AppendLine("TÍTULO: nome do livro");
            promptBuilder.AppendLine("AUTOR: nome do autor");
            promptBuilder.AppendLine("PÁGINAS: quantidade aproximada de páginas");
            promptBuilder.AppendLine("JUSTIFICATIVA: explicação detalhada");
            promptBuilder.AppendLine();

            if (livros == null || !livros.Any())
            {
                promptBuilder.AppendLine("O usuário ainda não possui histórico de leitura.");
                promptBuilder.AppendLine($"Recomende livros populares do gênero {genero}, adequados para iniciar nesse gênero.");
            }
            else
            {
                promptBuilder.AppendLine("Use o histórico de leitura abaixo como base para personalizar as recomendações.");
                promptBuilder.AppendLine("Não recomende livros que já aparecem no histórico.");
                promptBuilder.AppendLine();
                promptBuilder.AppendLine("Histórico do usuário:");

                foreach (var livro in livros)
                {
                    promptBuilder.AppendLine(
                        $"- Título: {livro.Titulo} | Autor: {livro.Autor} | Gênero: {livro.Genero} | Páginas: {livro.QuantPaginas}");
                }
            }

            promptBuilder.AppendLine();
            promptBuilder.AppendLine("Responda em português do Brasil.");

            var resposta = await _aiService.GerarRespostaAsync(promptBuilder.ToString());

            return Ok(resposta);
        }
    }
}