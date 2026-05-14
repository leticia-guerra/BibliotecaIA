namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface IAIService
    {
        Task<string> GerarRespostaAsync(string prompt);
    }
}