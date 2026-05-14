using System.Threading.Tasks;
using BibliotecaIA.Aplicacao.Models.Recomendacao;

namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface IRecomendacaoIAService
    {
        Task<RecomendacaoResultado> GerarRecomendacaoAsync(int usuarioID, string generoDesejado);
    }
}