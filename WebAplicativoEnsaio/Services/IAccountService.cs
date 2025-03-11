using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Interface para gerenciamento de contas (login, registro e logout).
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        Task<SignInResult> LoginAsync(string phoneNumber, string password);

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        Task<IdentityResult> RegisterAsync(string phoneNumber, string password);

        /// <summary>
        /// Verifica se o usuário é administrador.
        /// </summary>
        Task<bool> IsUserAdminAsync(string phoneNumber);

        /// <summary>
        /// Realiza o logout do usuário.
        /// </summary>
        Task LogoutAsync();
    }
}
