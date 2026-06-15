namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface IAIService
    {// gera respostas com base em prompts fornecidos
        Task<string> GerarRespostaAsync(string prompt);
    }
}