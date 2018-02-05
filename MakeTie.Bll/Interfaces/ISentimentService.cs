using System.Collections.Generic;

namespace MakeTie.Bll.Interfaces
{
    public interface ISentimentService
    {
        IEnumerable<string> GetEntities(string query);
    }
}