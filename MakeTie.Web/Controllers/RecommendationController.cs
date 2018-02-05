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
        private const int MaxEntitiesCount = 3;
        private readonly IEntityAnalysisService _entityAnalysisService;
        private readonly IAssociationService _associationService;
        private readonly IProductService _productService;

        public RecommendationController(
            IEntityAnalysisService entityAnalysisService,
            IAssociationService associationService,
            IProductService productService)
        {
            _entityAnalysisService = entityAnalysisService;
            _associationService = associationService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get(string query)
        {
            var entities = await _entityAnalysisService.GetEntitiesAsync(query, MaxEntitiesCount);
            var stringEntities = entities.Select(entity => entity.Name);
            var associations = await _associationService.GetAssociationsAsync(stringEntities, MaxEntitiesCount);
            var products = new List<Product>();

            foreach (var association in associations)
            {
                foreach (var item in association.Items)
                {
                    products.AddRange(_productService.SearchProducts(item.Item)
                        .Where(product => product.ImageUrl != null)
                        .Take(MaxEntitiesCount)
                        .ToList());
                }
            }

            return products;
        }
    }
}