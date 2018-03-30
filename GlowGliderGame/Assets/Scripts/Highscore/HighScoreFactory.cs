using UnityEngine;

namespace Highscore
{
    public class HighScoreFactory : MonoBehaviour
    {
        [SerializeField] private HighscoreView _highscoreView;

        private void Start()
        {
            var model = new HighScoreModel();
            _highscoreView.Initialize(model);
        }
    }
}