using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScoreFaker : MonoBehaviour
    {
        private HighScorePanelView _view;

        [SerializeField]
        private Button _fakeButton;

        [SerializeField]
        private InputField _inputField;

        public void Initialize(HighScorePanelView view)
        {
            _view = view;
            _fakeButton.onClick.AddListener(FakeHighScore);
        }

        private void FakeHighScore()
        {
            var score = int.Parse( _inputField.text);
            _view.OpenHighScoreView(true, score);
        }
    }
}