using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class DebugEnterHighScoreView : MonoBehaviour
    {
        private HighScoreModel _model;

        [SerializeField] private InputField _scoreInput;
        [SerializeField] private InputField _nameInput;

        [SerializeField] private Button _confirmButton;
        public void Initialize(HighScoreModel model)
        {
            _model = model;

            _confirmButton.onClick.AddListener(Upload);
        }

        private void Upload()
        {
            _model.UploadHighScore(int.Parse(_scoreInput.text), _nameInput.text);
        }
    }
}