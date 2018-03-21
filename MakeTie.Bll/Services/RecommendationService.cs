using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Product;
using MakeTie.Bll.Interfaces;

namespace MakeTie.Bll.Services
{
    public class RecommendationService : IRecommendationService
    {
        private const int MaxEntitiesCount = 3;
        private readonly IEntityAnalysisService _entityAnalysisService;
        private readonly IAssociationService _associationService;
        private readonly IProductService _productService;

        public RecommendationService(
            IEntityAnalysisService entityAnalysisService,
            IAssociationService associationService,
            IProductService productService)
        {
            _entityAnalysisService = entityAnalysisService;
            _associationService = associationService;
            _productService = productService;
        }

        public async Task<IEnumerable<Product>> GetRecommendationsAsync(string query)
        {
            var products = new List<Product>();
            var entities = await _entityAnalysisService.GetEntitiesAsync(query, MaxEntitiesCount);
            var stringEntities = entities.Select(entity => entity.Name);
            var associations = await _associationService.GetAssociationsAsync(stringEntities, MaxEntitiesCount);

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
