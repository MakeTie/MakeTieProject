using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Product;
using MakeTie.Bll.Entities.Product.EBay;
using MakeTie.Bll.Exceptions;
using MakeTie.Bll.Interfaces;
using MakeTie.Bll.Utils.Interfaces;
using Newtonsoft.Json;

namespace MakeTie.Bll.ProductProviders
{
    public class EBayProductProvider : IProductProvider
    {
        private readonly IHttpUtil _httpUtil;
        private readonly IEBaySettings _settings;

        public EBayProductProvider(IHttpUtil httpUtil, IEBaySettings settings)
        {
            _httpUtil = httpUtil;
            _settings = settings;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string searchQuery)
        {
            string responseString;

            try
            {
                responseString = await _httpUtil
                    .GetAsync(string.Format(_settings.ApiTemplate, searchQuery), _settings.ApiToken);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Cannot get response from the eBay API service", ex);
            }

            var eBayItems = JsonConvert.DeserializeObject<ItemsSearchResponse>(responseString).ItemSummaries;
            var products = MapEBayItemsToProducts(eBayItems ?? new List<EbayItem>());

            return products;
        }

        private IEnumerable<Product> MapEBayItemsToProducts(IEnumerable<EbayItem> eBayItems)
        {
            var store = new Store {Name = _settings.StoreName, ImageUrl = _settings.StoreImageUrl };
            var products = eBayItems.Select(item => new Product
            {
                Store = store,
                ImageUrl = item.Image?.ImageUrl,
                Price = new Price {Currency = item.Price.Currency, Value = item.Price.Value},
                SourceUrl = item.ItemWebUrl,
                Title = item.Title
            });

            return products;
        }
    }
}