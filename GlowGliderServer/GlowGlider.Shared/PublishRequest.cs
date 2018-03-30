namespace GlowGlider.Shared
{
    public class PublishRequest
    {
        public PublishRequest(string playerId, string playerAlias, int score)
        {
            PlayerId = playerId;
            PlayerAlias = playerAlias;
            Score = score;
        }

        public PublishRequest()
        {
        }

        public string PlayerId;
        public string PlayerAlias;
        public int Score;
        public string Token;
    }
}