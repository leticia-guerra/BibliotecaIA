using System.Collections.Generic;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        int Salvar(Usuario usuario);
        void Atualizar(Usuario usuario);
        Usuario Obter(int usuarioID);
        Usuario ObterPorEmail(string email);
        IEnumerable<Usuario> ObterTodos();
        IEnumerable<Usuario> Listar(bool ativo);
    }
}