using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Application.Dto.Permissionamento;
using Application.Application.Dto.UsuariosDto;
using Application.Infra.CrossCuting.Identity.Models.AccountViewModels;

namespace Application.Application.Interfaces
{
    public interface IUsuarioServiceApp
    {
        List<UsuarioDto> ListarUsuarios();
        UsuarioDto ObterUsuarioPorId(Guid id);
        Task<UsuarioDto> AdicionarUsuario(RegisterViewModel usuario);
        Task<string> Login(LoginViewModel model);
        Task<UsuarioDto> EditarUsuario(RegisterViewModel usuario);
        Task<bool> ExcluirUsuario(Guid id);
        List<CargoDto> ObterCargos();
        Task<List<PermissoesDto>> ObterTodasAsPermissoes();
        Task<CargoDto> CriarCargo(string cargo);
        Task<CargoDto> AdicionarPermissoes(CargoDto cargoUsuario);
    }
}