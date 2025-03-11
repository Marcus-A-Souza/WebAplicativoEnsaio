using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebAplicativoEnsaio.Services;
using Microsoft.Extensions.Configuration;
using System;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Controllers
{
    public class EnsaioController : Controller
    {
        // Injeção de dependência para os serviços necessários
        private readonly IEnsaioService _ensaioService;
        private readonly IMusicoService _musicoService;
        private readonly IMusicsService _musicaService;
        private readonly INotificacaoService _notificacaoService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor com injeção de dependência.
        /// </summary>
        public EnsaioController(IEnsaioService ensaioService, IMusicoService musicoService, IMusicsService musicaService, INotificacaoService notificacaoService, IConfiguration configuration)
        {
            _ensaioService = ensaioService;
            _musicoService = musicoService;
            _musicaService = musicaService;
            _notificacaoService = notificacaoService;
            _configuration = configuration;
        }

        /// <summary>
        /// Redireciona para a listagem de ensaios.
        /// </summary>
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// Obtém a lista de ensaios com músicas associadas.
        /// </summary>
        public async Task<IActionResult> List()
        {
            var ensaios = await _ensaioService.ObterTodosEnsaiosComMusicas();
            ViewBag.Musicos = await _musicoService.ObterTodosMusicos();
            return View(ensaios);
        }

        /// <summary>
        /// Exibe o formulário de criação de ensaio.
        /// </summary>
        public async Task<IActionResult> Create()
        {
            ViewBag.Musics = await _musicaService.ObterTodasMusicas();
            return View("Create");
        }

        /// <summary>
        /// Processa a criação de um novo ensaio.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Ensaio ensaio, int[] selectedMusicIds, string grupoSelecionado)
        {
            if (!ModelState.IsValid)
            {
                await _ensaioService.CriarEnsaio(ensaio, selectedMusicIds, grupoSelecionado);
                return RedirectToAction("Index");
            }

            ViewBag.Musics = await _musicaService.ObterTodasMusicas();
            ViewBag.Musicos = await _musicoService.ObterTodosMusicos();
            return View(ensaio);
        }

        /// <summary>
        /// Exibe o formulário de edição de um ensaio.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var ensaio = await _ensaioService.ObterEnsaioPorId(id);
            if (ensaio == null)
            {
                return NotFound();
            }
            ViewBag.Musics = await _musicaService.ObterTodasMusicas();
            return View(ensaio);
        }

        /// <summary>
        /// Processa a edição de um ensaio.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ensaio ensaio, int[] selectedMusicIds, string grupoSelecionado)
        {
            if (id != ensaio.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await _ensaioService.AtualizarEnsaio(ensaio, selectedMusicIds, grupoSelecionado);
                return RedirectToAction("List");
            }

            ViewBag.Musics = await _musicaService.ObterTodasMusicas();
            return View(ensaio);
        }

        /// <summary>
        /// Exibe a confirmação de exclusão de um ensaio.
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var ensaio = await _ensaioService.ObterEnsaioPorId(id);
            if (ensaio == null)
            {
                return NotFound();
            }
            return View(ensaio);
        }

        /// <summary>
        /// Processa a exclusão de um ensaio.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ensaioService.RemoverEnsaio(id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Envia notificações via WhatsApp para os músicos do ensaio.
        /// </summary>
        public async Task<IActionResult> EnviarNotificacaoWhatsApp(int ensaioId)
        {
            await _notificacaoService.EnviarNotificacaoWhatsApp(ensaioId, _configuration);
            TempData["SuccessMessage"] = "Notificação enviada com sucesso!";
            return RedirectToAction("List");
        }
    }
}