using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssociationsService.Entities.Product;
using AssociationsService.Entities.Product.EBay;
using AssociationsService.Exceptions;
using AssociationsService.Utils.Interfaces;
using MakeTie.Bll.Interfaces;
using Newtonsoft.Json;

namespace AssociationsService.ProductProviders
{
    public class EBayProductProvider : IProductProvider
    {
        private const string ApiTemplate = "https://api.sandbox.ebay.com/buy/browse/v1/item_summary/search?q={0}";
        private const string ApiToken = "v^1.1#i^1#f^0#p^1#r^0#I^3#t^H4sIAAAAAAAAAOVXa2wURRzv9SXlGYEg8gjnQmIAd292t9vubXsn15ec9gVXkIeK+5ht1+4rO7u0F0OorQFjTDQQUcRHIxEFAomaSBrxk9gPkmDwS5WoEOs7ShMNKSb4mN27lmslUKFEEvfLZmf+85/f7ze//8wO6CouWbFj9Y7hGaHb8nu7QFd+KERPAyXFRStnFuQvKMoDOQGh3q5lXYXdBT9UItHQbWEtRLZlIhjuNHQTCUFjjPAcU7BEpCHBFA2IBFcWUomGeoGhgGA7lmvJlk6EkzUxguZpUVXKRFZVOIkFMm41R3K2WDGCZQHLQZpjOIlXJY7F/Qh5MGkiVzTdGMEAmicBTTJ0C80LpbzAAYorj24iwuuhgzTLxCEUIOIBXCEY6+RgvTpUESHouDgJEU8m6lJNiWRNbWNLZSQnVzyrQ8oVXQ+N/aq2FBheL+oevPo0KIgWUp4sQ4SISDwzw9ikQmIEzHXAD6RmoiqELCiXFVbiOShPipR1lmOI7tVx+C2aQqpBqABNV3PT11IUqyE9BmU3+9WIUyRrwv5rjSfqmqpBJ0bUViU2JpqbiXgKOm2alrTIhK3Vdtq6RaaqNpAKp/AAk1XIUiBBXgZcdqJMtqzM42aqtkxF80VD4UbLrYIYNRyvDZujDQ5qMpuchOr6iHLiGDCiIYfjIiOr6Lltpr+u0MBChIPPa6/A6GjXdTTJc+FohvEdgUQxQrRtTSHGdwZezNqnE8WINte1hUiko6OD6mApy2mNMADQkQ0N9Sm5DRoigWP9Ws/Ea9ceQGoBFRnikUgT3LSNsXRir2IAZisRZ6J0KVue1X0srPj41n805HCOjK2IyaoQTlL4KB2VGBk/nCJNRoXEsyaN+DigJKZJQ3TaoWvrogxJGfvMM6CjKQLLqQzLq5BUyqIqWRpVVVLilDKSxlULIJQkOcr/nwplolZPyZYNmy1dk9OTYvhJMzvrKM2i46arvDT+TkFdx6+Jev+KVJFP9SaS9Gv9Ooj6ORBOItoa5Tucki0jYol4a/ObtgSowxMJikhemmr1IHIxCgWfLhMepGGLULhQlIkPyZThjS6Jhg/sW8p1mG6Gt6ZkTloqIE+hrTLlQGR5Dv7JoJr8g6fFaocmLmPXsXQdOuvpG1Ji8o6c/+i4uSIrWdewjFtuNWb/ch+/AvfC7tClifhbdG8t5jRHl3FlgGPBDa1rdbCuLembup9eB73VFnKhchP+kCJj72vxvOChu0MfgO5QH77ygXJA0ivB8uKCdYUF0wmEt1QKiaYiWZ2UJqoU0lpNfB1xINUO07aoOfnFoc0Lf7r3Us5NsfdhMH/0rlhSQE/LuTiCRZd7iuhZd8ygeUAzNM2X8hzYBJZe7i2k5xXOfalu6UB/xXdi+R/pt3cf3/jx0TOHPwIzRoNCoaI8bOG8V9rMkk+WfTq08bxReWzfgc1Dp4vq6R35DbcTK3/Lu7CrdfFb5w4/fvB435SZq/afPDZ8xDtVHXv35KpH/6o/9OLu+iW98FC6cv9XX0wJ9xw/MffMgDDrtalwNh+fObB2ifHmeWPe4jXLK6qeWr3i/bvf+LLkBTURV19ODQ4v6Nu7r2nOrOfpz/Yk82qYXY0DUuW6s872bY+c/rmnv+fPvukDD81NDg4+e+frF/csvcd4p+H3OUeeWFhRt3X+c3eleo8y2+/qt79ufyC+V40q56hji5qnVlw8MPtkz+e/9u0ZHB66/5cn7/swXPZNa+22EzufXvLtoi0rXn1m6MIlnjpbegqcO92/Vn7wvYPfD/+4M7OMfwONadwHww8AAA==";
        private const string StoreName = "eBay";
        private const string StoreImageUrl = "stub";

        private readonly IHttpUtil _httpUtil;

        public EBayProductProvider(IHttpUtil httpUtil)
        {
            _httpUtil = httpUtil;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string searchQuery)
        {
            string responseString;

            try
            {
                responseString = await _httpUtil.GetAsync(string.Format(ApiTemplate, searchQuery), ApiToken);
            }
            catch (Exception ex)
            {
                throw new ProductServiceException("Cannot get response from the eBay API service", ex);
            }

            var eBayItems = JsonConvert.DeserializeObject<ItemsSearchResponse>(responseString).ItemSummaries;
            var products = MapEBayItemsToProducts(eBayItems ?? new List<EbayItem>());

            return products;
        }

        private static IEnumerable<Product> MapEBayItemsToProducts(IEnumerable<EbayItem> eBayItems)
        {
            var store = new Store {Name = StoreName, ImageUrl = StoreImageUrl};
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