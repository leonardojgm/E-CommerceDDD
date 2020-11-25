using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
        #region Construtores

        public RepositoryCompraUsuario() { _optionsBuilder = new DbContextOptions<ContextBase>(); }

        #endregion

        #region Propriedades

        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        #endregion

        #region Métodos

        #region ICompraUsuario

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using(var banco = new ContextBase(_optionsBuilder)) { return await banco.CompraUsuario.CountAsync(c => c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho); }
        }

        public async Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new ContextBase(_optionsBuilder)) 
            {
                var compraUsuario = new CompraUsuario();
                var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                     join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                                     where c.UserId.Equals(userId) && c.Estado == estado
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id,
                                                         Url = p.Url,
                                                     }).AsNoTracking().ToListAsync();

                compraUsuario.ListaProdutos = produtosCarrinhoUsuario;
                compraUsuario.ApplicationUser = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                compraUsuario.QuantidadeProdutos = produtosCarrinhoUsuario.Count();
                compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - CEP: " + compraUsuario.ApplicationUser.CEP);
                compraUsuario.ValorTotal = produtosCarrinhoUsuario.Sum(v => v.Valor);
                compraUsuario.Estado = estado;

                return compraUsuario;
            }
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            try
            {
                using (var banco = new ContextBase(_optionsBuilder))
                {
                    var compraUsuario = new CompraUsuario();

                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                         join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                                         where c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho
                                                         select c).AsNoTracking().ToListAsync();

                    produtosCarrinhoUsuario.ForEach(p => { p.Estado = EstadoCompra.Produto_Comprado; });

                    banco.UpdateRange(produtosCarrinhoUsuario);

                    await banco.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}