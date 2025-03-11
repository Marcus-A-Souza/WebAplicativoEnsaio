using System.Collections.Generic;
using System.Threading.Tasks;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Interface para gerenciamento de músicos.
    /// </summary>
    public interface IMusicoService
    {
        /// <summary>
        /// Obtém todos os músicos cadastrados.
        /// </summary>
        Task<List<Musico>> ObterTodosMusicos();
    }
}
