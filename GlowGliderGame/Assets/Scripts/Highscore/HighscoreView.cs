using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GlowGlider.Shared;

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
                entryView.UpdateDescription("....","....",false);

                _highScoreEntryViews[i] = entryView;
            }
        }

        private void Update()
        {
            var highscores = _model.HighScoresAroundPlayer;
            if (highscores != null && highscores.Any())
            {
                SetHighScores(highscores);
            }
        }

        private void SetHighScores(IEnumerable<PlayerHighScore> highScores)
        {
            var index = 0;

            foreach (var highScore in highScores)
            {
                _highScoreEntryViews[index].UpdateDescription(highScore.PlayerName,highScore.Score.ToString(), highScore.IsPlayer);
                index++;
            }
        }

    }
}