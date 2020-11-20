﻿using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        #region Métodos

        Task AddProduct(Produto product);

        Task UpdateProduct(Produto product);

        #endregion
    }
}