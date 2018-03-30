using System.Collections.Generic;
using UnityEngine;

namespace Highscore
{
    public class HighscoreView : MonoBehaviour
    {
        [SerializeField] private HighScoreEntryView _highScoreEntryViewPrefab;
        [SerializeField] private Transform _highScoreEntryParent;
        private IHighScoreModel _model;

        private readonly HighScoreEntryView[] _highScoreEntryViews = new HighScoreEntryView[10];

        public void Initialize(IHighScoreModel model)
        {
            _model = model;

            for(int i =0; i < 10; i++)
            {
                var entryView = Instantiate(_highScoreEntryViewPrefab);
                entryView.transform.SetParent(_highScoreEntryParent);
                entryView.UpdateDescription("....","....");

                _highScoreEntryViews[i] = entryView;
            }

            UpdateHighScore();
        }

        public void UpdateHighScore()
        {
            //SetHighScores(_model.HighScoresAroundPlayer);
        }

        private void SetHighScores(IEnumerable<PlayerHighScore> highScoresAbovePlayers, PlayerHighScore playersHighScore, IEnumerable<PlayerHighScore> highScoresBelowPlayer)
        {
            var index = 0;
            foreach (var highScore in highScoresAbovePlayers)
            {
                _highScoreEntryViews[index].UpdateDescription(highScore.Name,highScore.Score.ToString());
                index++;
            }
        }

    }
}