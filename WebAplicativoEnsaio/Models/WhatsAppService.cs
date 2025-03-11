using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WebAplicativoEnsaio.Services
{
    public class WhatsAppService
    {
        private readonly IConfiguration _configuration;

        public WhatsAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(string to, string message)
        {
            // Inicializar o Twilio
            string accountSid = _configuration["Twilio:"];
            string authToken = _configuration["Twilio:"];
            string from = _configuration["Twilio:"];

            TwilioClient.Init(accountSid, authToken);

            // Enviar mensagem
            var result = MessageResource.Create(
                from: new PhoneNumber(from),
                to: new PhoneNumber($"whatsapp:{to}"),
                body: message
            );
        }
    }
}
