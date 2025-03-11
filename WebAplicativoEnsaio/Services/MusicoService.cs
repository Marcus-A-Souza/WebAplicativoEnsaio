using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAplicativoEnsaio.Data;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Implementação do serviço para gerenciamento de músicos.
    /// </summary>
    public class MusicoService : IMusicoService
    {
        private readonly MusicsDbContext _context;

        /// <summary>
        /// Construtor do serviço com injeção de dependência do contexto do banco de dados.
        /// </summary>
        public MusicoService(MusicsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os músicos cadastrados no sistema.
        /// </summary>
        public async Task<List<Musico>> ObterTodosMusicos()
        {
            return await _context.Musicos.ToListAsync();
        }
    }
}
