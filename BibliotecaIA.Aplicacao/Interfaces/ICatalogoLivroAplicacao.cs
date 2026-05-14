using System.Collections.Generic;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface ICatalogoLivroAplicacao
    {
        int Criar(CatalogoLivro livro);
        void Atualizar(CatalogoLivro livro);
        CatalogoLivro Obter(int livroID);
        IEnumerable<CatalogoLivro> Listar(bool ativo);
        IEnumerable<CatalogoLivro> ListarPorGenero(string genero);
    }
}