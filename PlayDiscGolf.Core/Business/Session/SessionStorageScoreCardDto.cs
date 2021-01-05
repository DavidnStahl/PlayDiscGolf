using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PlayDiscGolf.Core.Dtos.Cards;

namespace PlayDiscGolf.Core.Business.Session
{
    public class SessionStorageScoreCardDto : ISessionStorage<ScoreCardDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionStorageScoreCardDto(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public ScoreCardDto Get(string key) =>
            JsonConvert.DeserializeObject<ScoreCardDto>(_httpContextAccessor.HttpContext.Session.GetString(key));

        public void Save(string key, ScoreCardDto model) =>
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(model));
    }
}
