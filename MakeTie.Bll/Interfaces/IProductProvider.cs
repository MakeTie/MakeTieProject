using System.Collections.Generic;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Product;

namespace MakeTie.Bll.Interfaces
{
    public interface IProductProvider
    {
        Task<IEnumerable<Product>> GetProductsAsync(string searchQuery);
    }
}