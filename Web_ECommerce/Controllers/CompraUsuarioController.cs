using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Web_ECommerce.Models;

namespace Web_ECommerce.Controllers
{
    [LogActionFilter]
    public class CompraUsuarioController : HelpQrCode
    {
        #region Construtores

        public CompraUsuarioController(InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, ILogger<BaseController> logger
            , InterfaceLogSistemaApp InterfaceLogSistemaApp) : base(logger, userManager, InterfaceLogSistemaApp)
        {
            _InterfaceCompraUsuarioApp = InterfaceCompraUsuarioApp;
            _environment = environment;
        }

        #endregion

        #region Propriedades

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;

        private IWebHostEnvironment _environment;

        #endregion

        #region Métodos

        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<JsonResult> AdicionarProdutoCarrinho(string id, string nome, string qtd) 
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            if (idUsuario != null)
            {
                await _InterfaceCompraUsuarioApp.AdicionarProdutoCarrinho(idUsuario, new CompraUsuario
                {
                    IdProduto = Convert.ToInt32(id),
                    QtdCompra = Convert.ToInt32(qtd),
                    Estado = EnumEstadoCompra.Produto_Carrinho,
                    UserId = idUsuario,
                });

                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false });    
        }

        [HttpGet("/api/QtdProdutoCarrinho")]
        public async Task<JsonResult> QtdProdutoCarrinho()
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            var qtd = 0;

            if (idUsuario != null)
            {
                qtd = await _InterfaceCompraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(idUsuario);

                return Json(new { sucesso = true, qtd = qtd });
            }

            return Json(new { sucesso = false, qtd = qtd });
        }

        public async Task<IActionResult> FinalizarCompra()
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            var compraUsuario = await _InterfaceCompraUsuarioApp.CarrinhoCompras(idUsuario);

            return View(compraUsuario);
        }

        public async Task<IActionResult> MinhasCompras(bool mensagem = false)
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            var compraUsuario = await _InterfaceCompraUsuarioApp.MinhasCompras(idUsuario);

            if (mensagem)
            {
                ViewBag.Sucesso = true;
                ViewBag.Mensagem = "Compra efetivada com sucesso. Pague o boleto para garantir sua compra!";
            }

            return View(compraUsuario);
        }

        public async Task<IActionResult> ConfirmarCompra()
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            var sucesso = await _InterfaceCompraUsuarioApp.ConfirmaCompraCarrinhoUsuario(idUsuario);

            if (sucesso) return RedirectToAction("MinhasCompras", new { mensagem = true });

            else return RedirectToAction("FinalizarCompra");
        }

        public async Task<IActionResult> Imprimir(int id)
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            var compraUsuario = await _InterfaceCompraUsuarioApp.ProdutosComprados(idUsuario, id);

            return await Download(compraUsuario, _environment);
        }

        #endregion
    }
}