using System;

namespace BibliotecaIA.Api.Models.Livros.Requisicao
{
    public class LivroCriar
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int QuantPaginas { get; set; }
        public DateTime DataLeitura { get; set; }
        public int Avaliacao { get; set; }
        public string Comentario { get; set; }
        public int UsuarioID { get; set; }
    }
}