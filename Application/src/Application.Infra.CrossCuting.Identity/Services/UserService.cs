using Application.Infra.CrossCuting.Identity.Authorization;
using Application.Infra.CrossCuting.Identity.Interfaces;
using Application.Infra.CrossCuting.Identity.Models;
using Application.Infra.CrossCuting.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Infra.CrossCuting.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenDescriptor _tokenDescriptor;
        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            TokenDescriptor tokenDescriptor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenDescriptor = tokenDescriptor;
        }
        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math
                .Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        public List<IdentityRole> ObterPerfisDeCargos()
        {
            return _roleManager.Roles?.ToList();
        }

        public async Task<IdentityRole> ObterPerfilDeCargoPeloNome(string nome)
        {
            return await _roleManager.FindByNameAsync(nome);
        }

        public async Task<string> ObterPerfilDousuario(ApplicationUser usuario)
        {
            var roleString = await _userManager.GetRolesAsync(usuario);
            return roleString.FirstOrDefault();
        }

        public async Task<IdentityRole> ObterPerfilDeCargoPeloId(Guid cargoId)
        {
            return await _roleManager.FindByIdAsync(cargoId.ToString());
        }

        public async Task<IdentityResult> CriarPerfilDeCargo(string perfil)
        {
            return !await _roleManager.RoleExistsAsync(perfil)
                ? await _roleManager.CreateAsync(new IdentityRole(perfil))
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> GerarPermissoesUsuario(ApplicationUser user,
            Guid cargoId)
        {
            var resultRemoverPermissoes = await RemoverTodasAsPermissoesDoUsuario(user);

            if (!resultRemoverPermissoes.Succeeded) return resultRemoverPermissoes;

            var permisoesDoCargo = await ObterPermissoesDoCargo(cargoId);

            foreach (var claim in permisoesDoCargo)
            {
                resultRemoverPermissoes = await _userManager
                    .AddClaimAsync(user, new Claim(claim.Type, claim.Value));
            }
            return resultRemoverPermissoes;
        }

        public async Task<IList<Claim>> ObterTodasPermissoesCadastradas()
        {
            var cargoAdminApi = await ObterPerfilDeCargoPeloNome("AdminApi");
            return await ObterPermissoesDoCargo(Guid.Parse(cargoAdminApi.Id));
        }

        public async Task<IList<Claim>> ObterPermissoesDoCargo(Guid cargoId)
        {
            var cargo = await ObterPerfilDeCargoPeloId(cargoId);
            return await _roleManager.GetClaimsAsync(cargo);
        }

        public async Task<IdentityResult> AtualizarPermissoes(string email, Guid? cargoId)
        {
            var usuarioIdentity = await ObterUsuarioPorEmail(email);
            var roleString = await ObterPerfilDousuario(usuarioIdentity);

            if (string.IsNullOrWhiteSpace(roleString) && cargoId != null)
            {
                var roleUser = await ObterPerfilDeCargoPeloId((Guid)cargoId);
                var resultCargo = await _userManager.AddToRoleAsync(usuarioIdentity,
                    roleUser.Name);

                if (resultCargo.Succeeded)
                {
                    return await GerarPermissoesUsuario(usuarioIdentity,
                        Guid.Parse(roleUser.Id));
                }
            }
            var role = await ObterPerfilDeCargoPeloNome(roleString);

            var status = await RemoverTodasAsPermissoesDoUsuario(usuarioIdentity);

            if (status.Succeeded)
            {
                status = await GerarPermissoesUsuario(usuarioIdentity,
                    Guid.Parse(role.Id));
            }

            return status;
        }

        public async Task<IdentityResult> RemoverTodasAsPermissoesDoUsuario(
            ApplicationUser usuario)
        {
            var roleString = await ObterPerfilDousuario(usuario);

            if (string.IsNullOrWhiteSpace(roleString)) return IdentityResult.Failed();

            var role = await ObterPerfilDeCargoPeloNome(roleString);
            var claims = await ObterPermissoesDoCargo(Guid.Parse(role.Id));
            return await _userManager.RemoveClaimsAsync(usuario, claims);
        }

        public async Task<IdentityResult> AdicionarPermissaoAoCargo(Guid idDoCargo,
            string modulo, string permissao)
        {
            var claim = new Claim(modulo, permissao);
            var cargoPermissoes = await ObterPermissoesDoCargo(idDoCargo);
            var cargoCadastrado = await ObterPerfilDeCargoPeloId(idDoCargo);
            var exists = cargoPermissoes.Any(p => p.Type == modulo && p.Value == permissao);
            if (!exists && cargoCadastrado != null)
                return await _roleManager.AddClaimAsync(cargoCadastrado, claim);
            return IdentityResult.Failed();
        }

        public async Task<SignInResult> LoginIdentity(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);
            if (!result.Succeeded)
                return result;

            var resultPermissoes = await AtualizarPermissoes(model.Email, null);
            return resultPermissoes.Succeeded ? result : SignInResult.Failed;
        }

        public async Task<string> GerarTokenUsuario(LoginViewModel login, string nome)
        {
            var user = await ObterUsuarioPorEmail(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var cargo = await ObterPerfilDousuario(user);
            userClaims.Add(new Claim("Cargo", cargo));
            userClaims.Add(new Claim("Nome", nome));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                ClaimValueTypes.Integer64));

            // Necessário converter para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            return handler.WriteToken(securityToken);
        }

        public async Task<ApplicationUser> ObterUsuarioPorId(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<ApplicationUser> ObterUsuarioPorEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CriarUsuarioIdentity(RegisterViewModel userViewModel)
        {
            var user = new ApplicationUser { UserName = userViewModel.Email, Email = userViewModel.Email };

            var resultCriarUsuario = await _userManager.CreateAsync(user, userViewModel.Senha);

            if (!resultCriarUsuario.Succeeded) return resultCriarUsuario;

            var resultAdicionarCargo = await AdicionarCargoAoUsuario(user.Id, userViewModel.CargoId);

            if (resultAdicionarCargo.Succeeded)
                return resultCriarUsuario;

            await ExcluirUsuarioIdentity(user);

            return resultAdicionarCargo;
        }

        public async Task<IdentityResult> AtualizarUsuarioIdentity(ApplicationUser usuario, Guid cargoId)
        {
            var result = await _userManager.UpdateAsync(usuario);
            if (result.Succeeded)
                return await AtualizarPermissoes(usuario.Email, cargoId);
            return result;
        }

        public async Task<IdentityResult> AdicionarCargoAoUsuario(string usuarioId, Guid cargoId)
        {
            var user = await ObterUsuarioPorId(usuarioId);

            if (user == null) return IdentityResult.Failed();

            var cargo = await ObterPerfilDeCargoPeloId(cargoId);

            if (cargo == null) return IdentityResult.Failed();

            return await _userManager.AddToRoleAsync(user, cargo.Name);
        }

        public async Task<IdentityResult> ExcluirUsuarioIdentity(ApplicationUser usuario)
        {
            await RemoverTodasAsPermissoesDoUsuario(usuario);
            return await _userManager.DeleteAsync(usuario);
        }
    }
}