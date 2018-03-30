using UnityEngine;

namespace Highscore
{
    public class HighScoreFactory : MonoBehaviour
    {
        [SerializeField] private HighscoreView _highscoreView;

        private void Start()
        {
            var model = new DummyHighScoreModel();
            _highscoreView.Initialize(model);
        }
    }
}