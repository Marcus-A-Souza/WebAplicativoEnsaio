using System.Collections.Generic;
using System.Threading.Tasks;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Interface para gerenciamento de ensaios.
    /// </summary>
    public interface IEnsaioService
    {
        /// <summary>
        /// Obtém todos os ensaios com suas músicas associadas.
        /// </summary>
        Task<List<Ensaio>> ObterTodosEnsaiosComMusicas();

        /// <summary>
        /// Cria um novo ensaio no banco de dados.
        /// </summary>
        Task CriarEnsaio(Ensaio ensaio, int[] selectedMusicIds, string grupoSelecionado);

        /// <summary>
        /// Obtém um ensaio por ID.
        /// </summary>
        Task<Ensaio> ObterEnsaioPorId(int id);

        /// <summary>
        /// Atualiza um ensaio existente.
        /// </summary>
        Task AtualizarEnsaio(Ensaio ensaio, int[] selectedMusicIds, string grupoSelecionado);

        /// <summary>
        /// Remove um ensaio pelo ID.
        /// </summary>
        Task RemoverEnsaio(int id);
    }
}
