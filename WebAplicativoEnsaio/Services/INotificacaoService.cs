using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Interface para o serviço de notificações via WhatsApp.
    /// </summary>
    public interface INotificacaoService
    {
        /// <summary>
        /// Envia notificações via WhatsApp para os músicos do ensaio.
        /// </summary>
        /// <param name="ensaioId">ID do ensaio para o qual a notificação será enviada.</param>
        /// <param name="configuration">Configuração do Twilio para envio da notificação.</param>
        Task EnviarNotificacaoWhatsApp(int ensaioId, IConfiguration configuration);
    }
}
