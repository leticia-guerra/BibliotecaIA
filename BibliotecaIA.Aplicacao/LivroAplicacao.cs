using System;
using System.Collections.Generic;
using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Interfaces;

namespace BibliotecaIA.Aplicacao
{
    public class LivroAplicacao : ILivroAplicacao
    {
        private readonly ILivroRepositorio _livroRepositorio;

        public LivroAplicacao(ILivroRepositorio livroRepositorio)
        {
            _livroRepositorio = livroRepositorio;
        }

        public int Criar(Livro livro)
        {
            if (livro == null)
                throw new Exception("Livro não pode ser vazio");

            ValidarInformacoesLivro(livro);

            return _livroRepositorio.Salvar(livro);
        }

        public void Atualizar(Livro livro)
        {
            var livroDominio = _livroRepositorio.Obter(livro.ID);

            if (livroDominio == null)
                throw new Exception("Livro não encontrado");

            ValidarInformacoesLivro(livro);

            livroDominio.Titulo = livro.Titulo;
            livroDominio.Autor = livro.Autor;
            livroDominio.Genero = livro.Genero;
            livroDominio.QuantPaginas = livro.QuantPaginas;
            livroDominio.DataLeitura = livro.DataLeitura;
            livroDominio.Avaliacao = livro.Avaliacao;
            livroDominio.Comentario = livro.Comentario;
            livroDominio.UsuarioID = livro.UsuarioID;

            _livroRepositorio.Atualizar(livroDominio);
        }

        public void Deletar(int livroID)
        {
            var livroDominio = _livroRepositorio.Obter(livroID);

            if (livroDominio == null)
                throw new Exception("Livro não encontrado");

            livroDominio.Deletar();

            _livroRepositorio.Atualizar(livroDominio);
        }

        public void Restaurar(int livroID)
        {
            var livroDominio = _livroRepositorio.Obter(livroID);

            if (livroDominio == null)
                throw new Exception("Livro não encontrado");

            livroDominio.Restaurar();

            _livroRepositorio.Atualizar(livroDominio);
        }

        public Livro Obter(int livroID)
        {
            var livroDominio = _livroRepositorio.Obter(livroID);

            if (livroDominio == null)
                throw new Exception("Livro não encontrado");

            return livroDominio;
        }

        public IEnumerable<Livro> ObterTodos()
        {
            return _livroRepositorio.ObterTodos();
        }

        public IEnumerable<Livro> Listar(bool ativo)
        {
            return _livroRepositorio.Listar(ativo);
        }

        public IEnumerable<Livro> ListarPorUsuario(int usuarioID)
        {
            return _livroRepositorio.ListarPorUsuario(usuarioID);
        }

        private static void ValidarInformacoesLivro(Livro livro)
        {
            if (string.IsNullOrEmpty(livro.Titulo))
                throw new Exception("Título não pode ser vazio");

            if (string.IsNullOrEmpty(livro.Autor))
                throw new Exception("Autor não pode ser vazio");

            if (string.IsNullOrEmpty(livro.Genero))
                throw new Exception("Gênero não pode ser vazio");

            if (livro.QuantPaginas <= 0)
                throw new Exception("Quantidade de páginas deve ser maior que zero");

            if (livro.UsuarioID <= 0)
                throw new Exception("Usuário inválido");

            if (livro.DataLeitura == default(DateTime))
                throw new Exception("Data de leitura inválida");
        }
    }
}