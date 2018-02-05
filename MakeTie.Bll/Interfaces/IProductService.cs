using System.Collections.Generic;
using MakeTie.Bll.Entities.Product;

namespace MakeTie.Bll.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> SearchProducts(string query);
    }
}