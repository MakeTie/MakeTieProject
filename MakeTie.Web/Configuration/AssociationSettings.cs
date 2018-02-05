using MakeTie.Bll.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MakeTie.Web.Configuration
{
    public class AssociationSettings : IAssociationSettings
    {
        private const string SectionName = "AssociationSettings";
        private readonly IConfiguration _configuration;

        public AssociationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiTemplate => _configuration.GetSection(SectionName)[nameof(ApiTemplate)];

        public string ApiKey => _configuration.GetSection(SectionName)[nameof(ApiKey)];
    }
}