using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class ProdutoAPIController : BaseController
    {
        #region Construtores

        public ProdutoAPIController(UserManager<ApplicationUser> userManager, ILogger<BaseController> logger, InterfaceLogSistemaApp InterfaceLogSistemaApp, InterfaceProductApp InterfaceProductApp
            , InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp) : base(logger, userManager, InterfaceLogSistemaApp) 
        {
            _InterfaceProductApp = InterfaceProductApp;
            _InterfaceCompraUsuarioApp = InterfaceCompraUsuarioApp;
        }

        #endregion

        #region Propriedades

        private InterfaceProductApp _InterfaceProductApp;

        private InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;

        #endregion

        #region Métodos

        [HttpGet("/api/ListaProdutos")]
        public async Task<JsonResult> ListaProdutos(string descricao) { return Json(await _InterfaceProductApp.ListarPodutosComEstoque(descricao)); }

        #endregion
    }
}