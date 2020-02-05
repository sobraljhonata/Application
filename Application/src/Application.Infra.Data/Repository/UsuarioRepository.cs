using Application.Domain.Entity;
using Application.Domain.Interface;
using Application.Infra.Data.Context;

namespace Application.Infra.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext context) : base(context)
        {
        }
    }
}