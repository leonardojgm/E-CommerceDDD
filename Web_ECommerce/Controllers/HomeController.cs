using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web_ECommerce.Models;

namespace Web_ECommerce.Controllers
{
    [LogActionFilter]
    public class HomeController : BaseController
    {
        #region Construtores

        public HomeController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, InterfaceLogSistemaApp InterfaceLogSistemaApp) : base(logger, userManager, InterfaceLogSistemaApp) { }

        #endregion

        #region Métodos

        public IActionResult Index() { return View(); }

        public IActionResult Privacy() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() { return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); }

        #endregion
    }
}