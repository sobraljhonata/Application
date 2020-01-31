using Application.Infra.CrossCuting.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Infra.CrossCuting.Identity.Models.AccountViewModels;

namespace Application.Infra.CrossCuting.Identity.Interfaces
{
    public interface IUserService
    {
        List<IdentityRole> ObterPerfisDeCargos();
        Task<IdentityRole> ObterPerfilDeCargoPeloNome(string nome);
        Task<string> ObterPerfilDousuario(ApplicationUser usuario);
        Task<IdentityRole> ObterPerfilDeCargoPeloId(Guid cargoId);
        Task<IdentityResult> CriarPerfilDeCargo(string perfil);
        Task<IdentityResult> GerarPermissoesUsuario(ApplicationUser user, Guid cargoId);
        Task<IList<Claim>> ObterTodasPermissoesCadastradas();
        Task<IList<Claim>> ObterPermissoesDoCargo(Guid cargoId);
        Task<IdentityResult> AtualizarPermissoes(string email, Guid? cargoId);
        Task<IdentityResult> RemoverTodasAsPermissoesDoUsuario(ApplicationUser usuario);
        Task<IdentityResult> AdicionarPermissaoAoCargo(Guid idDoCargo, string modulo, string permissao);
        Task<SignInResult> LoginIdentity(LoginViewModel model);
        Task<string> GerarTokenUsuario(LoginViewModel login, string nome);
        Task<ApplicationUser> ObterUsuarioPorId(string Id);
        Task<ApplicationUser> ObterUsuarioPorEmail(string email);
        Task<IdentityResult> CriarUsuarioIdentity(RegisterViewModel userViewModel);
        Task<IdentityResult> AtualizarUsuarioIdentity(ApplicationUser usuario, Guid cargoId);
        Task<IdentityResult> AdicionarCargoAoUsuario(string usuarioId, Guid cargoId);
        Task<IdentityResult> ExcluirUsuarioIdentity(ApplicationUser usuario);
    }
}