using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAplicativoEnsaio.Data;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Implementação do serviço para gerenciamento de ensaios.
    /// </summary>
    public class EnsaioService : IEnsaioService
    {
        private readonly MusicsDbContext _context;

        /// <summary>
        /// Construtor do serviço com injeção de dependência do banco de dados.
        /// </summary>
        public EnsaioService(MusicsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os ensaios do banco de dados, incluindo as músicas associadas.
        /// </summary>
        public async Task<List<Ensaio>> ObterTodosEnsaiosComMusicas()
        {
            return await _context.Ensaios
                .Include(e => e.Musics)
                .OrderBy(e => e.Date)
                .ToListAsync();
        }

        /// <summary>
        /// Cria um novo ensaio no banco de dados.
        /// </summary>
        public async Task CriarEnsaio(Ensaio ensaio, int[] selectedMusicIds, string grupoSelecionado)
        {
            if (selectedMusicIds != null && selectedMusicIds.Any())
            {
                ensaio.Musics = await _context.Musics
                    .Where(m => selectedMusicIds.Contains(m.Id))
                    .ToListAsync();
            }

            ensaio.GrupoSelecionado = grupoSelecionado;
            _context.Ensaios.Add(ensaio);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém um ensaio por ID.
        /// </summary>
        public async Task<Ensaio> ObterEnsaioPorId(int id)
        {
            return await _context.Ensaios
                .Include(e => e.Musics)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Atualiza um ensaio existente.
        /// </summary>
        public async Task AtualizarEnsaio(Ensaio ensaio, int[] selectedMusicIds, string grupoSelecionado)
        {
            var existingEnsaio = await _context.Ensaios
                .Include(e => e.Musics)
                .FirstOrDefaultAsync(e => e.Id == ensaio.Id);

            if (existingEnsaio != null)
            {
                existingEnsaio.Date = ensaio.Date;
                existingEnsaio.GrupoSelecionado = grupoSelecionado;

                existingEnsaio.Musics.Clear();
                if (selectedMusicIds != null && selectedMusicIds.Any())
                {
                    existingEnsaio.Musics = await _context.Musics
                        .Where(m => selectedMusicIds.Contains(m.Id))
                        .ToListAsync();
                }

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Remove um ensaio pelo ID.
        /// </summary>
        public async Task RemoverEnsaio(int id)
        {
            var ensaio = await _context.Ensaios.FindAsync(id);
            if (ensaio != null)
            {
                _context.Ensaios.Remove(ensaio);
                await _context.SaveChangesAsync();
            }
        }
    }
}


