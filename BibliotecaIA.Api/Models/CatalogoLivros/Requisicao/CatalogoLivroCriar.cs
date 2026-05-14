namespace BibliotecaIA.Api.Models.CatalogoLivros.Requisicao
{
    public class CatalogoLivroCriar
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int QuantPaginas { get; set; }
        public string Resumo { get; set; }
    }
}