using Microsoft.AspNetCore.Identity;

namespace WebAplicativoEnsaio.Models
{
    public class Usuario : IdentityUser
    {
        public string Role { get; set; }
    }
}
