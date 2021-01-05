using PlayDiscGolf.Core.Dtos.Cards;


namespace PlayDiscGolf.Core.Business.DtoBuilder.ScoreCard
{
    public interface IScoreCardDtoBuilder
    {
        public ScoreCardDto BuildScoreCardCreateInformation(string courseID);

        public ScoreCardDto BuildUpdatedScoreCardWithUpdatedPlayers(ScoreCardDto sessionModel, string newName);
    }
}
