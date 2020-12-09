using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    public class MontaMenuController : BaseController
    {
        #region Construtores

        public MontaMenuController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, InterfaceLogSistemaApp InterfaceLogSistemaApp, InterfaceMontaMenuApp InterfaceMontaMenuApp) 
            : base(logger, userManager, InterfaceLogSistemaApp) 
        {
            _InterfaceMontaMenuApp = InterfaceMontaMenuApp;
        }

        #endregion

        #region Propriedades

        private readonly InterfaceMontaMenuApp _InterfaceMontaMenuApp;

        #endregion

        #region Métodos

        [AllowAnonymous]
        [HttpGet("/api/ListarMenu")]
        public async Task<IActionResult> ListarMenu()
        {
            var listaMenu = new List<MenuSite>();
            var usuario = await RetornarIdUsuarioLogado();

            listaMenu = await _InterfaceMontaMenuApp.MontaMenuPorPerfil(usuario);

            return Json(new { listaMenu });
        }

        #endregion
    }
}