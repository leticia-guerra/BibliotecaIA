using System.Collections.Generic;
using System.Linq;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Contexto;
using BibliotecaIA.Repositorio.Interfaces;

namespace BibliotecaIA.Repositorio
{
    public class CatalogoLivroRepositorio : BaseRepositorio, ICatalogoLivroRepositorio
    {
        public CatalogoLivroRepositorio(BibliotecaIAContext contexto) : base(contexto)
        {
        }

        public int Salvar(CatalogoLivro livro)
        {
            _contexto.CatalogoLivros.Add(livro);
            _contexto.SaveChanges();

            return livro.ID;
        }

        public void Atualizar(CatalogoLivro livro)
        {
            _contexto.CatalogoLivros.Update(livro);
            _contexto.SaveChanges();
        }

        public CatalogoLivro Obter(int livroID)
        {
            return _contexto.CatalogoLivros
                .Where(l => l.ID == livroID)
                .Where(l => l.Ativo)
                .FirstOrDefault();
        }

        public IEnumerable<CatalogoLivro> ObterTodos()
        {
            return _contexto.CatalogoLivros.ToList();
        }

        public IEnumerable<CatalogoLivro> Listar(bool ativo)
        {
            return _contexto.CatalogoLivros
                .Where(l => l.Ativo == ativo)
                .ToList();
        }

        public IEnumerable<CatalogoLivro> ListarPorGenero(string genero)
        {
            return _contexto.CatalogoLivros
                .Where(l => l.Genero.ToLower() == genero.ToLower())
                .Where(l => l.Ativo)
                .ToList();
        }
    }
}