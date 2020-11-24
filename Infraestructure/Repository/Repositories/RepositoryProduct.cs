using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Produto>, IProduct
    {
        #region Construtor

        public RepositoryProduct() { _optionsBuilder = new DbContextOptions<ContextBase>(); }

        #endregion

        #region Propriedades

        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        #endregion

        #region Métodos

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            using (var banco = new ContextBase(_optionsBuilder)) { return await banco.Produto.Where(p => p.UserId == userId).AsNoTracking().ToListAsync(); }
        }

        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var banco = new ContextBase(_optionsBuilder)) { return await banco.Produto.Where(exProduto).AsNoTracking().ToListAsync(); }
        }

        #endregion
    }
}