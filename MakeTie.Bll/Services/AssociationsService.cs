using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeTie.Bll.Entities.Association;
using MakeTie.Bll.Exceptions;
using MakeTie.Bll.Interfaces;
using MakeTie.Bll.Utils.Interfaces;
using Newtonsoft.Json;

namespace MakeTie.Bll.Services
{
    public class AssociationsService : IAssociationService
    {
        private readonly IHttpUtil _httpUtil;
        private readonly IAssociationSettings _settings;

        public AssociationsService(IHttpUtil httpUtil, IAssociationSettings settings)
        {
            _httpUtil = httpUtil;
            _settings = settings;
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
                responseString = await _httpUtil
                    .GetAsync(string.Format(_settings.ApiTemplate, _settings.ApiKey, word, limit));
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