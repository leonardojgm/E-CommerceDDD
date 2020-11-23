using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        #region Construtores

        public ProdutosController(InterfaceProductApp InterfaceProductApp, UserManager<ApplicationUser> userManager) 
        { 
            _InterfaceProductApp = InterfaceProductApp;
            _userManager = userManager;
        }

        #endregion

        #region Propriedades

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceProductApp _InterfaceProductApp;

        #endregion

        #region Métodos

        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _InterfaceProductApp.ListaProdutosUsuario(idUsuario)); 
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id) { return View(await _InterfaceProductApp.GetEntityById(id)); }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarIdUsuarioLogado();

                produto.UserId = idUsuario;

                await _InterfaceProductApp.AddProduct(produto);

                if (produto.Notifycoes.Any())
                {
                    foreach (var item in produto.Notifycoes) ModelState.AddModelError(item.NomePropriedade, item.Mensagem);

                    return View("Create", produto);
                }
            }
            catch
            {
                return View("Create", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id) { return View(await _InterfaceProductApp.GetEntityById(id)); }

        // POST: ProdutosController/Edit/5
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _InterfaceProductApp.UpdateProduct(produto);

                if (produto.Notifycoes.Any())
                {
                    foreach (var item in produto.Notifycoes) ModelState.AddModelError(item.NomePropriedade, item.Mensagem);

                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id) { return View(await _InterfaceProductApp.GetEntityById(id)); }

        // POST: ProdutosController/Delete/5
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _InterfaceProductApp.GetEntityById(id);

                await _InterfaceProductApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        #endregion
    }
}