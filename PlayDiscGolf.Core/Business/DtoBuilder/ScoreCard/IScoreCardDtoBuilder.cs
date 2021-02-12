using PlayDiscGolf.Core.Dtos.Cards;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Business.DtoBuilder.ScoreCard
{
    public interface IScoreCardDtoBuilder
    {
        public ScoreCardDto BuildScoreCardCreateInformation(string courseID);

        public Task<ScoreCardDto> BuildUpdatedScoreCardWithUpdatedPlayersAsync(ScoreCardDto sessionModel, string newName);
    }
}
