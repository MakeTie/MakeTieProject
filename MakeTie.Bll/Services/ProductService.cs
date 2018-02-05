using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Product;
using MakeTie.Bll.Interfaces;

namespace MakeTie.Bll.Services
{
    public class ProductService : IProductService
    {
        private readonly IEnumerable<IProductProvider> _productProviders;

        public ProductService(IEnumerable<IProductProvider> productProviders)
        {
            _productProviders = productProviders;
        }

        public IEnumerable<Product> SearchProducts(string query)
        {
            var products = new List<Product>();

            var tasks = _productProviders.Select(provider => provider.GetProductsAsync(query)).ToList();
            Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                products.AddRange(task.Result);
            }

            return products;
        }
    }
}