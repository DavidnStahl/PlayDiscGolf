using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PlayDiscGolf.ViewModels.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.Session
{
    public class SessionStorageScoreCardViewModel : ISessionStorage<ScoreCardViewModel>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionStorageScoreCardViewModel(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public ScoreCardViewModel Get(string key) =>
            JsonConvert.DeserializeObject<ScoreCardViewModel>(_httpContextAccessor.HttpContext.Session.GetString(key));

        public void Save(string key, ScoreCardViewModel model) =>
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(model));
    }
}
