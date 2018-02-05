using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Product;
using MakeTie.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MakeTie.Web.Controllers
{
    [Route("api/[controller]")]
    public class RecommendationController : Controller
    {
        private readonly ISentimentService _sentimentService;
        private readonly IAssociationService _associationService;
        private readonly IProductService _productService;

        public RecommendationController(
            ISentimentService sentimentService,
            IAssociationService associationService,
            IProductService productService)
        {
            _sentimentService = sentimentService;
            _associationService = associationService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get(string query)
        {
            var entities = _sentimentService.GetEntities(query);
            var associations = await _associationService.GetAssociationsAsync(entities, 3);
            var products = new List<Product>();

            foreach (var association in associations)
            {
                foreach (var item in association.Items)
                {
                    products.AddRange(_productService.SearchProducts(item.Item)
                        .Where(product => product.ImageUrl != null)
                        .Take(3)
                        .ToList());
                }
            }

            return products;
        }
    }
}