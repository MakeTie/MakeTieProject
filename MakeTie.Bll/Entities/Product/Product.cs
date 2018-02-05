using System.Collections.Generic;

namespace AssociationsService.Entities.Product
{
    public class Product
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public Price Price { get; set; }

        public string SourceUrl { get; set; }

        public Store Store { get; set; }
    }
}