using System.Collections.Generic;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Repositorio.Interfaces
{
    public interface ILivroRepositorio
    {
        int Salvar(Livro livro);
        void Atualizar(Livro livro);
        Livro Obter(int livroID);
        IEnumerable<Livro> ObterTodos();
        IEnumerable<Livro> Listar(bool ativo);
        IEnumerable<Livro> ListarPorUsuario(int usuarioID);
    }
}