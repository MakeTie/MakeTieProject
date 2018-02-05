using AutoMapper;
using MakeTie.Bll.Entities.Product;
using MakeTie.Bll.Entities.Product.EBay;
using MakeTie.Bll.Interfaces;

namespace MakeTie.Bll.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IEBaySettings eBaySettings)
        {
            CreateMap<EbayItem, Product>()
                .ForMember(
                    product => product.ImageUrl,
                    expression => expression.MapFrom(item => item.Image != null ? item.Image.ImageUrl : null))
                .ForMember(
                    product => product.SourceUrl,
                    expression => expression.MapFrom(item => item.ItemWebUrl))
                .ForMember(
                    product => product.Store,
                    expression => expression.UseValue(new Store
                    {
                        Name = eBaySettings.StoreName,
                        ImageUrl = eBaySettings.StoreImageUrl
                    }));

            CreateMap<EBayPrice, Price>();
        }
    }
}