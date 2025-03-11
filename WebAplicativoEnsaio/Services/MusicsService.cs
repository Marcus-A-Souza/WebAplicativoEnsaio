using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAplicativoEnsaio.Data;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Implementação do serviço para gerenciamento de músicas.
    /// </summary>
    public class MusicsService : IMusicsService
    {
        private readonly MusicsDbContext _context;

        /// <summary>
        /// Construtor com injeção de dependência do banco de dados.
        /// </summary>
        public MusicsService(MusicsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as músicas cadastradas no banco de dados.
        /// </summary>
        public async Task<List<Musics>> ObterTodasMusicas()
        {
            return await _context.Musics.ToListAsync();
        }
    }
}
