using System.Collections.Generic;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface ILivroAplicacao
    {
        int Criar(Livro livro);
        void Atualizar(Livro livro);
        void Deletar(int livroID);
        void Restaurar(int livroID);
        Livro Obter(int livroID);
        IEnumerable<Livro> ObterTodos();
        IEnumerable<Livro> Listar(bool ativo);
        IEnumerable<Livro> ListarPorUsuario(int usuarioID);
    }
}