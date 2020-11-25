using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        #endregion

        #endregion
    }
}