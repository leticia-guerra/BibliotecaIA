using System;
using System.Collections.Generic;
using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Repositorio.Interfaces;

namespace BibliotecaIA.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public int Criar(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Usuario não pode ser vazio");

            ValidarSenha(usuario.Senha);
            ValidarInformacoesUsuario(usuario);

            return _usuarioRepositorio.Salvar(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuario.ID);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            ValidarInformacoesUsuario(usuario);

            usuarioDominio.Nome = usuario.Nome;
            usuarioDominio.Email = usuario.Email;

            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

        public void AlterarSenha(int usuarioID, string senhaAtual, string novaSenha)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioID);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            if (usuarioDominio.Senha != senhaAtual)
                throw new Exception("Senha atual inválida");

            ValidarSenha(novaSenha);

            usuarioDominio.Senha = novaSenha;

            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

        public Usuario Obter(int usuarioID)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioID);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            return usuarioDominio;
        }

        public Usuario ObterPorEmail(string email)
        {
            var usuarioDominio = _usuarioRepositorio.ObterPorEmail(email);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            return usuarioDominio;
        }

        public void Deletar(int usuarioID)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioID);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            usuarioDominio.Deletar();

            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

        public void Restaurar(int usuarioID)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioID);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado");

            usuarioDominio.Restaurar();

            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

        public IEnumerable<Usuario> Listar(bool ativo)
        {
            return _usuarioRepositorio.Listar(ativo);
        }

        private static void ValidarInformacoesUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
                throw new Exception("Nome não pode ser vazio");

            if (string.IsNullOrEmpty(usuario.Email))
                throw new Exception("Email não pode ser vazio");
        }

        private static void ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                throw new Exception("Senha não pode ser vazia");

            if (senha.Length < 4)
                throw new Exception("Senha deve ter pelo menos 4 caracteres");
        }
        public Usuario Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Email não pode ser vazio");

            if (string.IsNullOrEmpty(senha))
                throw new Exception("Senha não pode ser vazia");

            var usuarioDominio = _usuarioRepositorio.ObterPorEmail(email);

            if (usuarioDominio == null)
                throw new Exception("Email ou senha inválidos");

            if (usuarioDominio.Senha != senha)
                throw new Exception("Email ou senha inválidos");

            if (!usuarioDominio.Ativo)
                throw new Exception("Usuário inativo");

            return usuarioDominio;
        }
    }
}