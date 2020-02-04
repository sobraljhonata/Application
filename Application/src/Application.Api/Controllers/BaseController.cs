using System;
using System.Linq;
using Application.Infra.CrossCuting.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected Guid UsuarioLogadoId { get; set; }

        protected BaseController(IUser user)
        {
            if (user.IsAuthenticated()) UsuarioLogadoId = user.GetUserId();
        }

        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
                return Ok(new
                {
                    success = true,
                    data = result
                });

            return BadRequest(new
            {
                success = false,
                errors = "" // _notifications.GetNotifications().Select(n => n.Value) TODO: retornar lista de erros.
            });
        }

        protected bool OperacaoValida()
        {
            // TODO: Retornar true se não houverem erros no handler de erros.
            return false;
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            // TODO: Criar pattern de notificação de erros
        }

        protected bool ModelStateValida()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }
    }
}