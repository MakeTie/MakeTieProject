using System.Threading.Tasks;

namespace AssociationsService.Utils.Interfaces
{
    public interface IHttpUtil
    {
        Task<string> GetAsync(string query, string authToken = null);
    }
}