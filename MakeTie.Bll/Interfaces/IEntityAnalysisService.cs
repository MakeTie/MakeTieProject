using System.Collections.Generic;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.EntityAnalysis;

namespace MakeTie.Bll.Interfaces
{
    public interface IEntityAnalysisService
    {
        Task<IEnumerable<AnalysisEntity>> GetEntitiesAsync(string text);

        Task<IEnumerable<AnalysisEntity>> GetEntitiesAsync(string text, int maxEntitiesCount);

        Task<IEnumerable<AnalysisEntity>> GetEntitiesAsync(string text, int maxEntitiesCount, int minEntitiesSalience);
    }
}