using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    public class CompraUsuarioController : Controller
    {
        #region Construtores

        public CompraUsuarioController(InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp, UserManager<ApplicationUser> userManager)
        {
            _InterfaceCompraUsuarioApp = InterfaceCompraUsuarioApp;
            _userManager = userManager;
        }

        #endregion

        #region Propriedades

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;

        #endregion

        #region Métodos

        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<JsonResult> AdicionarProdutoCarrinho(string id, string nome, string qtd) 
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                await _InterfaceCompraUsuarioApp.Add(new CompraUsuario
                {
                    IdProduto = Convert.ToInt32(id),
                    Qtd = Convert.ToInt32(qtd),
                    Estado = EstadoCompra.Produto_Carrinho,
                    UserId = usuario.Id,
                });

                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false });    
        }

        #endregion
    }
}