namespace MakeTie.Bll.Entities.Product.EBay
{
    public class EbayItem
    {
        public string Title { get; set; }

        public EBayImage Image { get; set; }

        public EBayPrice Price { get; set; }

        public string ItemWebUrl { get; set; }
    }
}