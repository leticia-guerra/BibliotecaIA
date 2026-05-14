using System;

namespace BibliotecaIA.Dominio.Entidades
{
    public class Livro
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int QuantPaginas { get; set; }
        public DateTime DataLeitura { get; set; }
        public string Comentario { get; set; }
        public int UsuarioID { get; set; }

        private int _avaliacao;
        public int Avaliacao
        {
            get { return _avaliacao; }
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException("A avaliação deve ser entre 1 e 5.");

                _avaliacao = value;
            }
        }

        public bool Ativo { get; set; }

        public Livro()
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