using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infraestructure.Repository.Generics;

namespace Infraestructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics <Produto>, IProduct { }
}
