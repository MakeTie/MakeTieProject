using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.EntityAnalysis;
using MakeTie.Bll.Entities.Product;
using MakeTie.Bll.Exceptions;
using MakeTie.Bll.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MakeTie.Web.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    public class RecommendationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRecommendationService _recommendationService;

        private List<AnalysisEntity> _sampleEntities = new List<AnalysisEntity>()
        {
            new AnalysisEntity()
            {
                Language = "en",
                Name = "car",
                Salience = 1,
                Type = "entity"
            }
        };

        private List<Product> _sampleProducts = new List<Product>()
        {
            new Product
            {
                ImageUrl = "http://ottstone.com/wp-content/uploads/2017/06/5550-a65ab931a7b6f60aa78db8a0e5e9e99f-600x600.jpg",
                SourceUrl = "http://ottstone.com/wp-content/uploads/2017/06/5550-a65ab931a7b6f60aa78db8a0e5e9e99f-600x600.jpg",
                Store = new Store
                {
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/EBay_logo.svg/1000px-EBay_logo.svg.png",
                    Name = "ebay"
                },
                Title = "Blue coat for you",
                Price = new Price
                {
                    Value = Decimal.One,
                    Currency = "USD"
                }
            },
            new Product
            {
                ImageUrl = "http://ottstone.com/wp-content/uploads/2017/06/5550-a65ab931a7b6f60aa78db8a0e5e9e99f-600x600.jpg",
                SourceUrl = "http://ottstone.com/wp-content/uploads/2017/06/5550-a65ab931a7b6f60aa78db8a0e5e9e99f-600x600.jpg",
                Store = new Store
                {
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/EBay_logo.svg/1000px-EBay_logo.svg.png",
                    Name = "ebay"
                },
                Title = "Blue coat for you",
                Price = new Price
                {
                    Value = Decimal.One,
                    Currency = "USD"
                }
            },
        };

        public RecommendationController(ILogger logger, IRecommendationService recommendationService)
        {
            _logger = logger;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            try
            {
                //var products = (await _recommendationService.GetRecommendationsAsync(query)).ToList();

                return Ok(_sampleProducts);
            }
            catch (AssociationServiceException ex)
            {
                return ReturnServiceUnavailableResult(ex);
            }
            catch (ProductServiceException ex)
            {
                return ReturnServiceUnavailableResult(ex);
            }
            catch (EntityAnalysisServiceException ex)
            {
                return ReturnServiceUnavailableResult(ex);
            }
        }

        private IActionResult ReturnServiceUnavailableResult(Exception ex)
        {
            _logger.Error(ex.Message, ex);

            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }
    }
}