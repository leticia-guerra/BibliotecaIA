using System.Linq;
using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Aplicacao.Models.Recomendacao;

namespace BibliotecaIA.Aplicacao.Services
{
    public class RecomendacaoIAService : IRecomendacaoIAService
    {
        private readonly ILivroAplicacao _livroAplicacao;
        private readonly ICatalogoLivroAplicacao _catalogoAplicacao;
        private readonly IAIService _aiService;
        public RecomendacaoIAService(
            ILivroAplicacao livroAplicacao,
            ICatalogoLivroAplicacao catalogoAplicacao,
            IAIService aiService)
        {
            _livroAplicacao = livroAplicacao;
            _catalogoAplicacao = catalogoAplicacao;
            _aiService = aiService;
        }

        public async Task<RecomendacaoResultado> GerarRecomendacaoAsync(int usuarioID, string generoDesejado)
        {
            if (string.IsNullOrWhiteSpace(generoDesejado))
            {
                return new RecomendacaoResultado
                {
                    UsuarioID = usuarioID,
                    PerfilLeitura = "Gênero não informado.",
                    Recomendacoes = new List<LivroRecomendadoResultado>(),
                    Justificativa = "Informe um gênero para receber recomendações.",
                    ExplicacaoIA = string.Empty
                };
            }

            var historicoUsuario = _livroAplicacao
                .ListarPorUsuario(usuarioID)
                .Where(l => l.Genero.ToLower() == generoDesejado.ToLower())
                .ToList();

            if (!historicoUsuario.Any())
            {
                return new RecomendacaoResultado
                {
                    UsuarioID = usuarioID,
                    PerfilLeitura = $"O usuário ainda não possui leituras cadastradas no gênero {generoDesejado}.",
                    Recomendacoes = new List<LivroRecomendadoResultado>(),
                    Justificativa = "Cadastre livros desse gênero para receber recomendações mais precisas.",
                    ExplicacaoIA = string.Empty
                };
            }

            var livrosLidos = historicoUsuario
                .Select(l => l.Titulo.ToLower())
                .ToHashSet();

            var todosLivros = _livroAplicacao
                .ObterTodos()
                .Where(l => l.UsuarioID != usuarioID)
                .Where(l => l.Genero.ToLower() == generoDesejado.ToLower())
                .ToList();

            var livrosMaisIndicadosPorOutrosUsuarios = todosLivros
                .GroupBy(l => l.Titulo)
                .Select(g => new
                {
                    Titulo = g.Key,
                    Frequencia = g.Count(),
                    MediaAvaliacao = g.Average(x => x.Avaliacao)
                })
                .OrderByDescending(x => x.MediaAvaliacao)
                .ThenByDescending(x => x.Frequencia)
                .Select(x => x.Titulo)
                .ToList();

            var catalogoDoGenero = _catalogoAplicacao
                .ListarPorGenero(generoDesejado)
                .ToList();

            var recomendacoes = catalogoDoGenero
                .Where(c => livrosMaisIndicadosPorOutrosUsuarios.Contains(c.Titulo))
                .Where(c => !livrosLidos.Contains(c.Titulo.ToLower()))
                .Select(c => new LivroRecomendadoResultado
                {
                    Titulo = c.Titulo,
                    Autor = c.Autor,
                    Genero = c.Genero,
                    Resumo = c.Resumo
                })
                .Take(3)
                .ToList();

            if (!recomendacoes.Any())
            {
                recomendacoes = catalogoDoGenero
                    .Where(c => !livrosLidos.Contains(c.Titulo.ToLower()))
                    .Select(c => new LivroRecomendadoResultado
                    {
                        Titulo = c.Titulo,
                        Autor = c.Autor,
                        Genero = c.Genero,
                        Resumo = c.Resumo
                    })
                    .Take(3)
                    .ToList();
            }

            var mediaAvaliacoes = historicoUsuario.Average(l => l.Avaliacao);

            // IA começa aqui
            var explicacaoIA = "No momento não foi possível gerar explicação da IA.";

            var livrosLidosTexto = string.Join(", ", historicoUsuario.Select(l => l.Titulo));
            var livrosRecomendadosTexto = string.Join(", ", recomendacoes.Select(r => r.Titulo));

            if (recomendacoes.Any())
            {
                var prompt = $"Você é um assistente de recomendação de livros. " +
                            $"O usuário quer ler no gênero {generoDesejado}. " +
                            $"Ele já leu: {livrosLidosTexto}. " +
                            $"A média de avaliação dele nesse gênero é {mediaAvaliacoes:F1}. " +
                            $"Os livros recomendados pelo sistema foram: {livrosRecomendadosTexto}. " +
                            $"Explique em português, de forma curta e objetiva, por que essas recomendações combinam com o perfil desse leitor.";

                explicacaoIA = await _aiService.GerarRespostaAsync(prompt);
            }
            else
            {
                explicacaoIA = "No momento não há recomendações suficientes para este gênero.";
            }

            return new RecomendacaoResultado
            {
                UsuarioID = usuarioID,
                PerfilLeitura = $"O usuário demonstra interesse por livros do gênero {generoDesejado} com média de avaliação {mediaAvaliacoes:F1}.",
                Recomendacoes = recomendacoes,
                Justificativa = "As recomendações foram geradas com base no histórico do usuário, nos livros bem avaliados por outros usuários e no catálogo interno.",
                ExplicacaoIA = explicacaoIA
            };
        }
    }
}