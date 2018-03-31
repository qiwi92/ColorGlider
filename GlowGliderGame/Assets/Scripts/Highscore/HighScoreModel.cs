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
        private static TaskScheduler CurrentContext => TaskScheduler.FromCurrentSynchronizationContext();

        public IReadOnlyList<PlayerHighScore> RelevantHighScores { get; private set; }
        private readonly HighScoreApi _highScoreApi = new HighScoreApi("https://glowglider.azurewebsites.net");
        private readonly GuidProvider _guidProvider = new GuidProvider();
        
        public HighScoreModel()
        {
            UpdateHighscore();
        }

        private void UpdateHighscore()
        {
            var playerId = PlayerId;
            var hasAlias = !string.IsNullOrEmpty(PlayerPrefsService.Instance.Alias);
            Task.Run(async () => await ReadData(playerId, hasAlias))
                .ContinueWith(c => RelevantHighScores = ConvertScores(c.Result), CurrentContext);
        }


        private IReadOnlyList<PlayerHighScore> ConvertScores(IReadOnlyList<HighScoreData> result)
        {
            return result.Select(MapServerToViewHighscore).ToList();
        }

        private async Task<IReadOnlyList<HighScoreData>> ReadData(Guid playerId, bool hasAlias)
        {
            Debug.Log("Calling Api");
            try
            {
                if (!hasAlias)
                {
                    return await _highScoreApi.GetTop10Async();
                }

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
                .ContinueWith(res =>
                {
                    Debug.Log("Uploaded Highscore! " + request);
                    UpdateHighscore();
                }, CurrentContext);
        }
    }
}