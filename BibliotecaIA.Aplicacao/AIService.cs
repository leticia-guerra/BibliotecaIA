using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BibliotecaIA.Aplicacao.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BibliotecaIA.Aplicacao.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GerarRespostaAsync(string prompt)
        {
            var url = _configuration["GitHubModels:ApiUrl"];
            var token = _configuration["GitHubModels:Token"];
            var model = _configuration["GitHubModels:Model"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("BibliotecaIA");
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            var requestBody = new
            {
                model = model,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 2000
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Erro IA: {response.StatusCode} - {error}";
            }

            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);

            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }
    }
}