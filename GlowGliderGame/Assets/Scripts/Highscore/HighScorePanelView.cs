﻿using System.Collections.Generic;
using UI.HighScore;
using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScorePanelView : MonoBehaviour
    {
        [SerializeField] private HighScoreEntryView _highScoreEntryViewPrefab;
        [SerializeField] private Transform _highScoreEntryParent;
        [SerializeField] private NameInputView _nameInputView;
        [SerializeField] private Image _backButtonImage;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private Image _refreshButtonImage;
         
        public Transform PanelTransform;
        public Button PlayButton;

        private IHighScoreModel _model;

        private readonly HighScoreEntryView[] _highScoreEntryViews = new HighScoreEntryView[10];
        private int _currentPlayerHighScore;

        public void Initialize(IHighScoreModel model)
        {
            _model = model;
            _model.UpdateHighScoreCallback += UpdateHighScore;

            for (int i = 0; i < 10; i++)
            {
                var entryView = Instantiate(_highScoreEntryViewPrefab);
                entryView.transform.SetParent(_highScoreEntryParent, false);
                entryView.UpdateDescription("....", 0, i, false);

                _highScoreEntryViews[i] = entryView;
            }

            _nameInputView.Initialize();
            _nameInputView.SubmitRequested += NameInputViewOnSubmitRequested;

            _refreshButton.onClick.AddListener(()=> _model.UpdateHighScore());
        }

        private void NameInputViewOnSubmitRequested(string playerAlias)
        {
            _model.UploadHighScore(_currentPlayerHighScore, playerAlias);
            _nameInputView.Close();
        }

        private void UpdateHighScore()
        {
            var highscores = _model.RelevantHighScores;
            if (highscores?.Count > 0)
            {
                SetHighScores(highscores);
            }
        }

        private void SetHighScores(IEnumerable<PlayerHighScore> highScores)
        {
            var index = 0;

            foreach (var highScore in highScores)
            {
                _highScoreEntryViews[index].UpdateDescription(highScore.PlayerName, highScore.Score, highScore.Rank, highScore.IsPlayer);
                index++;
            }
        }

        public void OpenHighScoreView(bool hasNewHighScore, int highScore)
        {
            _currentPlayerHighScore = highScore;
            _nameInputView.NewPlayerHighScoreText.text = highScore.ToString("0");

            if (hasNewHighScore)
            {
                _nameInputView.Open();
            }

        }

        public void SetColors(Color color)
        {
            _backButtonImage.color = color;
            _refreshButtonImage.color = color;
        }
    }
}