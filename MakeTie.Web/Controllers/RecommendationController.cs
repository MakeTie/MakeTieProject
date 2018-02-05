using System.Collections.Generic;
using AssociationsService.Entities.Product;
using Microsoft.AspNetCore.Mvc;

namespace MakeTie.Web.Controllers
{
    [Route("api/[controller]")]
    public class RecommendationController : Controller
    {
        [HttpGet]
        public IEnumerable<Product> Get(string query)
        {
            return new List<Product>();
        }
    }
}