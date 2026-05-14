using System.Collections.Generic;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Repositorio.Interfaces
{
    public interface ICatalogoLivroRepositorio
    {
        int Salvar(CatalogoLivro livro);
        void Atualizar(CatalogoLivro livro);
        CatalogoLivro Obter(int livroID);
        IEnumerable<CatalogoLivro> ObterTodos();
        IEnumerable<CatalogoLivro> Listar(bool ativo);
        IEnumerable<CatalogoLivro> ListarPorGenero(string genero);
    }
}