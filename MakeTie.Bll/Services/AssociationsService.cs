using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssociationsService.Entities.Association;
using AssociationsService.Exceptions;
using AssociationsService.Utils.Interfaces;
using MakeTie.Bll.Interfaces;
using Newtonsoft.Json;

namespace AssociationsService.Services
{
    public class AssociationsService : IAssociationService
    {
        private const string ApiTemplate = 
            "https://api.wordassociations.net/associations/v1.0/json/search?apikey={0}&text={1}&lang=en&limit={2}&pos=noun";
        private const string ApiKey = "4e64996e-0d38-4ca6-8053-401e8d697d9c";

        private readonly IHttpUtil _httpUtil;

        public AssociationsService(IHttpUtil httpUtil)
        {
            _httpUtil = httpUtil;
        }

        public async Task<IEnumerable<Association>> GetAssociationsAsync(IEnumerable<string> words, int limitForEach)
        {
            var tasks = words.Select(word => GetAssociationAsync(word, limitForEach)).ToList();
            await Task.WhenAll(tasks);

            return tasks.Select(task => task.Result);
        }

        public async Task<Association> GetAssociationAsync(string word, int limit)
        {
            string responseString;

            try
            {
                responseString = await _httpUtil.GetAsync(string.Format(ApiTemplate, ApiKey, word, limit));
            }
            catch (Exception ex)
            {
                throw new AssociationServiceException("Cannot get response from the API service", ex);
            }

            var responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseString);

            return responseModel.Response.FirstOrDefault();
        }
    }
}