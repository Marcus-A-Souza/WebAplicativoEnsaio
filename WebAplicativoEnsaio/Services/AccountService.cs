using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAplicativoEnsaio.Services
{
    /// <summary>
    /// Implementação do serviço de gerenciamento de contas.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Construtor com injeção de dependência.
        /// </summary>
        public AccountService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        public async Task<SignInResult> LoginAsync(string phoneNumber, string password)
        {
            return await _signInManager.PasswordSignInAsync(phoneNumber, password, false, false);
        }

        /// <summary>
        /// Registra um novo usuário e atribui "Administrador" se for o primeiro cadastrado ou se o nome for "Administrador".
        /// </summary>
        public async Task<IdentityResult> RegisterAsync(string phoneNumber, string password)
        {
            var user = new IdentityUser { UserName = phoneNumber, PhoneNumber = phoneNumber };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Criar a role "Administrador" se ainda não existir.
                if (!await _roleManager.RoleExistsAsync("Administrador"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Administrador"));
                }

                // Se for o primeiro usuário OU se o nome do usuário for "Administrador", atribuir a role
                if (await _userManager.Users.CountAsync() == 1 || phoneNumber == "Administrador")
                {
                    await _userManager.AddToRoleAsync(user, "Administrador");
                }
            }

            return result;
        }


        /// <summary>
        /// Verifica se o usuário é administrador.
        /// </summary>
        public async Task<bool> IsUserAdminAsync(string phoneNumber)
        {
            var user = await _userManager.FindByNameAsync(phoneNumber);

            if (user == null)
            {
                return false;
            }

            // Garante que o usuário está na role "Administrador"
            return await _userManager.IsInRoleAsync(user, "Administrador");
        }

        /// <summary>
        /// Realiza o logout do usuário.
        /// </summary>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
