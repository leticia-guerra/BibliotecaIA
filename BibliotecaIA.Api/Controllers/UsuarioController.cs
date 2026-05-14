using BibliotecaIA.Aplicacao.Interfaces;
using BibliotecaIA.Api.Models.Usuarios.Requisicao;
using BibliotecaIA.Api.Models.Usuarios.Resposta;
using BibliotecaIA.Dominio.Entidades;
using BibliotecaIA.Dominio.Enumeradores;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaIA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;

        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromBody] UsuarioCriar request)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = request.Senha,
                    TipoUsuario = (TipoUsuario)request.TipoUsuario
                };

                var id = _usuarioAplicacao.Criar(usuario);

                return Ok(new UsuarioResponse
                {
                    Id = id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuario = usuario.TipoUsuario.ToString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UsuarioLogin request)
        {
            try
            {
                var usuario = _usuarioAplicacao.Login(request.Email, request.Senha);

                return Ok(new UsuarioLoginResponse
                {
                    Id = usuario.ID,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuario = (int)usuario.TipoUsuario
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Obter/{usuarioID}")]
        public IActionResult Obter([FromRoute] int usuarioID)
        {
            try
            {
                var usuario = _usuarioAplicacao.Obter(usuarioID);

                return Ok(new UsuarioResponse
                {
                    Id = usuario.ID,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuario = usuario.TipoUsuario.ToString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Listar/{ativo}")]
        public IActionResult Listar([FromRoute] bool ativo)
        {
            try
            {
                var usuarios = _usuarioAplicacao.Listar(ativo);

                var resposta = usuarios.Select(usuario => new UsuarioResponse
                {
                    Id = usuario.ID,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuario = usuario.TipoUsuario.ToString()
                });

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar([FromBody] UsuarioAtualizar request)
        {
            try
            {
                var usuario = new Usuario
                {
                    ID = request.Id,
                    Nome = request.Nome,
                    Email = request.Email
                };

                _usuarioAplicacao.Atualizar(usuario);

                return Ok("Usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarSenha")]
        public IActionResult AlterarSenha([FromBody] UsuarioAlterarSenha request)
        {
            try
            {
                _usuarioAplicacao.AlterarSenha(
                    request.UsuarioID,
                    request.SenhaAtual,
                    request.NovaSenha
                );

                return Ok("Senha alterada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Deletar/{usuarioID}")]
        public IActionResult Deletar([FromRoute] int usuarioID)
        {
            try
            {
                _usuarioAplicacao.Deletar(usuarioID);

                return Ok("Usuário deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Restaurar/{usuarioID}")]
        public IActionResult Restaurar([FromRoute] int usuarioID)
        {
            try
            {
                _usuarioAplicacao.Restaurar(usuarioID);

                return Ok("Usuário restaurado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}