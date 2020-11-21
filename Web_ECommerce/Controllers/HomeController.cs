using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web_ECommerce.Models;

namespace Web_ECommerce.Controllers
{
    public class HomeController : Controller
    {
        #region Construtores

        public HomeController(ILogger<HomeController> logger) { _logger = logger; }

        #endregion

        #region Propriedades

        private readonly ILogger<HomeController> _logger;

        #endregion

        #region Métodos

        public IActionResult Index() { return View(); }

        public IActionResult Privacy() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() { return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); }

        #endregion
    }
}