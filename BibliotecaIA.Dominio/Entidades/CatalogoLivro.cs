namespace BibliotecaIA.Dominio.Entidades
{
    public class CatalogoLivro
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int QuantPaginas { get; set; }
        public string Resumo { get; set; }
        public bool Ativo { get; set; }

        public CatalogoLivro()
        {
            Ativo = true;
        }

        public void Deletar()
        {
            Ativo = false;
        }

        public void Restaurar()
        {
            Ativo = true;
        }
    }
}