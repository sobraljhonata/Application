namespace Application.Application.Services
{
    public abstract class BaseServiceApp
    {
        //protected readonly DomainNotificationHandler _notifications;
        //private readonly IMediatorHandler _mediator;
        //protected Guid UsuarioLogadoId { get; set; }
        //public BaseServiceApp(
        //    IUser user,
        //    INotificationHandler<DomainNotification> notifications,
        //    IMediatorHandler mediator)
        //{
        //    UsuarioLogadoId = user.GetUserId();
        //    _notifications = (DomainNotificationHandler)notifications;
        //    _mediator = mediator;
        //}

        //protected bool OperacaoValida()
        //{
        //    return (!_notifications.HasNotifications());
        //}

        //protected void NotificarErro(string codigo, string mensagem)
        //{
        //    _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        //}

        //protected void AdicionarErrosIdentity(IdentityResult result)
        //{
        //    foreach (var error in result.Errors) NotificarErro(result.ToString(), error.Description);
        //}
    }
}