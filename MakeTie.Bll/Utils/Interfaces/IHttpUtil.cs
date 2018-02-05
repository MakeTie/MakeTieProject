using System.Threading.Tasks;

namespace MakeTie.Bll.Utils.Interfaces
{
    public interface IHttpUtil
    {
        Task<string> GetAsync(string query, string authToken = null);
    }
}