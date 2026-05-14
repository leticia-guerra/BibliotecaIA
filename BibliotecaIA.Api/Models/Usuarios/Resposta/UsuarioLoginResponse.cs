namespace BibliotecaIA.Api.Models.Usuarios.Resposta
{
    public class UsuarioLoginResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int TipoUsuario { get; set; }
    }
}