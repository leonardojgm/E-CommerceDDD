﻿using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        #region Construtores

        public ProdutosController(InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp, InterfaceProductApp InterfaceProductApp, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _InterfaceCompraUsuarioApp = InterfaceCompraUsuarioApp;
            _InterfaceProductApp = InterfaceProductApp;
            _userManager = userManager;
            _environment = environment;
        }

        #endregion

        #region Propriedades

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceProductApp _InterfaceProductApp;

        public readonly InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;

        private IWebHostEnvironment _environment;

        #endregion

        #region Métodos

        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _InterfaceProductApp.ListarProdutosUsuario(idUsuario)); 
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id) { return View(await _InterfaceProductApp.GetEntityById(id)); }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost] 
        [ValidateAntiForgeryToken]
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

                await SalvarImagemProduto(produto);
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
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _InterfaceProductApp.UpdateProduct(produto);

                if (produto.Notifycoes.Any())
                {
                    foreach (var item in produto.Notifycoes) ModelState.AddModelError(item.NomePropriedade, item.Mensagem);

                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique, ocorreu algum erro!";

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
        [HttpPost] 
        [ValidateAntiForgeryToken]
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

        [HttpGet("/api/ListarProdutosComEstoque")] 
        [AllowAnonymous]
        public async Task<JsonResult> ListarProdutosComEstoque() { return Json(await _InterfaceProductApp.ListarPodutosComEstoque()); }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _InterfaceProductApp.ListarProdutosCarrinhoUsuario(idUsuario));
        }

        public async Task<IActionResult> RemoverCarrinho(int id) { return View(await _InterfaceProductApp.ObterProdutosCarrinho(id)); }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _InterfaceCompraUsuarioApp.GetEntityById(id);

                await _InterfaceCompraUsuarioApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }

        public async Task SalvarImagemProduto (Produto produtoTela)
        {
            try
            {
                var produto = await _InterfaceProductApp.GetEntityById(produtoTela.Id);

                if (produtoTela.Imagem != null)
                {
                    var webRoot = _environment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/imgProdutos"));

                    permissionSet.AddPermission(writePermission);

                    var extension = System.IO.Path.GetExtension(produtoTela.Imagem.FileName);
                    var nomeArquivo = string.Concat(produto.Id, extension);
                    var diretorioArquivoSalvar = string.Concat(webRoot, "\\imgProdutos\\", nomeArquivo);

                    produtoTela.Imagem.CopyTo(new FileStream(diretorioArquivoSalvar, FileMode.Create));

                    produto.Url = string.Concat("https://localhost:5001", "/imgProdutos/", nomeArquivo);

                    await _InterfaceProductApp.UpdateProduct(produto);
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = true;
                ViewBag.Mensagem = erro.Message;
            }
        }

        #endregion
    }
}