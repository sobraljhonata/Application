using Application.Application.Interfaces;
using Application.Infra.CrossCuting.Identity.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Application.Dto.Permissionamento;
using Application.Application.Dto.Usuario;

namespace Application.Application.Services
{
    public class UsuarioServiceApp : BaseServiceApp, IUsuarioServiceApp
    {
        public List<UsuarioDto> ListarUsuarios()
        {
            throw new NotImplementedException();
        }

        public UsuarioDto ObterUsuarioPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> AdicionarUsuario(RegisterViewModel usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(LoginViewModel usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> EditarUsuario(RegisterViewModel usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExcluirUsuario(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<CargoDto> ObterCargos()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PermissoesDto>> ObterTodasAsPermissoes()
        {
            throw new NotImplementedException();
        }

        public async Task<CargoDto> CriarCargo(string cargo)
        {
            throw new NotImplementedException();
        }

        public async Task<CargoDto> AdicionarPermissoes(CargoDto cargo)
        {
            throw new NotImplementedException();
        }
    }
}