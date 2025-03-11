using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WebAplicativoEnsaio.Data;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Implementação do serviço para envio de notificações via WhatsApp.
    /// </summary>
    public class NotificacaoService : INotificacaoService
    {
        private readonly MusicsDbContext _context;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor com injeção de dependência do banco de dados e configuração.
        /// </summary>
        public NotificacaoService(MusicsDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Envia notificações via WhatsApp para os músicos do ensaio.
        /// </summary>
        public async Task EnviarNotificacaoWhatsApp(int ensaioId, IConfiguration configuration)
        {
            Console.WriteLine("🔹 Função EnviarNotificacaoWhatsApp chamada!");

            var ensaio = await _context.Ensaios
                .FindAsync(ensaioId);

            if (ensaio == null)
            {
                Console.WriteLine("⚠️ Ensaio não encontrado!");
                throw new Exception("Ensaio não encontrado.");
            }

            var musicos = _context.Musicos
                .Where(m => m.Grupo == ensaio.GrupoSelecionado)
                .Select(m => new { m.Nome, m.NumeroTelefone })
                .ToList();

            Console.WriteLine($"📢 {musicos.Count} músicos encontrados para notificação!");

            var musicas = _context.Musics
                .Where(m => m.EnsaioId == ensaioId)
                .Select(m => m.Title)
                .ToList();

            string mensagem = $"🎵 Olá! O repertório para o ensaio do dia {ensaio.Date:dd/MM/yyyy} já está disponível! 🎶\n\n";
            mensagem += "**Músicas:**\n" + string.Join("\n", musicas);
            mensagem += "\n\n🎤 Músicos escalados:\n" + string.Join("\n", musicos.Select(m => m.Nome));
            mensagem += "\n\n📅 Nos vemos no ensaio!";

            // Configuração Twilio
            string accountSid = _configuration["Twilio:accountSid"];
            string authToken = _configuration["Twilio:authToken"];
            string fromNumber = _configuration["Twilio:fromNumber"];

            Console.WriteLine($"🔹 Twilio AccountSid: {accountSid}");
            Console.WriteLine($"🔹 Twilio AuthToken: {authToken}");
            Console.WriteLine($"🔹 Twilio FromNumber: {fromNumber}");

            if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(fromNumber))
            {
                throw new Exception("❌ Twilio AccountSid ou AuthToken não configurados corretamente.");
            }

            TwilioClient.Init(accountSid, authToken);

            foreach (var musico in musicos)
            {
                if (!string.IsNullOrEmpty(musico.NumeroTelefone))
                {
                    var to = new PhoneNumber($"whatsapp:+55{musico.NumeroTelefone}");
                    await MessageResource.CreateAsync(
                        body: mensagem,
                        from: new PhoneNumber(fromNumber),
                        to: to
                    );
                }
            }

            Console.WriteLine("✅ Notificação enviada com sucesso!");
        }
    }
}
