using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAplicativoEnsaio.Data;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class MusicsController : Controller
    {
        private readonly MusicsDbContext _context;

        public MusicsController(MusicsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var musics = _context.Musics.ToList();
            Console.WriteLine("Número de músicas carregadas: " + musics.Count);
            return View(musics);
        }

        [HttpGet]
        public IActionResult Create() => View(); // Procura pela view Create.cshtml

        [HttpPost]
        public IActionResult Create(Musics musics)
        {
            if (!ModelState.IsValid) // Acrescentei uma exclamação e o sistema salvou as informacoes no BD//
            {
                Console.WriteLine("Salvando música: " + musics.Title);
                musics.CreatedAt = DateTime.Now;
                _context.Musics.Add(musics);
                _context.SaveChanges();
                Console.WriteLine("Música salva com sucesso.");
                return RedirectToAction("Index"); // deixei a View fazia//
            }

            Console.WriteLine("ModelState inválido.");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Erro: " + error.ErrorMessage);
            }

            return View(musics);
        }
        public IActionResult Edit(int id)
        {
            var musics = _context.Musics.FirstOrDefault(m => m.Id == id);
            if (musics == null) return NotFound();
            return View(musics);
        }

        [HttpPost]
        public IActionResult Edit(Musics musics)
        {
            if (!ModelState.IsValid)
            {
                _context.Musics.Update(musics);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musics);
        }

        public IActionResult Delete(int id)
        {
            var music = _context.Musics.FirstOrDefault(m => m.Id == id);
            if (music == null) return NotFound();
            return View(music);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var musics = _context.Musics.FirstOrDefault(m => m.Id == id);
            if (musics == null) return NotFound();

            _context.Musics.Remove(musics);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }


}    
