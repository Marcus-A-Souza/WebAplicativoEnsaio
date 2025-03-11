using System.Collections.Generic;
using System.Threading.Tasks;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Interface para gerenciamento de músicas.
    /// </summary>
    public interface IMusicsService
    {
        /// <summary>
        /// Obtém todas as músicas cadastradas.
        /// </summary>
        Task<List<Musics>> ObterTodasMusicas();
    }
}

