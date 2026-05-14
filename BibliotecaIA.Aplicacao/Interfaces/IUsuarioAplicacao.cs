using System.Collections.Generic;
using BibliotecaIA.Dominio.Entidades;

namespace BibliotecaIA.Aplicacao.Interfaces
{
    public interface IUsuarioAplicacao
    {
        int Criar(Usuario usuario);
        void AlterarSenha(int usuarioID, string senhaAtual, string novaSenha);
        void Atualizar(Usuario usuario);
        void Deletar(int usuarioID);
        void Restaurar(int usuarioID);
        Usuario ObterPorEmail(string email);
        Usuario Obter(int usuarioID);
        IEnumerable<Usuario> Listar(bool ativo);
        Usuario Login(string email, string senha);
    }
}