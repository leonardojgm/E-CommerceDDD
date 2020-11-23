using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduct : IGeneric<Produto> 
    {
        #region Métodos

        Task<List<Produto>> ListaProdutosUsuario(string userId);

        #endregion
    }
}