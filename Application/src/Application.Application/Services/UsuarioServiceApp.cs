using Application.Application.Interfaces;
using Application.Infra.CrossCuting.Identity.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Application.Dto.Permissionamento;
using Application.Application.Dto.UsuariosDto;
using Application.Application.Helper.UsuarioHelper;
using Application.Domain.Entity;
using Application.Domain.Interface;
using Application.Infra.CrossCuting.Identity.Interfaces;

namespace Application.Application.Services
{
    public class UsuarioServiceApp : BaseServiceApp, IUsuarioServiceApp
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _uow;

        public UsuarioServiceApp(
            IUnitOfWork uow,
            IUsuarioRepository usuarioRepository,
            IUserService userService)
        {
            _usuarioRepository = usuarioRepository;
            _userService = userService;
            _uow = uow;
        }

        public List<UsuarioDto> ListarUsuarios()
        {
            var usuarios = _usuarioRepository.ObterTodos();
            if (usuarios == null)
                return null;
            var usuarioHelper = new UsuarioHelper();
            return usuarios.Select(usuario => usuarioHelper.EntityToDto(usuario)).ToList();
        }

        public UsuarioDto ObterUsuarioPorId(Guid id)
        {
            var usuarioEtity = _usuarioRepository.ObterPorId(id);
            var usuarioHelper = new UsuarioHelper();
            return usuarioHelper.EntityToDto(usuarioEtity);
        }

        public async Task<UsuarioDto> AdicionarUsuario(RegisterViewModel usuario)
        {
            var userEntity = _usuarioRepository.Buscar(user => 
                    user.Email == usuario.Email || user.CPF == usuario.CPF).FirstOrDefault();

            if (userEntity != null)
            {
                //TODO: Notificar erro (Já existe um usuário cadastrado com o mesmo Email ou CPF informado).
            }

            var resultCriarUsuarioIdentity = await _userService.CriarUsuarioIdentity(usuario);

            if (resultCriarUsuarioIdentity != null)
            {
                //TODO: Notificar erro (erros do identity).
            }

            var resultClaim = await _userService.AtualizarPermissoes(usuario.Email, usuario.CargoId);

            if (!resultClaim.Succeeded)
            {
                //TODO: Notificar erro (criar permissões).
            }

            var usuarioEntity = new Usuario(usuario.Nome, usuario.CPF, usuario.Telefone, usuario.Email);

            _usuarioRepository.Adicionar(usuarioEntity);

            if (_uow.Commit())
            {
                var usuarioSalvo = _usuarioRepository
                    .Buscar(usuarioObtido => 
                        usuarioObtido.Email == usuario.Email).FirstOrDefault();
                if (usuarioSalvo == null)
                    return null;
                var usuarioHelper = new UsuarioHelper();
                return usuarioHelper.EntityToDto(usuarioSalvo);
            }

            return null;
        }

        public async Task<string> Login(LoginViewModel model)
        {
            var result = await _userService.LoginIdentity(model);
            if (!result.Succeeded)
                return null;
            var usuarioEntity = _usuarioRepository.Buscar(usuario => usuario.Email == model.Email).FirstOrDefault();
            if (usuarioEntity == null)
                return null;
            return await _userService.GerarTokenUsuario(model, usuarioEntity.Nome);
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