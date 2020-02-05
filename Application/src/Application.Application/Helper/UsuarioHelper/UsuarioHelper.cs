using Application.Application.Dto.UsuariosDto;
using Application.Domain.Entity;

namespace Application.Application.Helper.UsuarioHelper
{
    public class UsuarioHelper : HelperBase<Usuario, UsuarioDto>
    {
        public override Usuario DtoToEntity(UsuarioDto obj)
        {
            return new Usuario(obj.Nome, obj.CPF, obj.Telefone, obj.Email);
        }

        public override UsuarioDto EntityToDto(Usuario obj)
        {
            return new UsuarioDto();
        }
    }
}
