using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Language.V1;
using MakeTie.Bll.Entities.EntityAnalysis;
using MakeTie.Bll.Interfaces;

namespace MakeTie.Bll.Services
{
    public class EntityAnalysisService : IEntityAnalysisService
    {
        private readonly LanguageServiceClient _languageServiceClient;
        private readonly IMapper _mapper;

        public EntityAnalysisService(LanguageServiceClient languageServiceClient, IMapper mapper)
        {
            _languageServiceClient = languageServiceClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnalysisEntity>> GetEntitiesAsync(string text)
        {
            var requestDocument = new Document
            {
                Content = text,
                Type = Document.Types.Type.PlainText
            };

            var analysisResult = await _languageServiceClient.AnalyzeEntitiesAsync(requestDocument);
            var analysisEntities = _mapper.Map<IEnumerable<AnalysisEntity>>(analysisResult.Entities);
            analysisEntities = _mapper.Map(analysisResult, analysisEntities);

            return analysisEntities;
        }

        public async Task<IEnumerable<AnalysisEntity>> GetEntitiesAsync(string text, int maxEntitiesCount)
        {
            var analysisEntities = await GetEntitiesAsync(text);
            var analysisEntitiesLimited = analysisEntities.OrderBy(entity => entity.Salience).Take(maxEntitiesCount);

            return analysisEntitiesLimited;
        }

        public async Task<IEnumerable<AnalysisEntity>> GetEntitiesAsync(string text, int maxEntitiesCount, int minEntitiesSalience)
        {
            var analysisEntities = await GetEntitiesAsync(text, maxEntitiesCount);
            var analysisEntitiesLimited = analysisEntities.Where(entity => entity.Salience > minEntitiesSalience);

            return analysisEntitiesLimited;
        }
    }
}