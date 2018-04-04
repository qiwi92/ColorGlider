using System.Collections.Generic;
using UI.HighScore;
using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScorePanelView : MonoBehaviour
    {
        private const int NumberOfEntries = 10;

        private static readonly PlayerHighScore EmptyHighScore = 
            new PlayerHighScore { IsPlayer = false, PlayerName = "-----", Rank = 0, Score = 0 };

        [SerializeField] private HighScoreEntryView _highScoreEntryViewPrefab;
        [SerializeField] private Transform _highScoreEntryParent;
        [SerializeField] private NameInputView _nameInputView;
        [SerializeField] private Image _backButtonImage;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private Image _refreshButtonImage;


        [SerializeField] public GameObject Panel;
        [SerializeField] private Canvas _canvas;

        public Transform PanelTransform;
        public Button PlayButton;

        private IHighScoreModel _model;

        private readonly HighScoreEntryView[] _highScoreEntryViews = new HighScoreEntryView[NumberOfEntries];
        private int _currentPlayerHighScore;

        public void SetupCamera(Camera cam)
        {
            _canvas.worldCamera = cam;
        }

        public void Initialize(IHighScoreModel model)
        {
            _model = model;
            _model.UpdateHighScoreCallback += UpdateHighScore;

            for (int i = 0; i < NumberOfEntries; i++)
            {
                var entryView = Instantiate(_highScoreEntryViewPrefab);
                entryView.transform.SetParent(_highScoreEntryParent, false);
                entryView.UpdateDescription(EmptyHighScore);

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

        private void SetHighScores(IReadOnlyList<PlayerHighScore> highScores)
        {
            for (var i = 0; i < NumberOfEntries; i++)
            {
                var entryView = _highScoreEntryViews[i];
                if (i < highScores.Count)
                {
                    entryView.UpdateDescription(highScores[i]);
                }
                else
                {
                    entryView.Disable();
                }
            }
        }

        public void OpenHighScoreView(bool hasNewHighScore, int highScore)
        {
            _currentPlayerHighScore = highScore;
            _nameInputView.NewPlayerHighScoreText.text = highScore.ToString();

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