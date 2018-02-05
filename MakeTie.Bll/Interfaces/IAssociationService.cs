using System.Collections.Generic;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Association;

namespace MakeTie.Bll.Interfaces
{
    public interface IAssociationService
    {
        Task<IEnumerable<Association>> GetAssociationsAsync(IEnumerable<string> words, int limitForEach);

        Task<Association> GetAssociationAsync(string word, int limit);
    }
}