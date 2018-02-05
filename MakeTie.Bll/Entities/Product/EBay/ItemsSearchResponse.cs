using System.Collections.Generic;

namespace AssociationsService.Entities.Product.EBay
{
    public class ItemsSearchResponse
    {
        public IEnumerable<EbayItem> ItemSummaries { get; set; }
    }
}