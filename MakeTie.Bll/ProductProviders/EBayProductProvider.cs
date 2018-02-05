using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public EBayProductProvider(IHttpUtil httpUtil, IEBaySettings settings, IMapper mapper)
        {
            _httpUtil = httpUtil;
            _settings = settings;
            _mapper = mapper;
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
            var products = _mapper.Map<IEnumerable<Product>>(eBayItems);

            return products ?? new List<Product>();
        }
    }
}