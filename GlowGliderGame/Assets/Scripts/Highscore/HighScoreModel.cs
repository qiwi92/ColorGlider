using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlowGlider.Shared;
using UnityEngine;

namespace Highscore
{
    public class HighScoreModel : IHighScoreModel
    {
        private readonly HighScoreApi _highScoreApi = new HighScoreApi("https://glowglider.azurewebsites.net");
        private readonly GuidProvider _guidProvider = new GuidProvider();
        private IReadOnlyList<HighScoreData> _highscoresAroundPlayer = new HighScoreData[0];


        public HighScoreModel()
        {
            UpdateHighscore();
        }

        private void UpdateHighscore()
        {
            var playerId = PlayerId;
            Task.Run(async () => await ReadData (playerId))
                .ContinueWith(c => _highscoresAroundPlayer = c.Result);
        }

        private async Task<IReadOnlyList<HighScoreData>> ReadData(Guid playerId)
        {
            Debug.Log("Calling Api");
            try
            {
                var highScoreDatas = await _highScoreApi.GetRanksAroundPlayer(playerId);
                Debug.Log("Received " + highScoreDatas.Count);
                return highScoreDatas;
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

            return null;
        }

        private Guid PlayerId => _guidProvider.GetGuid();

        private PlayerHighScore MapServerToViewHighscore(HighScoreData from)
        {
            return new PlayerHighScore
            {
                IsPlayer = from.PlayerId == PlayerId,
                PlayerName = from.PlayerName,
                Rank = from.Rank,
                Score = from.Score
            };
        }

        public void UploadHighScore(int highscore,string playerAlias)
        {
            var request = new PublishRequest(PlayerId.ToString(), playerAlias, highscore);
            Task.Run(async () => await _highScoreApi.PublishScore(request))
                .ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
                {
                    Debug.Log("Uploaded Highscore! " + request);
                    UpdateHighscore();
                });
        }

        public IEnumerable<PlayerHighScore> HighScoresAroundPlayer => _highscoresAroundPlayer.Select(MapServerToViewHighscore);
    }
}