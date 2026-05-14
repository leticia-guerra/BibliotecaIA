using System.Collections.Generic;
using System.Linq;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Contexto;
using BibliotecaIA.Repositorio.Interfaces;

namespace BibliotecaIA.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(BibliotecaIAContext contexto) : base(contexto)
        {
        }

        public int Salvar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();

            return usuario.ID;
        }

        public void Atualizar(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            _contexto.SaveChanges();
        }

        public Usuario Obter(int usuarioID)
        {
            return _contexto.Usuarios
                .Where(u => u.ID == usuarioID)
                .Where(u => u.Ativo)
                .FirstOrDefault();
        }

        public Usuario ObterPorEmail(string email)
        {
            return _contexto.Usuarios
                .Where(u => u.Email == email)
                .Where(u => u.Ativo)
                .FirstOrDefault();
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _contexto.Usuarios.ToList();
        }

        public IEnumerable<Usuario> Listar(bool ativo)
        {
            return _contexto.Usuarios
                .Where(u => u.Ativo == ativo)
                .ToList();
        }
    }
}