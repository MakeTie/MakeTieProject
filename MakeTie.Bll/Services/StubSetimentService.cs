using System.Collections.Generic;
using MakeTie.Bll.Interfaces;

namespace MakeTie.Bll.Services
{
    public class StubSetimentService : ISentimentService
    {
        public IEnumerable<string> GetEntities(string query)
        {
            return new[] { "shoes", "cheese", "wooden doll" };
        }
    }
}
