using System.Collections.Generic;

namespace MakeTie.Bll.Entities.Product.EBay
{
    public class ItemsSearchResponse
    {
        public IEnumerable<EbayItem> ItemSummaries { get; set; }
    }
}