using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebAplicativoEnsaio.Services;

namespace WebAplicativoEnsaio.Controllers
{
    public class AccountController : Controller
    {
        // Declaração das dependências injetadas
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        // Construtor recebendo as dependências injetadas
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        // Exibe a página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Processa a tentativa de login
        [HttpPost]
        public async Task<IActionResult> Login(string phoneNumber, string password)
        {
            var result = await _accountService.LoginAsync(phoneNumber, password);

            if (result.Succeeded)
            {
                // Recupera o usuário autenticado
                //var user = await _userManager.FindByNameAsync(phoneNumber);

               // if (user == null)
                //{
                 //   ModelState.AddModelError(string.Empty, "Usuário não encontrado.");
                 //   return View();
               // }

                // Confere se o usuário é administrador
                var isAdmin = await _accountService.IsUserAdminAsync(phoneNumber);

                if (isAdmin)
                {
                    return RedirectToAction("Index", "Home"); // Administrador vai para Home
                }

                return RedirectToAction("MusicosView", "AcessoMusico"); // Músicos vão para MusicosView
            }

            ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
            return View();
        }

        // Exibe a página de registro de usuários
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Processa o registro de um novo usuário
        [HttpPost]
        public async Task<IActionResult> Register(string phoneNumber, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "As senhas não coincidem.");
                return View();
            }

            var result = await _accountService.RegisterAsync(phoneNumber, password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        // Realiza o logout do usuário
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            _logger.LogInformation("Usuário deslogado com sucesso.");
            return RedirectToAction("Login", "Account");
        }
    }
}
