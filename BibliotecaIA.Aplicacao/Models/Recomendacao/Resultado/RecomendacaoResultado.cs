using System.Collections.Generic;

namespace BibliotecaIA.Aplicacao.Models.Recomendacao
{
    public class RecomendacaoResultado
    {
        public int UsuarioID { get; set; }
        public string PerfilLeitura { get; set; }
        public List<LivroRecomendadoResultado> Recomendacoes { get; set; }
        public string Justificativa { get; set; }
        public string ExplicacaoIA { get; set; }
    }
}