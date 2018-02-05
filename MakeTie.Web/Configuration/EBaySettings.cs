using MakeTie.Bll.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MakeTie.Web.Configuration
{
    public class EBaySettings : IEBaySettings
    {
        private const string SectionName = "EBaySettings";
        private readonly IConfiguration _configuration;

        public EBaySettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiTemplate => _configuration.GetSection(SectionName)[nameof(ApiTemplate)];

        public string ApiToken => _configuration.GetSection(SectionName)[nameof(ApiToken)];

        public string StoreName => _configuration.GetSection(SectionName)[nameof(StoreName)];

        public string StoreImageUrl => _configuration.GetSection(SectionName)[nameof(StoreImageUrl)];
    }
}
