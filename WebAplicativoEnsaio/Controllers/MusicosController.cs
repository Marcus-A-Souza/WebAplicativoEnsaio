using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAplicativoEnsaio.Data;
using WebAplicativoEnsaio.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebAplicativoEnsaio.Controllers
{
    public class MusicosController : Controller
    {
        private readonly MusicsDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MusicosController(MusicsDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var musicos = await _context.Musicos.Include(m => m.Ensaio).ToListAsync();
            return View(musicos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.EnsaioId = _context.Ensaios
                .Select(e => new { e.Id, Data = e.Date.ToString("dd/MM/yyyy") })
                .ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Musico musico)
        {
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var numeroTelefone = User.Identity.Name; // O login é o número de telefone

            if (!ModelState.IsValid)
            {

                musico.UsuarioId = usuarioId;
                musico.NumeroTelefone = numeroTelefone ?? "Sem telefone"; // 🔹 Evita erro de NULL

                _context.Musicos.Add(musico);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EnsaioId = _context.Ensaios.Select(e => new { e.Id, e.Description }).ToList();
            return View(musico);

        }

        public IActionResult Edit(int id)
        {
            var musico = _context.Musicos
                .FirstOrDefault(m => m.Id == id);

            if (musico == null)
            {
                return NotFound();
            }

            ViewBag.Ensaios = _context.Ensaios
                .Select(e => new { e.Id, Data = e.Date.ToString("dd/MM/yyyy") })
                .ToList();
            return View(musico);
        }

        [HttpPost]
        public IActionResult Edit(Musico musico)
        {
            if (!ModelState.IsValid)
            {
                var existingMusico = _context.Musicos
                    .FirstOrDefault(m => m.Id == musico.Id);

                if (existingMusico == null)
                {
                    return NotFound();
                }

                var ensaioExiste = _context.Ensaios.Any(e => e.Id == musico.EnsaioId);
                if (!ensaioExiste)
                {
                    ModelState.AddModelError("EnsaioId", "O ensaio selecionado não existe.");
                }
                else
                {
                    existingMusico.Nome = musico.Nome;
                    existingMusico.Grupo = musico.Grupo;
                    existingMusico.EnsaioId = musico.EnsaioId;
                    existingMusico.NumeroTelefone = musico.NumeroTelefone ?? existingMusico.NumeroTelefone; // 🔹 Evita que o telefone seja apagado

                    _context.Musicos.Update(existingMusico);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.EnsaioId = _context.Ensaios
                .Select(e => new { e.Id, Data = e.Date.ToString("dd/MM/yyyy") })
                .ToList();
            return View(musico);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var musico = await _context.Musicos
                .Include(m => m.Ensaio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musico == null)
            {
                return NotFound();
            }
            return View(musico);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musico = await _context.Musicos.FindAsync(id);
            if (musico != null)
            {
                _context.Musicos.Remove(musico);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
