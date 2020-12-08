using ApplicationApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApiSite.Controllers
{
    [Authorize]
    public class ProdutoAPIController : Controller
    {
        #region Construtores

        public ProdutoAPIController(InterfaceProductApp InterfaceProductApp, InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp) 
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