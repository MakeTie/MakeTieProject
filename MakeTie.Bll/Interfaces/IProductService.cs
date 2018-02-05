using System.Collections.Generic;
using AssociationsService.Entities.Product;

namespace MakeTie.Bll.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> SearchProducts(string query);
    }
}