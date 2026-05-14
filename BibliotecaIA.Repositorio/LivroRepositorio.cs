using System.Collections.Generic;
using System.Linq;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Contexto;
using BibliotecaIA.Repositorio.Interfaces;

namespace BibliotecaIA.Repositorio
{
    public class LivroRepositorio : BaseRepositorio, ILivroRepositorio
    {
        public LivroRepositorio(BibliotecaIAContext contexto) : base(contexto)
        {
        }

        public int Salvar(Livro livro)
        {
            _contexto.Livros.Add(livro);
            _contexto.SaveChanges();

            return livro.ID;
        }

        public void Atualizar(Livro livro)
        {
            _contexto.Livros.Update(livro);
            _contexto.SaveChanges();
        }

        public Livro Obter(int livroID)
        {
            return _contexto.Livros
                .Where(l => l.ID == livroID && l.Ativo)
                .FirstOrDefault();
        }

        public IEnumerable<Livro> ObterTodos()
        {
            return _contexto.Livros.ToList();
        }

        public IEnumerable<Livro> Listar(bool ativo)
        {
            return _contexto.Livros
                .Where(l => l.Ativo == ativo)
                .ToList();
        }

        public IEnumerable<Livro> ListarPorUsuario(int usuarioID)
        {
            return _contexto.Livros
                .Where(l => l.UsuarioID == usuarioID && l.Ativo)
                .ToList();
        }
    }
}