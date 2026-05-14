using BibliotecaIA.Repositorio.Contexto;

namespace BibliotecaIA.Repositorio
{
    public abstract class BaseRepositorio
    {
        protected readonly BibliotecaIAContext _contexto;

        protected BaseRepositorio(BibliotecaIAContext contexto)
        {
            _contexto = contexto;
        }
    }
}