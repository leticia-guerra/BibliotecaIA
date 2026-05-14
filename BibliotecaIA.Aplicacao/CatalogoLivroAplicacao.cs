using System;
using System.Collections.Generic;
using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Interfaces;

namespace BibliotecaIA.Aplicacao
{
    public class CatalogoLivroAplicacao : ICatalogoLivroAplicacao
    {
        private readonly ICatalogoLivroRepositorio _repositorio;

        public CatalogoLivroAplicacao(ICatalogoLivroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public int Criar(CatalogoLivro livro)
        {
            if (livro == null)
                throw new Exception("Livro não pode ser vazio");

            if (string.IsNullOrEmpty(livro.Titulo))
                throw new Exception("Título é obrigatório");

            if (string.IsNullOrEmpty(livro.Genero))
                throw new Exception("Gênero é obrigatório");

            return _repositorio.Salvar(livro);
        }

        public void Atualizar(CatalogoLivro livro)
        {
            var livroDb = _repositorio.Obter(livro.ID);

            if (livroDb == null)
                throw new Exception("Livro não encontrado");

            livroDb.Titulo = livro.Titulo;
            livroDb.Autor = livro.Autor;
            livroDb.Genero = livro.Genero;
            livroDb.QuantPaginas = livro.QuantPaginas;
            livroDb.Resumo = livro.Resumo;

            _repositorio.Atualizar(livroDb);
        }

        public CatalogoLivro Obter(int livroID)
        {
            var livro = _repositorio.Obter(livroID);

            if (livro == null)
                throw new Exception("Livro não encontrado");

            return livro;
        }

        public IEnumerable<CatalogoLivro> Listar(bool ativo)
        {
            return _repositorio.Listar(ativo);
        }

        public IEnumerable<CatalogoLivro> ListarPorGenero(string genero)
        {
            return _repositorio.ListarPorGenero(genero);
        }
    }
}