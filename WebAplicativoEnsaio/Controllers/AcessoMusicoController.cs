using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebAplicativoEnsaio.Services;

namespace WebAplicativoEnsaio.Controllers
{
    public class AcessoMusicoController : Controller
    {
        // Injeção de dependência dos serviços necessários
        private readonly IEnsaioService _ensaioService;

        /// <summary>
        /// Construtor com injeção de dependência para o serviço de ensaios.
        /// </summary>
        public AcessoMusicoController(IEnsaioService ensaioService)
        {
            _ensaioService = ensaioService;
        }

        /// <summary>
        /// Exibe a view de músicos, carregando os ensaios e seus respectivos dados.
        /// </summary>
        /// <returns>Retorna a view com a lista de ensaios e suas músicas e músicos associados.</returns>
        public async Task<IActionResult> MusicosView()
        {
            // Obtém os ensaios do banco de dados, incluindo músicas e músicos, ordenados por data
            var ensaios = await _ensaioService.ObterTodosEnsaiosComMusicas();

            // Especifica o caminho completo para a view na pasta "AcessoMusico"
            return View("~/Views/AcessoMusico/MusicosView.cshtml", ensaios);
        }
    }
}