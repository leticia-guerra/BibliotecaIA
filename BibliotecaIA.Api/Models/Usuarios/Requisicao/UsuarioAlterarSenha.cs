namespace BibliotecaIA.Api.Models.Usuarios.Requisicao
{
    public class UsuarioAlterarSenha
    {
        public int UsuarioID { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
    }
}