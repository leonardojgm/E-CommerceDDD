using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiSite.Token;

namespace WebApiSite.Controllers
{
    public class TokenController : Controller
    {
        #region Construtores

        public TokenController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager; 
        }

        #endregion

        #region Propriedades

        private readonly SignInManager<ApplicationUser> _signInManager;

        #endregion

        #region Métodos

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] InputModel Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password)) return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                var token = new TokenJWTBuilder().AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                                                 .AddSubject("Empresa E-Commerce")
                                                 .AddIssuer("Teste.Security.Bearer")
                                                 .AddAudience("Teste.Security.Bearer")
                                                 .AddClaim("UsuarioAPINumero", "1")
                                                 .AddExpiry(5)
                                                 .Builder();

                return Ok(token.value);
            }

            else return Unauthorized();
        }

        #endregion
    }
}