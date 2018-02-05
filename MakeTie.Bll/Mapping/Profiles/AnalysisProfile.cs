using System.Collections.Generic;
using AutoMapper;
using Google.Cloud.Language.V1;
using MakeTie.Bll.Entities.EntityAnalysis;

namespace MakeTie.Bll.Mapping.Profiles
{
    public class AnalysisProfile : Profile
    {
        public AnalysisProfile()
        {
            CreateMap<Entity, AnalysisEntity>();
            CreateMap<AnalysisEntity, Entity>();
            CreateMap<AnalyzeEntitiesResponse, IEnumerable<AnalysisEntity>>()
                .AfterMap((source, entities) =>
                {
                    foreach (var entity in entities)
                    {
                        entity.Language = source.Language;
                    }
                });
        }
    }
}