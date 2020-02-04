using System.Threading.Tasks;
using Application.Application.Interfaces;
using Application.Infra.CrossCuting.Identity.Interfaces;
using Application.Infra.CrossCuting.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioServiceApp _usuarioServiceApp;
        public UsuarioController(
            IUsuarioServiceApp usuarioServiceApp,
            IUser user) : base(user)
        {
            _usuarioServiceApp = usuarioServiceApp;
        }

        [HttpPost]
        [Authorize(Policy = "PodeAdicionarUsuario")]
        [Route("usuarios/registrar")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelStateValida()) return Response();

            var result = await _usuarioServiceApp.AdicionarUsuario(model);

            return result != null ? Response(result) : Response();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelStateValida()) return Response(model);

            var result = await _usuarioServiceApp.Login(model);

            return string.IsNullOrEmpty(result)
                ? Unauthorized(new
                {
                    success = false,
                    errors = ""// TODO: listar erros.
                })
                : Response(result);
        }
    }
}